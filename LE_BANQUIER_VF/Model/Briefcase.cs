using LE_BANQUIER_VF.Core;

namespace LE_BANQUIER_VF.Model
{
    /// <summary>
    /// Class representing a briefcase in the game
    /// </summary>
    public class Briefcase : BaseViewModel
    {
        // Number of the briefcase
        private int _number;

        // Prize in the briefcase
        private Prize _prize;

        // Determine if the briefcase is opened or not
        private bool _isOpened = false;

        public int Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }

        public Prize Prize
        {
            get => _prize;
            set
            {
                _prize = value;
                OnPropertyChanged();
            }
        }

        public bool IsOpened
        {
            get => _isOpened;
            set
            {
                _isOpened = value;
                OnPropertyChanged();
            }
        }
    }
}
