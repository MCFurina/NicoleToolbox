using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.ComponentModel;
using System.Diagnostics;
using Windows.Storage;

namespace NicoleToolbox
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            // 同步当前主题设置到ComboBox
            if (App.MainWindow?.Content is FrameworkElement rootElement)
            {
                switch (rootElement.RequestedTheme)
                {
                    case ElementTheme.Light:
                        ColorModeComboBox.SelectedItem = ColorModeComboBox.Items[0];
                        break;
                    case ElementTheme.Dark:
                        ColorModeComboBox.SelectedItem = ColorModeComboBox.Items[1];
                        break;
                    case ElementTheme.Default:
                        ColorModeComboBox.SelectedItem = ColorModeComboBox.Items[2];
                        break;
                }
            }

            // 如果用户已开启元素声音，则将开关设置为开启状态
            if (ElementSoundPlayer.State == ElementSoundPlayerState.On)
            {
                SoundToggle.IsOn = true;
            }
            else
            {
                SoundToggle.IsOn = false;
            }

            // 同步当前导航栏位置设置到ComboBox
            if (App.MainWindow != null)
            {
                switch (App.MainWindow.Nav.PaneDisplayMode)
                {
                    case NavigationViewPaneDisplayMode.Left:
                        NavComboBox.SelectedItem = NavComboBox.Items[0];
                        break;
                    case NavigationViewPaneDisplayMode.Top:
                        NavComboBox.SelectedItem = NavComboBox.Items[1];
                        break;
                }
            }
        }

        private void SelectorBar_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
        {
            grid0.Visibility = Visibility.Collapsed;
            grid1.Visibility = Visibility.Collapsed;
            SelectorBarItem selectedItem = sender.SelectedItem;
            int currentSelectedIndex = sender.Items.IndexOf(selectedItem);

            switch (currentSelectedIndex)
            {
                case 0:
                    grid0.Visibility = Visibility.Visible;
                    break;
                case 1:
                    grid1.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void ColorModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (App.MainWindow?.Content is FrameworkElement rootElement)
            {
                // 根据ComboBox选中的值，设置RequestedTheme
                if (ColorModeComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    switch (selectedItem.Content.ToString())
                    {
                        case "浅色":
                            rootElement.RequestedTheme = ElementTheme.Light;
                            break;
                        case "深色":
                            rootElement.RequestedTheme = ElementTheme.Dark;
                            break;
                        case "跟随系统":
                            rootElement.RequestedTheme = ElementTheme.Default;
                            break;
                    }
                }
            }
        }

        private void SoundToggle_Toggled(object sender, RoutedEventArgs e)
        {
            if (SoundToggle.IsOn)
            {
                ElementSoundPlayer.State = ElementSoundPlayerState.On;
            }
            else
            {
                ElementSoundPlayer.State = ElementSoundPlayerState.Off;
            }
        }

        private void NavComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "左侧":
                        App.MainWindow?.Nav.PaneDisplayMode = NavigationViewPaneDisplayMode.Left;
                        break;
                    case "顶部":
                        App.MainWindow?.Nav.PaneDisplayMode = NavigationViewPaneDisplayMode.Top;
                        break;
                }
            }
        }

        // About Page
        private void GitHub(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/MCFurina/NicoleToolbox/");
        }

        private void Issue(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/MCFurina/NicoleToolbox/issues/");
        }

        private void Contributors(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ContributorsPage));
        }

        private void Website(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", "https://mcfurina.github.io/NicoleToolbox/");
        }
    }
}