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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NicoleToolbox.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ToolsPage : Page
    {
        public ToolsPage()
        {
            InitializeComponent();
        }

        private void shell_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "wt", UseShellExecute = true });
            }
            catch (Exception ex)
            {
                ShowError("打开失败，可能您的系统不支持此操作。\n错误信息：" + ex.Message, "错误");
            }
        }

        private void taskmgr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "taskmgr", UseShellExecute = true });
            }
            catch (Exception ex)
            {
                ShowError("打开失败，可能您的系统不支持此操作。\n错误信息：" + ex.Message, "错误");
            }
        }

        private void devmgmt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "devmgmt.msc", UseShellExecute = true });
            }
            catch (Exception ex)
            {
                ShowError("打开失败，可能您的系统不支持此操作。\n错误信息：" + ex.Message, "错误");
            }
        }

        private void advanced_system_settings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "SystemPropertiesAdvanced.exe", UseShellExecute = true });
            }
            catch (Exception ex)
            {
                ShowError("打开失败，可能您的系统不支持此操作。\n错误信息：" + ex.Message, "错误");
            }
        }

        private void compmgmt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "compmgmt.msc", UseShellExecute = true });
            }
            catch (Exception ex)
            {
                ShowError("打开失败，可能您的系统不支持此操作。\n错误信息：" + ex.Message, "错误");
            }
        }

        private async void ShowError(string message, string title)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "确定",
                XamlRoot = this.Content.XamlRoot,
                Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style
            };
            if (this.Content is FrameworkElement rootElement)
            {
                dialog.RequestedTheme = rootElement.RequestedTheme;
            }
            await dialog.ShowAsync();
        }

        private void shelladmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "wt", UseShellExecute = true, Verb = "runas" });
            }
            catch (Exception ex)
            {
                ShowError("打开失败，可能您的系统不支持此操作。\n错误信息：" + ex.Message, "错误");
            }
        }

        private void ghproxy_Click(object sender, RoutedEventArgs e)
        {
            new WidgetWindow.GitHubProxyWindow().Activate();
        }
    }
}
