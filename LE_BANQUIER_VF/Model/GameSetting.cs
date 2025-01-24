using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LE_BANQUIER_VF.Model
{
    public class GameSetting
    {
        public int MaxAmount { get; set; }
        public string PlayerName { get; set; }

        public static GameSetting Default => new GameSetting
        {
            MaxAmount = 100000,
            PlayerName = "Joueur"
        };
    }

}
