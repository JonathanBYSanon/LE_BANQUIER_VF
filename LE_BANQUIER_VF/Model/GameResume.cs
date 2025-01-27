using LE_BANQUIER_VF.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LE_BANQUIER_VF.Model
{
    /// <summary>
    /// Class representing the game resume
    /// </summary>
    public class GameResume
    {
        // Player details
        public string PlayerName { get; set; }
        public int ChosenBriefcase { get; set; }

        // Banker details
        public List<int> BankerOffers { get; set; }
        public int HighestOffer { get; set; }

        public int LastOffer => BankerOffers.Any() ? BankerOffers.Last() : 0;

        // Game details
        public int FinalPrize { get; set; }
        public List<int> PrizesEliminated { get; set; }
        public List<int> PrizesRemaining{ get; set; }
        public int LowestPrizeRemaining { get; set; }
        public int HighestPrizeRemaining { get; set; }
        public int TotalRoundsPlayed { get; set; }

        // Constructor
        public GameResume(Player player, Banker banker, ObservableCollection<Prize> prizes)
        {
            // Initialize player details
            PlayerName = player.Name;
            ChosenBriefcase = player.Briefcase.Number;

            // Initialize banker details
            BankerOffers = banker.Offers.ToList<int>(); // Copy list of offers
            HighestOffer = banker.Offers.Any() ? banker.Offers.Max() : 0;

            // Initialize game details
            PrizesEliminated = prizes
                .Where(p => !p.IsAvailable)
                .Select(p => p.Amount)
                .ToList();

            PrizesRemaining = prizes
                .Where(p => p.IsAvailable)
                .Select(p => p.Amount)
                .ToList();

            HighestPrizeRemaining = PrizesRemaining.Any() ? PrizesRemaining.Max() : 0;
            LowestPrizeRemaining = PrizesRemaining.Any() ? PrizesRemaining.Min() : 0;

            // Access round through GameProgress
            TotalRoundsPlayed = GameProgressService.Instance.Round;

            // Determine final prize based on the round
            FinalPrize = GameProgressService.Instance.Round < 25
                ? BankerOffers.Any() ? BankerOffers.Last() : 0
                : player.Briefcase.Prize.Amount;
        }
    }
}
