using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Windows.ApplicationModel.WindowsAppRuntime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NicoleToolbox.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
            AppVersionLoad();
            DotnetVersionLoad();
            LoadWinAppSdkVersion();
        }

        private void AppVersionLoad()
        {
            string appversion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "未知";
            AppVersion.Text = appversion;
        }

        private void DotnetVersionLoad()
        {
            string dotnetversion = Environment.Version.ToString();
            DotnetVersion.Text = dotnetversion;
        }

        private void LoadWinAppSdkVersion()
        {
            try
            {
                var version = RuntimeInfo.Version;
                string displayVersion = $"{version.Major}.{version.Minor}.{version.Build}";

                WinAppSdkVersion.Text = displayVersion;
            }
            catch
            {
                WinAppSdkVersion.Text = "未知";
            }

        }
    }
}
