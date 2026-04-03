using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI;

namespace NicoleToolbox;

public class VideoItem
{
    // 修复：声明为可空 string，解决构造函数非空警告
    public string? Title { get; set; }
    public string? Cover { get; set; }
    public string? PageUrl { get; set; }
}

public sealed partial class VideoResourcesWindow : Window, INotifyPropertyChanged
{
    // 修复：声明为可空事件，匹配接口定义 + 解决非空警告
    public event PropertyChangedEventHandler? PropertyChanged;

    public ObservableCollection<VideoItem> CharacterVideos { get; } = new();
    public ObservableCollection<VideoItem> CutsceneVideos { get; } = new();

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (_isLoading != value) { _isLoading = value; OnPropertyChanged(); }
        }
    }

    public Visibility ToVisibility(bool isLoading) => isLoading ? Visibility.Visible : Visibility.Collapsed;

    private const string CHAR_VIDEO_URL = "https://baike.mihoyo.com/ys/obc/channel/map/80/212?bbs_presentation_style=no_header&visit_device=pc";
    private const string CUTSCENE_URL = "https://baike.mihoyo.com/ys/obc/channel/map/80/81?bbs_presentation_style=no_header&visit_device=pc";

    public VideoResourcesWindow()
    {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
        var root = Content as FrameworkElement;
        if (root != null)
        {
            root.ActualThemeChanged += Root_ActualThemeChanged;
            UpdateTitleBar(root.ActualTheme);
        }

        _ = InitializeAsync();
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        // 修复：空值触发前判断
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async Task InitializeAsync()
    {
        try
        {
            await CrawlerWebView.EnsureCoreWebView2Async();
            await LoadVideosAsync(CHAR_VIDEO_URL, CharacterVideos);
        }
        catch (Exception ex) { Debug.WriteLine($"Init failed: {ex.Message}"); }
    }

    private async void OnPivotSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // 修复：安全类型转换 + 空判断
        if (sender is Pivot pivot && pivot.SelectedItem is PivotItem item)
        {
            if (item.Tag?.ToString() == "Cutscene" && CutsceneVideos.Count == 0)
                await LoadVideosAsync(CUTSCENE_URL, CutsceneVideos);
            else if (item.Tag?.ToString() == "Character" && CharacterVideos.Count == 0)
                await LoadVideosAsync(CHAR_VIDEO_URL, CharacterVideos);
        }
    }

    private async Task LoadVideosAsync(string url, ObservableCollection<VideoItem> targetCollection)
    {
        if (IsLoading) return;
        IsLoading = true;
        try
        {
            var tcs = new TaskCompletionSource<bool>();
            void OnNav(CoreWebView2 s, CoreWebView2NavigationCompletedEventArgs a) => tcs.TrySetResult(true);

            CrawlerWebView.CoreWebView2.NavigationCompleted += OnNav;
            CrawlerWebView.CoreWebView2.Navigate(url);
            await tcs.Task;
            CrawlerWebView.CoreWebView2.NavigationCompleted -= OnNav;

            await Task.Delay(2000);

            string jsCode = @"
                (function() {
                    var items = [];
                    var nodes = document.querySelectorAll('.channel-content-container li');
                    nodes.forEach(function(li) {
                        var a = li.querySelector('a');
                        var img = li.querySelector('.item__img');
                        var title = li.querySelector('h5');
                        if(a && img && title) {
                            var link = a.getAttribute('href');
                            if(link && !link.startsWith('http')) link = 'https://baike.mihoyo.com' + link;
                            items.push({
                                Title: title.innerText.trim(),
                                Cover: img.getAttribute('data-src'),
                                PageUrl: link
                            });
                        }
                    });
                    return JSON.stringify(items);
                })();
            ";

            var json = await CrawlerWebView.ExecuteScriptAsync(jsCode);
            // 修复：增加 null 判断
            if (!string.IsNullOrEmpty(json) && json != "null")
            {
                var unescapedJson = JsonSerializer.Deserialize<string>(json);
                // 修复：判断反序列化结果不为空
                if (!string.IsNullOrEmpty(unescapedJson))
                {
                    var items = JsonSerializer.Deserialize<List<VideoItem>>(unescapedJson);
                    if (items != null)
                    {
                        targetCollection.Clear();
                        foreach (var item in items) targetCollection.Add(item);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[VideoResource] Load List Failed: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task<string?> GetVideoSourceUrlAsync(string pageUrl)
    {
        try
        {
            var tcs = new TaskCompletionSource<bool>();
            void OnNav(CoreWebView2 s, CoreWebView2NavigationCompletedEventArgs a) => tcs.TrySetResult(true);

            CrawlerWebView.CoreWebView2.NavigationCompleted += OnNav;
            CrawlerWebView.CoreWebView2.Navigate(pageUrl);
            await tcs.Task;
            CrawlerWebView.CoreWebView2.NavigationCompleted -= OnNav;

            await Task.Delay(2500);

            string jsExtract = @"
                (function() {
                    var v = document.querySelector('video');
                    if(v && v.src) return v.src;
                    var src = document.querySelector('source');
                    if(src && src.src) return src.src;
                    return '';
                })();
            ";

            var json = await CrawlerWebView.ExecuteScriptAsync(jsExtract);
            // 修复：空值判断
            var videoUrl = string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<string>(json);

            if (string.IsNullOrEmpty(videoUrl))
            {
                await CrawlerWebView.ExecuteScriptAsync("document.querySelector('.custom-video-wrapper')?.click()");
                await Task.Delay(1000);
                json = await CrawlerWebView.ExecuteScriptAsync(jsExtract);
                videoUrl = string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<string>(json);
            }

            return videoUrl;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Extract Video Failed: {ex.Message}");
            return null;
        }
    }

    private async void OnPlayClick(object sender, RoutedEventArgs e)
    {
        // 修复：安全类型转换
        if (sender is Button btn && btn.Tag is VideoItem item)
        {
            if (IsLoading) return;
            IsLoading = true;
            try
            {
                string? videoUrl = await GetVideoSourceUrlAsync(item.PageUrl!);

                if (!string.IsNullOrEmpty(videoUrl))
                {
                    OpenImmersivePlayer(item.Title ?? "视频", videoUrl);
                }
                else
                {
                    Debug.WriteLine("播放失败：无法解析视频地址");
                }
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    private async void OpenImmersivePlayer(string title, string videoUrl)
    {
        var playerWindow = new Window();
        playerWindow.Title = title;
        playerWindow.ExtendsContentIntoTitleBar = true;

        // 修复标题栏按钮颜色问题
        playerWindow.Activate(); 
        var tb = playerWindow.AppWindow.TitleBar;
        tb.ButtonForegroundColor = Colors.White;
        tb.ButtonHoverForegroundColor = Colors.White;
        tb.ButtonPressedForegroundColor = Colors.White;
        tb.ButtonBackgroundColor = Colors.Transparent;
        tb.ButtonHoverBackgroundColor = Color.FromArgb(255,45,45,45);
        tb.ButtonPressedBackgroundColor = Color.FromArgb(255,38,38,38);

        var rootGrid = new Grid();
        rootGrid.Background = new SolidColorBrush(Colors.Black);
        rootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(32) });
        rootGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

        var customTitleBar = new Grid();
        customTitleBar.Background = new SolidColorBrush(Color.FromArgb(100, 0, 0, 0));
        customTitleBar.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(16) });
        customTitleBar.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
        customTitleBar.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

        var titleText = new TextBlock()
        {
            Text = title,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 12,
            Foreground = new SolidColorBrush(Colors.White)
        };

        Grid.SetColumn(titleText, 2);
        customTitleBar.Children.Add(titleText);
        rootGrid.Children.Add(customTitleBar);
        playerWindow.SetTitleBar(customTitleBar);

        var webView = new WebView2();
        webView.DefaultBackgroundColor = Colors.Black;
        Grid.SetRow(webView, 1);
        rootGrid.Children.Add(webView);

        playerWindow.Content = rootGrid;

        await webView.EnsureCoreWebView2Async();

        string htmlContent = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <style>
        body {{ margin:0; background-color:black; display:flex; justify-content:center; align-items:center; height:100vh; overflow:hidden; }}
        video {{ max-width:100%; max-height:100%; outline:none; }}
        #speed-overlay {{ position: absolute; top: 10%; background: rgba(0,0,0,0.6); color: white; padding: 8px 16px; border-radius: 20px; font-size: 14px; display: none; pointer-events: none; }}
    </style>
</head>
<body>
    <div id='speed-overlay'>3.0x 速进中</div>
    <video controls autoplay loop id='player'>
        <source src='{videoUrl}' type='video/mp4'>
    </video>
    <script>
        const v = document.getElementById('player');
        const overlay = document.getElementById('speed-overlay');
        let isLongPress = false;
        let pressTimer = null;
        let lastRate = 1.0;

        document.addEventListener('keydown', (e) => {{
            if (e.code === 'Space' && !e.repeat) {{
                pressTimer = setTimeout(() => {{
                    isLongPress = true;
                    lastRate = v.playbackRate;
                    v.playbackRate = 3.0;
                    overlay.style.display = 'block';
                }}, 200);
            }}
        }});

        document.addEventListener('keyup', (e) => {{
            if (e.code === 'Space') {{
                clearTimeout(pressTimer);
                if (isLongPress) {{
                    v.playbackRate = lastRate;
                    overlay.style.display = 'none';
                    isLongPress = false;
                }}
            }}
        }});
    </script>
</body>
</html>";

        webView.NavigateToString(htmlContent);
    }

    private async void OnDownloadClick(object sender, RoutedEventArgs e)
    {
        // 修复：安全类型转换
        if (sender is Button btn && btn.Tag is VideoItem item)
        {
            if (IsLoading) return;
            IsLoading = true;
            try
            {
                string? videoUrl = await GetVideoSourceUrlAsync(item.PageUrl!);
                if (!string.IsNullOrEmpty(videoUrl))
                {
                    _ = Launcher.LaunchUriAsync(new Uri(videoUrl));
                }
            }
            finally
            {
                IsLoading = false;
            }
        }
    }

    private void Root_ActualThemeChanged(FrameworkElement sender, object args)
    {
        UpdateTitleBar(sender.ActualTheme);
    }

    private void UpdateTitleBar(ElementTheme theme)
    {
        var tb = AppWindow.TitleBar;

        if (theme == ElementTheme.Dark)
        {
            tb.ButtonForegroundColor = Colors.White;
            tb.ButtonHoverForegroundColor = Colors.White;
            tb.ButtonHoverBackgroundColor = ColorHelper.FromArgb(255, 45, 45, 45);
            tb.ButtonPressedBackgroundColor = ColorHelper.FromArgb(255, 38, 38, 38);
        }
        else
        {
            tb.ButtonForegroundColor = Colors.Black;
            tb.ButtonHoverForegroundColor = Colors.Black;
            tb.ButtonHoverBackgroundColor = ColorHelper.FromArgb(255, 232, 232, 232);
            tb.ButtonPressedBackgroundColor = ColorHelper.FromArgb(255, 210, 210, 210);
        }
    }
}
