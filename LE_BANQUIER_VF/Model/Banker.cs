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
    public class Banker : BaseViewModel
    {
        private int _offer;

        // The current offer of the banker
        public int Offer
        {
            get => _offer;
            set
            {
                _offer = value;
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

        public void MakeSmartOffer(List<int> prizes)
        {
            Offer = CalculateSmartOffer(prizes, GameProgressService.Instance.Round, 26);
            Offers.Add(Offer);
        }

        // Calculate the offer of the banker based on the average of the remaining cases
        public int CalculateSmartOffer(List<int> prizes, int round, int totalRounds)
        {
            if (prizes == null || !prizes.Any())
            {
                MessageBox.Show("Une erreur est survenue, veuillez lancer une autre partie", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                GameProgressService.Instance.Reset();
                NavigationServiceLocator.NavigationService.NavigateTo("Home");
            }

            // Base calculation using the average of the remaining prizes
            double baseOffer = prizes.Average();

            // Consider highest and lowest remaining prizes
            int highestPrize = prizes.Max();
            int lowestPrize = prizes.Min();

            // Risk factor: Increase gradually as the game progresses
            double riskFactor = round < 12 ? 0.1 :
                                round < 21 ? 0.4 :
                                0.75;  // Increase offer gradually

            // Psychological factor to simulate negotiations (random small variation)
            double psychologicalFactor = (new Random().NextDouble() * 0.2) + 0.9;  // Between 0.9 and 1.1

            // Calculate the adjusted offer
            double adjustedOffer = baseOffer * riskFactor * psychologicalFactor;

            // Ensure the offer is within the range of [lowestPrize, highestPrize * maxFactor]
            double maxFactor = round / (double)totalRounds;  // Scale max factor based on game progress
            double maxAllowedOffer = highestPrize * Math.Max(0.8, maxFactor);  // At least 80%, scale further late game
            adjustedOffer = Math.Max(adjustedOffer, lowestPrize);  // Ensure it's at least the lowest prize
            adjustedOffer = Math.Min(adjustedOffer, maxAllowedOffer);  // Cap it to a percentage of the highest prize


            // Round the offer simplicity if possible
            if (adjustedOffer > 20)
            {
                adjustedOffer = (int)Math.Round(adjustedOffer / 5.0) * 5;
            }
            else if (adjustedOffer > 100)
            {
                adjustedOffer = (int)Math.Round(adjustedOffer / 10.0) * 10;
            }
            else if (adjustedOffer > 1000)
            {
                adjustedOffer = (int)Math.Round(adjustedOffer / 50.0) * 50;
            }
            else if (adjustedOffer > 10000)
            {
                adjustedOffer = (int)Math.Round(adjustedOffer / 500.0) * 500;
            }
            else if (adjustedOffer > 100000)
            {
                adjustedOffer = (int)Math.Round(adjustedOffer / 1000.0) * 1000;
            }

            int finalOffer = (int)adjustedOffer;
            return finalOffer;
        }


    }
}
