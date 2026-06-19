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
using NicoleToolbox.Pages;

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

            this.AppWindow.Resize(new global::Windows.Graphics.SizeInt32(1400, 800));
            CenterWindow();

            // 给导航栏自带的设置按钮设置Tag
            if (Nav.SettingsItem is NavigationViewItem settingsItem)
            {
                settingsItem.Tag = "Settings";
            }

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

        // 菜单点击导航
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
                    case "WebTools":
                        ContentFrame?.Navigate(typeof(WebToolsPage));
                        break;
                    case "Settings":
                        ContentFrame?.Navigate(typeof(SettingsPage));
                        break;
                }
            }
        }

        // 页面导航完成后 → 自动选中对应菜单
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (Nav == null || ContentFrame == null) return;

            // 1. 获取当前页面的 Type
            var currentPageType = e.SourcePageType;
            string? targetTag = null;

            // 2. 根据 Type 获取对应的 Tag 字符串
            if (currentPageType == typeof(HomePage))
            {
                targetTag = "Home";
            }
            else if (currentPageType == typeof(WebToolsPage))
            {
                targetTag = "WebTools";
            }
            else if (currentPageType == typeof(SettingsPage))
            {
                targetTag = "Settings";
            }

            // 3. 如果找到了 Tag，尝试高亮菜单
            if (targetTag != null)
            {
                bool found = false;

                // [步骤 A] 先在普通的 MenuItems 里找
                foreach (var menuItem in Nav.MenuItems)
                {
                    if (menuItem is NavigationViewItem item && item.Tag is string tag && tag == targetTag)
                    {
                        Nav.SelectedItem = item;
                        found = true;
                        break;
                    }
                }

                // [步骤 B] 如果在普通菜单没找到，并且目标就是 Settings，则选中自带的 SettingsItem
                if (!found && targetTag == "Settings")
                {
                    // 直接选中系统自带的设置项
                    Nav.SelectedItem = Nav.SettingsItem;
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

        private void CenterWindow()
        {
            IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

            // 获取屏幕工作区大小（排除任务栏）
            var displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(windowId, Microsoft.UI.Windowing.DisplayAreaFallback.Nearest);
            var workArea = displayArea.WorkArea;

            // 获取当前窗口大小
            var currentSize = appWindow.Size;

            // 计算居中位置
            int x = (workArea.Width - currentSize.Width) / 2;
            int y = (workArea.Height - currentSize.Height) / 2;

            // 移动窗口到居中位置
            appWindow.Move(new Windows.Graphics.PointInt32(x, y));
        }
    }
}