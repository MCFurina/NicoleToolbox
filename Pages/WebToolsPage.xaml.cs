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
using System.Diagnostics;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NicoleToolbox
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WebToolsPage : Page
    {
        public WebToolsPage()
        {
            InitializeComponent();
        }

        private void Map(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe","https://act.mihoyo.com/ys/app/interactive-map/index.html");
        }

        private void Calc(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe","https://act.mihoyo.com/ys/event/calculator/index.html");
        }

        private void Lineup(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe","https://webstatic.mihoyo.com/ys/event/lineup-fe/index.html");
        }

        private void Wiki(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe","https://baike.mihoyo.com/ys/obc");
        }
    }
}
