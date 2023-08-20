using MorseCode.UWP.Classes;
using MorseCode.UWP.Dialogs;
using MorseCodeToAudio;
using System;
using System.Linq;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace MorseCode.UWP.Views
{
    public sealed partial class MainPageTab : Page
    {
        public Settings Settings { get; set; }
        public MainPageTab()
        {
            InitializeComponent();
            Settings = App.Settings;
        }

        private void Editor_TextChanging(TextBox sender, TextBoxTextChangingEventArgs e)
        {
            if (bool.Parse(ButtonSwitch.IsChecked.ToString())) return;

            PauseMediaPlayer();
            string newval = string.Empty;
            string text = Editor.Text;

            for (int i = 0; i < text.Length; i++)
            {
                string value = Characters.Symbols.FirstOrDefault(x => x.Key == text[i].ToString().ToLower()).Value;
                newval += " " + value;
            }
            EditorMorse.Text = newval;
        }

        private void EditorMorse_TextChanging(TextBox sender, TextBoxTextChangingEventArgs e)
        {
            if (!bool.Parse(ButtonSwitch.IsChecked.ToString())) return;

            PauseMediaPlayer();
            string newval = string.Empty;
            string text = EditorMorse.Text;

            string[] data = text.Split(" ");
            foreach (string s in data)
            {
                string myKey = Characters.Symbols.FirstOrDefault(x => x.Value == s).Key;
                newval += " " + myKey;
            }
            Editor.Text = newval;
        }

        void PauseMediaPlayer()
        {
            if (mediaPlayer != null)
            {
                if (mediaPlayer.PlaybackSession.CanPause)
                {
                    mediaPlayer.Pause();
                }
            }
        }

        private void SwitchButton_Checked(object sender, RoutedEventArgs e)
        {
            Grid.SetRow(Editor, 1);
            Grid.SetRow(MorseGrid, 0);
        }

        private void SwitchButton_Unchecked(object sender, RoutedEventArgs e)
        {
            Grid.SetRow(Editor, 0);
            Grid.SetRow(MorseGrid, 1);
        }

        private void EditorMorse_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (!bool.Parse(ButtonSwitch.IsChecked.ToString())) return;
            char[] stringdata = new char[] { '.', '-', ' ' };
            bool found = false;
            foreach(char s in args.NewText)
            {
                found = stringdata.Contains(s);
            }
            if (string.IsNullOrEmpty(args.NewText)) found = true;
            if (!found)
            {
                args.Cancel = true;
            }
        }

        MediaPlayer mediaPlayer;
        private void PlayPauseButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            GenerateMorsePlayBack(Editor.Text);
        }

        private void GenerateMorsePlayBack(string Text)
        {
            MemoryRandomAccessStream randomAccessStream = MorseHelper.GenerateMorsePlayBack(Text, Settings);
            if (randomAccessStream != null)
            {
                mediaPlayer = new MediaPlayer
                {
                    Source = MediaSource.CreateFromStream(randomAccessStream, "wav")
                };

                mediaPlayer.Play();
            }
        }

        private void StopButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PauseMediaPlayer();
        }

        private async void LearnButton_Click(object sender, RoutedEventArgs e) => await new DialogLearn() { Settings = Settings }.ShowAsync();

        private async void Button_Click(object sender, RoutedEventArgs e) => await new DialogAbout().ShowAsync();
    }
}
