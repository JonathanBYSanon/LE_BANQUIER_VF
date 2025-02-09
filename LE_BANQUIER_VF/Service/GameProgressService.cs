using LE_BANQUIER_VF.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LE_BANQUIER_VF.Service
{
    /// <summary>
    /// Service to manage the game progression
    /// </summary>
    public class GameProgressService : BaseViewModel
    {
        // Instance unique (Singleton)
        private static GameProgressService _instance = new GameProgressService();
        public static GameProgressService Instance => _instance;

        // Private constructor to avoid external instantiation
        private GameProgressService()
        {
            Reset();
        }

        // Properties for the game progression
        private int _round;
        public int Round
        {
            get => _round;
            set
            {
                if (_round != value)
                {
                    _round = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsOfferRound));
                }
            }
        }

        private int _nextOfferIndex;
        public int NextOfferIndex
        {
            get => _nextOfferIndex;
            set
            {
                if (_nextOfferIndex != value)
                {
                    _nextOfferIndex = value;
                    OnPropertyChanged(nameof(NextOfferRound));
                    OnPropertyChanged();
                    
                }
            }
        }

        public List<int> OfferRounds { get; } = new List<int> { 6, 11, 15, 18, 20, 21, 22, 23, 24 };
        public bool IsOfferRound => OfferRounds[NextOfferIndex] == Round;
        public int NextOfferRound => OfferRounds[NextOfferIndex];

        public bool isOverOnOffer = false;

        // Advance to the next round
        public void NextRound()
        {

            if (NextOfferIndex < OfferRounds.Count - 1 && Round >= OfferRounds[NextOfferIndex])
            {
                NextOfferIndex++;
            }
            Round++;

        }

        // Réinitialize the game progression
        public void Reset()
        {
            Round = 0;
            NextOfferIndex = 0;
            isOverOnOffer = false;
        }
    }
}
