using MorseCode.UWP.Classes;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace MorseCode.UWP.UserControls
{
    public sealed partial class HeaderTile : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(HeaderTile), new PropertyMetadata(string.Empty));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(nameof(Description), typeof(string), typeof(HeaderTile), new PropertyMetadata(string.Empty));

        public HeaderTile()
        {
            InitializeComponent();
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MemoryRandomAccessStream randomAccessStream = MorseHelper.GenerateMorsePlayBack((sender as Button).Tag.ToString(), App.Settings);
            if (randomAccessStream != null)
            {
                MediaPlayer mediaPlayer = new MediaPlayer
                {
                    Source = MediaSource.CreateFromStream(randomAccessStream, "wav")
                };
                mediaPlayer.Play();
            }
        }
    }
}
