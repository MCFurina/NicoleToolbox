using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NicoleToolbox.Pages.WebsiteFrames
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GIMapWindow : Window
    {
        public GIMapWindow()
        {
            InitializeComponent();
            AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/Logo.ico"));
            Title = "原神地图工具";
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(titleBar);
            var root = Content as FrameworkElement;
            if (root != null)
            {
                root.ActualThemeChanged += Root_ActualThemeChanged;
                UpdateTitleBar(root.ActualTheme);
            }
        }

        private void PinToTop(object sender, RoutedEventArgs e)
        {
            if (pinToTopToggle.IsOn)
            {
                OverlappedPresenter presenter = OverlappedPresenter.Create();
                presenter.IsAlwaysOnTop = true;
                AppWindow.SetPresenter(presenter);
            }
            else
            {
                OverlappedPresenter presenter = OverlappedPresenter.Create();
                presenter.IsAlwaysOnTop = false;
                AppWindow.SetPresenter(presenter);
            }
        }

        private void Reload(object sender, RoutedEventArgs e)
        {
            webview.Reload();
        }

        private void OpenWithEdge(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "https://act.mihoyo.com/ys/app/interactive-map/index.html");
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
}
