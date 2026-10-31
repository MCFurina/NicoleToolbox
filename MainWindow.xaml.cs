using Microsoft.UI.Dispatching;
using Microsoft.UI.Windowing;
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
using Windows.Graphics;
using Windows.UI.ViewManagement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NicoleToolbox
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private bool _isLoaded = false;
        private UISettings? _uiSettings;
        private DispatcherQueue? _dispatcherQueue;

        public MainWindow()
        {
            InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(MainTitleBar);
            this.AppWindow.SetIcon("Assets/NicoleToolbox.ico");
            this.AppWindow.Resize(new SizeInt32(1200, 800));
            var area = DisplayArea.GetFromWindowId(AppWindow.Id, DisplayAreaFallback.Primary);
            var x = (area.WorkArea.Width - 1200) / 2;
            var y = (area.WorkArea.Height - 800) / 2;
            AppWindow.Move(new PointInt32(x, y));
            RootGrid.Loaded += RootGrid_Loaded;
            _dispatcherQueue = DispatcherQueue.GetForCurrentThread();
            _uiSettings = new UISettings();
            _uiSettings.ColorValuesChanged += OnColorValuesChanged;
            UpdateTitleBarButtonColors();
        }

        private void RootGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isLoaded) return;
            _isLoaded = true;

            DispatcherQueue.TryEnqueue(() =>
            {
                Nav.SelectedItem = Nav_HomeItem;
            });
        }

        private void Nav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (Nav.SelectedItem is not NavigationViewItem item) return;
            switch (item.Tag)
            {
                case "Home":
                    MainFrame.Navigate(typeof(Pages.HomePage));
                    break;
                case "Tools":
                    MainFrame.Navigate(typeof(Pages.ToolsPage));
                    break;
                case "About":
                    MainFrame.Navigate(typeof(Pages.AboutPage));
                    break;
            }
        }

        private void MainTitleBar_PaneToggleRequested(TitleBar sender, object args)
        {
            Nav.IsPaneOpen = !Nav.IsPaneOpen;
        }

        private void OnColorValuesChanged(UISettings sender, object args)
        {
            _dispatcherQueue?.TryEnqueue(() =>
            {
                UpdateTitleBarButtonColors();
            });
        }

        private void UpdateTitleBarButtonColors()
        {
            var titleBar = AppWindow.TitleBar;
            if (titleBar == null) return;

            if (RootGrid.ActualTheme == ElementTheme.Dark)
            {
                titleBar.ButtonForegroundColor = Microsoft.UI.Colors.White;
                titleBar.ButtonHoverForegroundColor = Microsoft.UI.Colors.White;
                titleBar.ButtonHoverBackgroundColor = Windows.UI.Color.FromArgb(255, 50, 50, 50);
                titleBar.ButtonPressedForegroundColor = Microsoft.UI.Colors.Gray;
                titleBar.ButtonPressedBackgroundColor = Windows.UI.Color.FromArgb(255, 30, 30, 30);
                titleBar.ButtonInactiveForegroundColor = Microsoft.UI.Colors.Gray;
            }
            else
            {
                titleBar.ButtonForegroundColor = Microsoft.UI.Colors.Black;
                titleBar.ButtonHoverForegroundColor = Microsoft.UI.Colors.Black;
                titleBar.ButtonHoverBackgroundColor = Windows.UI.Color.FromArgb(255, 230, 230, 230);
                titleBar.ButtonPressedForegroundColor = Microsoft.UI.Colors.Gray;
                titleBar.ButtonPressedBackgroundColor = Windows.UI.Color.FromArgb(255, 200, 200, 200);
                titleBar.ButtonInactiveForegroundColor = Microsoft.UI.Colors.Gray;
            }
        }
    }
}
