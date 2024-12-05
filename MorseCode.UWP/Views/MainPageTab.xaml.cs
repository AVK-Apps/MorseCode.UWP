using MorseCode.UWP.Classes;
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

        private void Editor_TextChanging(TextBox sender, TextBoxTextChangingEventArgs e) => MainPageCommonEvents.Editor_TextChanging(ButtonSwitch, EditorMorse, Editor);

        private void EditorMorse_TextChanging(TextBox sender, TextBoxTextChangingEventArgs e) => MainPageCommonEvents.EditorMorse_TextChanging(ButtonSwitch, EditorMorse, Editor);

        private void EditorMorse_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args) => MainPageCommonEvents.EditorMorse_BeforeTextChanging(ButtonSwitch, sender, args);

        private void PlayPauseButton_Tapped(object sender, TappedRoutedEventArgs e) => MainPageCommonEvents.GenerateMorsePlayBack(Settings, Editor.Text);

        private void StopButton_Tapped(object sender, TappedRoutedEventArgs e) => MainPageCommonEvents.PauseMediaPlayer();

        private void Button_Click(object sender, RoutedEventArgs e) => MainPageCommonEvents.Button_Click();

        private void LearnButton_Click(object sender, RoutedEventArgs e) => MainPageCommonEvents.LearnButton_Click(Settings);        
    }
}
