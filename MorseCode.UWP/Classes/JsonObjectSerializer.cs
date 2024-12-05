using Microsoft.Toolkit.Helpers;
using Newtonsoft.Json;

namespace MorseCode.UWP.Classes
{
    internal class JsonObjectSerializer : IObjectSerializer
    {
        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }

        string IObjectSerializer.Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}