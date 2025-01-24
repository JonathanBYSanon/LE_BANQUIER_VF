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
    public class GameProgressService : BaseViewModel
    {
        // Instance unique (Singleton)
        private static GameProgressService _instance = new GameProgressService();
        public static GameProgressService Instance => _instance;

        // Constructeur privé pour empêcher la création d'autres instances
        private GameProgressService()
        {
            Reset();
        }

        // Propriétés liées à la progression du jeu
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

        // Avancer d'un round
        public void NextRound()
        {
            
            if (NextOfferIndex < OfferRounds.Count-1 && Round >= OfferRounds[NextOfferIndex])
            {
                NextOfferIndex++;
            }
            Round++;

        }

        // Vérifier si le round actuel est un round d'offre
        public bool IsOfferRound => OfferRounds[NextOfferIndex] == Round;
        public int NextOfferRound => OfferRounds[NextOfferIndex];

        // Réinitialiser la progression
        public void Reset()
        {
            Round = 0;
            NextOfferIndex = 0;
        }
    }
}
