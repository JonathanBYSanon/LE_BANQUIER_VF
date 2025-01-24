using LE_BANQUIER_VF.Model;
using System.IO;
using System.Text.Json;


namespace LE_BANQUIER_VF.Service
{
    public static class SettingService
    {
        private const string SettingsFile = "settings.json";

        public static GameSetting LoadSettings()
        {
            if (File.Exists(SettingsFile))
            {
                var json = File.ReadAllText(SettingsFile);
                return JsonSerializer.Deserialize<GameSetting>(json) ?? GameSetting.Default;
            }
            return GameSetting.Default;
        }

        public static void SaveSettings(GameSetting settings)
        {
            if(settings.MaxAmount < 10)
            {
                settings.MaxAmount = 10;
            }

            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFile, json);
        }
    }
}
