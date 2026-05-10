using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using Windows.Storage;

namespace NicoleToolbox
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            // 如果用户已开启元素声音，则将开关设置为开启状态
            if (ElementSoundPlayer.State == ElementSoundPlayerState.On)
            {
                SoundToggle.IsOn = true;
            }
            else
            {
                SoundToggle.IsOn = false;
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
    }
}