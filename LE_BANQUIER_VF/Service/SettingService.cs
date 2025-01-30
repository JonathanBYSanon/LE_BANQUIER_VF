using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.Model;

namespace LE_BANQUIER_VF.Service
{
    /// <summary>
    /// Static class for managing game settings.
    /// </summary>
    public class SettingService
    {
        public static GameSetting Settings { get; set; } = new GameSetting();

    }
}
