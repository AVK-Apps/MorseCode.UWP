using System;
using Windows.UI.Xaml.Data;

namespace MorseCode.UWP.Converter
{
    public class StringToUpperCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string result = string.Empty;
            int charval = char.Parse(value.ToString());
            if (charval >= 97 && charval <= 122) result = " , " + value.ToString().ToUpper();
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
