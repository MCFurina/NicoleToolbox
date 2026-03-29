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
    public sealed partial class MapPage : Page
    {
        public MapPage()
        {
            InitializeComponent();
        }

        private async void Button1(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://baike.baidu.com/item/%E8%92%99%E5%BE%B7/24622024");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button2(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://baike.baidu.com/item/%E7%92%83%E6%9C%88/24626418");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button3(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://baike.baidu.com/item/%E7%A8%BB%E5%A6%BB/56127026");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button4(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://baike.baidu.com/item/%E9%A1%BB%E5%BC%A5/57962219");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button5(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://baike.baidu.com/item/%E6%9E%AB%E4%B8%B9/57960797");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button6(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://baike.baidu.com/item/%E7%BA%B3%E5%A1%94/58660278");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button7(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://baike.baidu.com/item/%E6%8C%AA%E5%BE%B7%E5%8D%A1%E8%8E%B1/65101107");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button8(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://baike.baidu.com/item/%E8%87%B3%E5%86%AC/58316607");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button9(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://baike.baidu.com/item/%E6%B8%8A%E4%B8%8B%E5%AE%AB/59729162");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button10(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://baike.baidu.com/item/%E5%B1%82%E5%B2%A9%E5%B7%A8%E6%B8%8A%C2%B7%E5%9C%B0%E4%B8%8B%E7%9F%BF%E5%8C%BA/63407280");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button11(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://baike.baidu.com/item/%E6%97%A7%E6%97%A5%E4%B9%8B%E6%B5%B7/64726303");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button12(object sender, RoutedEventArgs e)
        {
            var uri = new System.Uri("https://baike.baidu.com/item/%E8%BF%9C%E5%8F%A4%E5%9C%A3%E5%B1%B1/66265918");
            await Launcher.LaunchUriAsync(uri);
        }

        private async void Button13(object sender, RoutedEventArgs e)
        {

        }
    }
}
