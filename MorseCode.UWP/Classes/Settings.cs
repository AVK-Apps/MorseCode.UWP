namespace MorseCode.UWP.Classes
{
    public class Settings
    {
        public int WordsPerMinute { get; set; }
        public double Frequency { get; set; }
        public int Speed { get; set; }
        public int FontSize { get; set; }
        public bool ClassicView { get; set; }

        public Settings()
        {
            WordsPerMinute = 20;
            Frequency = 750.0;
            Speed = 20;
            FontSize = 32;
            ClassicView = true;
        }
    }
}
