using Microsoft.Toolkit.Uwp.Helpers;
using Newtonsoft.Json;

namespace MorseCode.UWP.Classes
{
    internal class JsonObjectSerializer : IObjectSerializer
    {
        public T Deserialize<T>(object value)
        {
            return JsonConvert.DeserializeObject<T>(value.ToString());
            //return JsonSerializer.Deserialize<T>(value.ToString());
        }

        public object Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
            //return JsonSerializer.Serialize(value);
        }
    }
}