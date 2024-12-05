using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Threading.Tasks;

namespace MorseCode.UWP.Classes
{
    public class SettingsHelper
    {
        private static readonly string SettingsFileName = "SettingsValue";
        private static readonly ApplicationDataStorageHelper ApplicationDataStorageHelper = ApplicationDataStorageHelper.GetCurrent(new JsonObjectSerializer());
        public Settings Settings { get; set; }

        public static async Task<Settings> GetSettings()
        {
            Settings result;
            try
            {
                result = await ApplicationDataStorageHelper.ReadFileAsync<Settings>(SettingsFileName);
            }
            catch (Exception)
            {
                result = new Settings();
            }
            if (result == null)
            {
                result = new Settings();
            }

            return result;
        }

        public static async void SaveSettings(Settings settings)
        {
            if (settings != null)
            {
                await ApplicationDataStorageHelper.CreateFileAsync(SettingsFileName, settings);
            }
        }
    }
}
