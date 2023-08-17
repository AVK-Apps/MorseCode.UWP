using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace MorseCode.UWP.UserControls
{
    public sealed partial class HeaderTile : UserControl
    {
        private SpringVector3NaturalMotionAnimation _springAnimation;

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(HeaderTile), new PropertyMetadata(null));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(HeaderTile), new PropertyMetadata(null));

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(HeaderTile), new PropertyMetadata(null));

        public string Link
        {
            get { return (string)GetValue(LinkProperty); }
            set { SetValue(LinkProperty, value); }
        }

        public static readonly DependencyProperty LinkProperty =
            DependencyProperty.Register("Link", typeof(string), typeof(HeaderTile), new PropertyMetadata(null));


        public HeaderTile()
        {
            this.InitializeComponent();
        }

        private void Element_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            //CreateOrUpdateSpringAnimation(1.1f);
            (sender as UIElement).CenterPoint = new Vector3(70, 40, 1f);
            (sender as UIElement).StartAnimation(_springAnimation);
        }

        private void Element_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            //CreateOrUpdateSpringAnimation(1.0f);
            (sender as UIElement).CenterPoint = new Vector3(70, 40, 1f);
            (sender as UIElement).StartAnimation(_springAnimation);
        }
    }
}
