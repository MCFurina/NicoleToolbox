using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NicoleToolbox
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MorePage : Page
    {
        public MorePage()
        {
            InitializeComponent();
        }

        private async void Button1(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://philia093.xyz/");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button2(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://www.bettergi.com/");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button3(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://v3.yuanshen.site/");
            await Launcher.LaunchUriAsync(uri);
        }
    }
}
