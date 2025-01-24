using LE_BANQUIER_VF.Core;

namespace LE_BANQUIER_VF.Model
{
    public class Prize : BaseViewModel
    {
        // Amount of the prize
        private int _amount;

        // Determine if the prize is eliminated or not
        private bool _isAvailable;

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public bool IsAvailable
        {
            get => _isAvailable;
            set
            {
                _isAvailable = value;
                OnPropertyChanged();
            }
        }

    }
}
