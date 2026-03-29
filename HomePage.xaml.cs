using FufuLauncher.Views;
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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NicoleToolbox
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void GoMapPage (object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MapPage));
        }

        private void GoWebMapPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WebMapPage));
        }

        private void GoCalcPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CalcPage));
        }

        private void GoWikiPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WikiPage));
        }

        private void GoLineupPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LineupPage));
        }

        private void GoEnkaPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EnkaPage));
        }

        private void GoGameAnnouncementPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GameAnnouncementPage));
        }

        private void GoMorePage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MorePage));
        }

        private void OpenVideoWindow(object sender, RoutedEventArgs e)
        {
            VideoResourcesWindow videoWindow = new VideoResourcesWindow();
            videoWindow.Activate();
        }
    }
}
