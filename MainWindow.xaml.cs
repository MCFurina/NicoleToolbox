using ABI.System;
using Microsoft.UI;
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
using Windows.System;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NicoleToolbox
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/Logo.ico"));
            Title = "尼可工具箱";

            // 安全绑定导航点击事件（防止控件为 null）
            if (Nav != null)
            {
                Nav.ItemInvoked += Nav_ItemInvoked;

                if (Nav.MenuItems?.Count > 0)
                {
                    Nav.SelectedItem = Nav.MenuItems[0];
                }
            }

            // 绑定 Frame 导航事件 → 页面变化时自动更新菜单选中状态
            if (ContentFrame != null)
            {
                ContentFrame.Navigated += ContentFrame_Navigated;
                // 默认打开“Home”
                ContentFrame.Navigate(typeof(HomePage));
            }

            ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(titleBar);
            var root = Content as FrameworkElement;
            if (root != null)
            {
                root.ActualThemeChanged += Root_ActualThemeChanged;
                UpdateTitleBar(root.ActualTheme);
            }
        }

        /// <summary>
        /// 菜单点击导航
        /// </summary>
        private void Nav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args?.InvokedItemContainer is NavigationViewItem item && item.Tag is string tag)
            {
                // 根据 Tag 切换页面
                switch (tag)
                {
                    case "Home":
                        ContentFrame?.Navigate(typeof(HomePage));
                        break;
                    case "Map":
                        ContentFrame?.Navigate(typeof(MapPage));
                        break;
                    case "WebMap":
                        ContentFrame?.Navigate(typeof(WebMapPage));
                        break;
                    case "Calc":
                        ContentFrame?.Navigate(typeof(CalcPage));
                        break;
                    case "Wiki":
                        ContentFrame?.Navigate(typeof(WikiPage));
                        break;
                    case "Lineup":
                        ContentFrame?.Navigate(typeof(LineupPage));
                        break;
                    case "Enka":
                        ContentFrame?.Navigate(typeof(EnkaPage));
                        break;
                    case "GameAnnouncement":
                        ContentFrame?.Navigate(typeof(GameAnnouncementPage));
                        break;
                    case "More":
                        ContentFrame?.Navigate(typeof(MorePage));
                        break;
                    case "About":
                        ContentFrame?.Navigate(typeof(AboutPage));
                        break;
                }
            }
        }

        /// <summary>
        /// 页面导航完成后 → 自动选中对应菜单
        /// </summary>
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (Nav == null || ContentFrame == null) return;

            // 获取当前显示的页面类型
            System.Type currentPageType = e.SourcePageType;

            // 根据页面类型匹配菜单项
            foreach (var menuItem in Nav.MenuItems)
            {
                if (menuItem is NavigationViewItem item && item.Tag is string tag)
                {
                    // 页面类型与 Tag 匹配 → 选中该项
                    bool isMatch = currentPageType switch
                    {
                        System.Type t when t == typeof(HomePage) => tag == "Home",
                        System.Type t when t == typeof(MapPage) => tag == "Map",
                        System.Type t when t == typeof(WebMapPage) => tag == "WebMap",
                        System.Type t when t == typeof(CalcPage) => tag == "Calc",
                        System.Type t when t == typeof(WikiPage) => tag == "Wiki",
                        System.Type t when t == typeof(LineupPage) => tag == "Lineup",
                        System.Type t when t == typeof(EnkaPage) => tag == "Enka",
                        System.Type t when t == typeof(GameAnnouncementPage) => tag == "GameAnnouncement",
                        System.Type t when t == typeof(MorePage) => tag == "More",
                        System.Type t when t == typeof(AboutPage) => tag == "About",
                        _ => false
                    };

                    if (isMatch)
                    {
                        Nav.SelectedItem = item;
                        break;
                    }
                }
            }
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