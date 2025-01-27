using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LE_BANQUIER_VF.Model
{
    /// <summary>
    /// Class representing the settings of the game
    /// </summary>
    public class GameSetting
    {
        // Max amount of winnable money
        public int MaxAmount { get; set; }
        public string PlayerName { get; set; }

        public static GameSetting Default => new GameSetting
        {
            MaxAmount = 1000,
            PlayerName = "Jonathan"
        };
    }

}
