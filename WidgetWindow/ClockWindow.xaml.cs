using Microsoft.UI.Windowing;
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NicoleToolbox.WidgetWindow
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ClockWindow : Window
    {
        private DispatcherTimer _timer;

        public ClockWindow()
        {
            var resourceLoader = new ResourceLoader();
            InitializeComponent();
            this.AppWindow.Resize(new SizeInt32(600, 400));
            this.AppWindow.SetIcon("Assets/NicoleToolbox.ico");
            try
            {
                this.AppWindow.TitleBar.PreferredTheme = TitleBarTheme.UseDefaultAppMode;
            }
            catch { }
            this.AppWindow.Title = resourceLoader.GetString("ClockWindow_WindowTitle");
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) => UpdateTime();
            _timer.Start();

            UpdateTime();
        }

        private void UpdateTime()
        {
            var resourceLoader = new ResourceLoader();
            var now = DateTime.Now;
            TimeText.Text = now.ToString("HH:mm:ss");
            DateText.Text = now.ToString(resourceLoader.GetString("Date_Format"), new CultureInfo(resourceLoader.GetString("App_LangCode")));
        }

        private void TopSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (AppWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.IsAlwaysOnTop = true;
            }
        }

        private void TopSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            if (AppWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.IsAlwaysOnTop = false;
            }
        }
    }
}
