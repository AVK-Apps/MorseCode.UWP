using MorseCode.UWP.Classes;
using MorseCode.UWP.Dialogs;
using MorseCodeToAudio;
using System;
using System.Linq;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace MorseCode.UWP.Views
{
    public class MainPageCommonEvents
    {
        public static MediaPlayer Player { get; set; }

        public static async void Button_Click() => await new DialogAbout().ShowAsync();

        public static async void LearnButton_Click(Settings settings) => await new DialogLearn() { Settings = settings }.ShowAsync();

        public static void PauseMediaPlayer()
        {
            if (Player != null && Player.PlaybackSession.CanPause)
            {
                Player.Pause();
            }
        }

        public static void GenerateMorsePlayBack(Settings settings, string Text)
        {
            MemoryRandomAccessStream randomAccessStream = MorseHelper.GenerateMorsePlayBack(Text, settings);
            if (randomAccessStream != null)
            {
                Player = new MediaPlayer
                {
                    Source = MediaSource.CreateFromStream(randomAccessStream, "wav")
                };

                Player.Play();
            }
        }

        public static void EditorMorse_BeforeTextChanging(ToggleButton ButtonSwitch, TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (!bool.Parse(ButtonSwitch.IsChecked.ToString())) return;
            char[] stringdata = new char[] { '.', '-', ' ' };
            bool found = false;
            foreach (char s in args.NewText)
            {
                found = stringdata.Contains(s);
            }
            if (string.IsNullOrEmpty(args.NewText)) found = true;
            if (!found)
            {
                args.Cancel = true;
            }
        }

        public static void EditorMorse_TextChanging(ToggleButton ButtonSwitch, TextBox EditorMorse, TextBox Editor)
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

        public static  void Editor_TextChanging(ToggleButton ButtonSwitch, TextBox EditorMorse, TextBox Editor)
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
    }
}
