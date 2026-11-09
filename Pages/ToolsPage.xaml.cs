using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.ApplicationModel.Resources;
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
            var resourceLoader = new ResourceLoader();
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "wt", UseShellExecute = true });
            }
            catch (Exception ex)
            {
                ShowError(resourceLoader.GetString("Dialog_OpenError") + "\n" + resourceLoader.GetString("Dialog_ErrorInfo") + ex.Message, resourceLoader.GetString("Dialog_Title_Error"));
            }
        }

        private void taskmgr_Click(object sender, RoutedEventArgs e)
        {
            var resourceLoader = new ResourceLoader();
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "taskmgr", UseShellExecute = true });
            }
            catch (Exception ex)
            {
                ShowError(resourceLoader.GetString("Dialog_OpenError") + "\n" + resourceLoader.GetString("Dialog_ErrorInfo") + ex.Message, resourceLoader.GetString("Dialog_Title_Error"));
            }
        }

        private void devmgmt_Click(object sender, RoutedEventArgs e)
        {
            var resourceLoader = new ResourceLoader();
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "devmgmt.msc", UseShellExecute = true });
            }
            catch (Exception ex)
            {
                ShowError(resourceLoader.GetString("Dialog_OpenError") + "\n" + resourceLoader.GetString("Dialog_ErrorInfo") + ex.Message, resourceLoader.GetString("Dialog_Title_Error"));
            }
        }

        private void advanced_system_settings_Click(object sender, RoutedEventArgs e)
        {
            var resourceLoader = new ResourceLoader();
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "SystemPropertiesAdvanced.exe", UseShellExecute = true });
            }
            catch (Exception ex)
            {
                ShowError(resourceLoader.GetString("Dialog_OpenError") + "\n" + resourceLoader.GetString("Dialog_ErrorInfo") + ex.Message, resourceLoader.GetString("Dialog_Title_Error"));
            }
        }

        private void compmgmt_Click(object sender, RoutedEventArgs e)
        {
            var resourceLoader = new ResourceLoader();
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "compmgmt.msc", UseShellExecute = true });
            }
            catch (Exception ex)
            {
                ShowError(resourceLoader.GetString("Dialog_OpenError") + "\n" + resourceLoader.GetString("Dialog_ErrorInfo") + ex.Message, resourceLoader.GetString("Dialog_Title_Error"));
            }
        }

        private async void ShowError(string message, string title)
        {
            var resourceLoader = new ResourceLoader();
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = resourceLoader.GetString("Dialog_OK"),
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
            var resourceLoader = new ResourceLoader();
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "wt", UseShellExecute = true, Verb = "runas" });
            }
            catch (Exception ex)
            {
                ShowError(resourceLoader.GetString("Dialog_OpenError") + "\n" + resourceLoader.GetString("Dialog_ErrorInfo") + ex.Message, resourceLoader.GetString("Dialog_Title_Error"));
            }
        }

        private void ghproxy_Click(object sender, RoutedEventArgs e)
        {
            new WidgetWindow.GitHubProxyWindow().Activate();
        }

        private void SelectorBar1_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
        {
            SelectorBarItem selectedItem = sender.SelectedItem;
            int currentSelectedIndex = sender.Items.IndexOf(selectedItem);
            switch (currentSelectedIndex)
            {
                case 0:
                    BuiltinTools_Grid.Visibility = Visibility.Visible;
                    SystemTools_Grid.Visibility = Visibility.Visible;
                    ThirdPartyTools_Grid.Visibility = Visibility.Visible;
                    break;
                case 1:
                    BuiltinTools_Grid.Visibility = Visibility.Visible;
                    SystemTools_Grid.Visibility = Visibility.Collapsed;
                    ThirdPartyTools_Grid.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    BuiltinTools_Grid.Visibility = Visibility.Collapsed;
                    SystemTools_Grid.Visibility = Visibility.Visible;
                    ThirdPartyTools_Grid.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    BuiltinTools_Grid.Visibility = Visibility.Collapsed;
                    SystemTools_Grid.Visibility = Visibility.Collapsed;
                    ThirdPartyTools_Grid.Visibility = Visibility.Visible;
                    break;
                default:
                    return;
            }
        }

        private void diskinfo_Click(object sender, RoutedEventArgs e)
        {
            var resourceLoader = new ResourceLoader();
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = Path.Combine(AppContext.BaseDirectory, "ThirdPartyTools", "CrystalDiskInfo", "DiskInfo64.exe"),
                    WorkingDirectory = Path.Combine(AppContext.BaseDirectory, "ThirdPartyTools", "CrystalDiskInfo"),
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                ShowError(resourceLoader.GetString("Dialog_OpenError") + "\n" + resourceLoader.GetString("Dialog_ErrorInfo") + ex.Message, resourceLoader.GetString("Dialog_Title_Error"));
            }
        }

        private void diskmark_Click(object sender, RoutedEventArgs e)
        {
            var resourceLoader = new ResourceLoader();
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = Path.Combine(AppContext.BaseDirectory, "ThirdPartyTools", "CrystalDiskMark", "DiskMark64.exe"),
                    WorkingDirectory = Path.Combine(AppContext.BaseDirectory, "ThirdPartyTools", "CrystalDiskMark"),
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                ShowError(resourceLoader.GetString("Dialog_OpenError") + "\n" + resourceLoader.GetString("Dialog_ErrorInfo") + ex.Message, resourceLoader.GetString("Dialog_Title_Error"));
            }
        }

        private void cpuz_Click(object sender, RoutedEventArgs e)
        {
            var resourceLoader = new ResourceLoader();
            string applang = resourceLoader.GetString("App_LangCode");
            try
            {
                if (applang == "zh-CN")
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Path.Combine(AppContext.BaseDirectory, "ThirdPartyTools", "CPU_Z_CN", "cpuz_x64.exe"),
                        WorkingDirectory = Path.Combine(AppContext.BaseDirectory, "ThirdPartyTools", "CPU_Z_CN"),
                        UseShellExecute = true
                    });
                }
                else
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = Path.Combine(AppContext.BaseDirectory, "ThirdPartyTools", "CPU_Z_EN", "cpuz_x64.exe"),
                        WorkingDirectory = Path.Combine(AppContext.BaseDirectory, "ThirdPartyTools", "CPU_Z_EN"),
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                ShowError(resourceLoader.GetString("Dialog_OpenError") + "\n" + resourceLoader.GetString("Dialog_ErrorInfo") + ex.Message, resourceLoader.GetString("Dialog_Title_Error"));
            }
        }
    }
}
