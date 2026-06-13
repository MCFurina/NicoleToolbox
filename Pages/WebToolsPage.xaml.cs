using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using NicoleToolbox.Pages.Windows;
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

namespace NicoleToolbox.Pages
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

        // 切换逻辑
        private void selectorbar_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
        {
            SelectorBarItem selectedItem = sender.SelectedItem;
            int currentSelectedIndex = sender.Items.IndexOf(selectedItem);

            switch (currentSelectedIndex)
            {
                case 0:
                    grid0.Visibility = Visibility.Visible;
                    grid1.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    grid1.Visibility = Visibility.Visible;
                    grid0.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        // 原神工具
        private void Map(object sender, RoutedEventArgs e)
        {
            GIMapWindow mapWindow = new GIMapWindow();
            mapWindow.Activate();
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

        // Windows工具
        private void winver(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "C:\\Windows\\System32\\winver.exe");
        }

        private void systeminfo(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "ms-settings:about");
        }

        private void dxdiag(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "C:\\Windows\\System32\\dxdiag.exe");
        }

        private void msinfo32(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "C:\\Windows\\System32\\msinfo32.exe");
        }

        private void taskmgr(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "C:\\Windows\\System32\\Taskmgr.exe");
        }
    }
}
