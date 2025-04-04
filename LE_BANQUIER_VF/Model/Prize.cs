﻿using LE_BANQUIER_VF.Core;

namespace LE_BANQUIER_VF.Model
{
    /// <summary>
    /// Class representing a winnable prize in the game, from MaxAmount to 0
    /// </summary>
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
