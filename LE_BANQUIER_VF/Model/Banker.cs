using LE_BANQUIER_VF.Core;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Collections.Generic;
using LE_BANQUIER_VF.Service;
using System.Windows;
using System.Windows.Interop;

namespace LE_BANQUIER_VF.Model
{
    /// <summary>
    /// Class representing the banker in the game
    /// </summary>
    public class Banker : BaseViewModel
    {
        // The current offer of the banker
        private int _offer;

        public int Offer
        {
            get => _offer;
            set
            {
                _offer = value;
                Offers.Add(value);
                OnPropertyChanged();
            }
        }

        // List of all offers made by the banker
        private ObservableCollection<int> _offers = new ObservableCollection<int>();

        public ObservableCollection<int> Offers
        {
            get => _offers;
            set
            {
                _offers = value;
                OnPropertyChanged();
            }
        }
    }
}
