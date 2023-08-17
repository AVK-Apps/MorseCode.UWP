using Microsoft.Toolkit.Uwp.UI.Controls;
using MorseCode.UWP.Classes;
using MorseCode.UWP.UserControls;
using MorseCodeToAudio;
using System;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace MorseCode.UWP.Dialogs
{
    public sealed partial class DialogLearn : ContentDialog
    {
        public Settings Settings
        {
            get { return (Settings)GetValue(SettingsHelperProperty); }
            set { SetValue(SettingsHelperProperty, value); }
        }

        public static readonly DependencyProperty SettingsHelperProperty = DependencyProperty.Register(nameof(Settings), typeof(Settings), typeof(LearnUserControl), new PropertyMetadata(null));

        MediaPlayer mediaPlayer;
        public DialogLearn()
        {
            this.InitializeComponent();
            foreach (var item in Characters.Symbols)
            {
                MorseCodeItemsControl.Items.Add(item);
            }
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MemoryRandomAccessStream randomAccessStream = MorseHelper.GenerateMorsePlayBack((sender as Button).Tag.ToString(), Settings);
            if (randomAccessStream != null)
            {
                mediaPlayer = new MediaPlayer
                {
                    Source = MediaSource.CreateFromStream(randomAccessStream, "wav")
                };

                mediaPlayer.Play();
            }
        }

        private async void MarkdownTextBlock_LinkClicked(object sender, LinkClickedEventArgs e) => await Launcher.LaunchUriAsync(new Uri(e.Link));
    }
}
