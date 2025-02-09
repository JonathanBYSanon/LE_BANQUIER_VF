using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.Service;

namespace LE_BANQUIER_VF.Model
{
    /// <summary>
    /// Class representing the settings of the game.
    /// Inherits from BaseViewModel to notify the UI of changes.
    /// </summary>
    public class GameSetting : BaseViewModel
    {
        private int _maxAmount;
        private string _playerName;
        private bool _isSoundEnabled;
        private bool _isSmartOfferEnabled;
        private bool _isSmartPrizeGeneratorEnabled;

        public int MaxAmount
        {
            get => _maxAmount;
            set
            {
                _maxAmount = value;
                OnPropertyChanged();
            }
        }

        public string PlayerName
        {
            get => _playerName;
            set
            {
                _playerName = value;
                OnPropertyChanged();
            }
        }

        public bool IsSoundEnabled
        {
            get => _isSoundEnabled;
            set
            {
                _isSoundEnabled = value;
                SoundManagerService.IsMuted = !_isSoundEnabled;

                OnPropertyChanged();
            }
        }

        public bool IsSmartOfferEnabled
        {
            get => _isSmartOfferEnabled;
            set
            {
                _isSmartOfferEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool IsSmartPrizeGeneratorEnabled
        {
            get => _isSmartPrizeGeneratorEnabled;
            set
            {
                _isSmartPrizeGeneratorEnabled = value;
                OnPropertyChanged();
            }
        }

        public GameSetting()
        {
            MaxAmount = 1000;
            PlayerName = "Etudiant";
            IsSoundEnabled = true;
            IsSmartOfferEnabled = true;
            IsSmartPrizeGeneratorEnabled = false;
        }
    }
}
