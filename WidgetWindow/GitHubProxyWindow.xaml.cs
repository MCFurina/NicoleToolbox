using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.AI.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NicoleToolbox.WidgetWindow
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GitHubProxyWindow : Window
    {
        public GitHubProxyWindow()
        {
            InitializeComponent();
            this.AppWindow.Resize(new SizeInt32(1200, 1000));
            this.AppWindow.SetIcon("Assets/NicoleToolbox.ico");
            try
            {
                this.AppWindow.TitleBar.PreferredTheme = TitleBarTheme.UseDefaultAppMode;
            }
            catch { }
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            string inputlink = Input.Text.Trim();
            if (string.IsNullOrEmpty(inputlink))
            {
                Output1.Text = null;
                Output2.Text = null;
                Output3.Text = null;
                Output4.Text = null;
                Output5.Text = null;
                OutputCard.Visibility = Visibility.Collapsed;
                TeachCard.Visibility = Visibility.Visible;
                return;
            }
            if (!inputlink.StartsWith("https://github.com/", StringComparison.OrdinalIgnoreCase))
            {
                ShowError("请输入正确的 GitHub 链接！", "提示");
                return;
            }
            OutputCard.Visibility = Visibility.Visible;
            TeachCard.Visibility = Visibility.Collapsed;
            Output1.Text = "https://gh-proxy.org/" + inputlink;
            Output2.Text = "https://v4.gh-proxy.org/" + inputlink;
            Output3.Text = "https://v6.gh-proxy.org/" + inputlink;
            Output4.Text = "https://cdn.gh-proxy.org/" + inputlink;
            Output5.Text = "https://axisnow.gh-proxy.org/" + inputlink;
        }

        private async void CopyButton1_Click(object sender, RoutedEventArgs e)
        {
            string outputlink = Output1.Text;
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(outputlink);
            Clipboard.SetContent(dataPackage);
            var originalText = CopyButton1.Content.ToString();
            CopyButton1.Content = "已复制";
            CopyButton1.IsEnabled = false;
            await Task.Delay(2000);
            CopyButton1.Content = originalText;
            CopyButton1.IsEnabled = true;
        }

        private void OpenButton1_Click(object sender, RoutedEventArgs e)
        {
            string outputlink = Output1.Text;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(outputlink) { UseShellExecute = true });
        }

        private async void CopyButton2_Click(object sender, RoutedEventArgs e)
        {
            string outputlink = Output2.Text;
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(outputlink);
            Clipboard.SetContent(dataPackage);
            var originalText = CopyButton2.Content.ToString();
            CopyButton2.Content = "已复制";
            CopyButton2.IsEnabled = false;
            await Task.Delay(2000);
            CopyButton2.Content = originalText;
            CopyButton2.IsEnabled = true;
        }

        private void OpenButton2_Click(object sender, RoutedEventArgs e)
        {
            string outputlink = Output2.Text;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(outputlink) { UseShellExecute = true });
        }

        private async void CopyButton3_Click(object sender, RoutedEventArgs e)
        {
            string outputlink = Output3.Text;
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(outputlink);
            Clipboard.SetContent(dataPackage);
            var originalText = CopyButton3.Content.ToString();
            CopyButton3.Content = "已复制";
            CopyButton3.IsEnabled = false;
            await Task.Delay(2000);
            CopyButton3.Content = originalText;
            CopyButton3.IsEnabled = true;
        }

        private void OpenButton3_Click(object sender, RoutedEventArgs e)
        {
            string outputlink = Output3.Text;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(outputlink) { UseShellExecute = true });
        }

        private async void CopyButton4_Click(object sender, RoutedEventArgs e)
        {
            string outputlink = Output4.Text;
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(outputlink);
            Clipboard.SetContent(dataPackage);
            var originalText = CopyButton4.Content.ToString();
            CopyButton4.Content = "已复制";
            CopyButton4.IsEnabled = false;
            await Task.Delay(2000);
            CopyButton4.Content = originalText;
            CopyButton4.IsEnabled = true;
        }

        private void OpenButton4_Click(object sender, RoutedEventArgs e)
        {
            string outputlink = Output4.Text;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(outputlink) { UseShellExecute = true });
        }

        private async void CopyButton5_Click(object sender, RoutedEventArgs e)
        {
            string outputlink = Output5.Text;
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(outputlink);
            Clipboard.SetContent(dataPackage);
            var originalText = CopyButton5.Content.ToString();
            CopyButton5.Content = "已复制";
            CopyButton5.IsEnabled = false;
            await Task.Delay(2000);
            CopyButton5.Content = originalText;
            CopyButton5.IsEnabled = true;
        }

        private void OpenButton5_Click(object sender, RoutedEventArgs e)
        {
            string outputlink = Output5.Text;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(outputlink) { UseShellExecute = true });
        }

        private async void ShowError(string message, string title)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "确定",
                XamlRoot = this.Content.XamlRoot,
                Style = Microsoft.UI.Xaml.Application.Current.Resources["DefaultContentDialogStyle"] as Style
            };
            if (this.Content is FrameworkElement rootElement)
            {
                dialog.RequestedTheme = rootElement.RequestedTheme;
            }
            await dialog.ShowAsync();
        }
    }
}
