using MorseCodeToAudio;

namespace MorseCode.UWP.Classes
{
    public class MorseHelper
    {
        public static MemoryRandomAccessStream GenerateMorsePlayBack(string Text, Settings Settings)
        {
            TextToMorse converter = new TextToMorse(Settings.Speed, Settings.WordsPerMinute, Settings.Frequency);
            byte[] outfile = converter.ConvertToMorse(Text);
            MemoryRandomAccessStream randomAccessStream = new MemoryRandomAccessStream(outfile);
            return randomAccessStream;
        }
    }
}
