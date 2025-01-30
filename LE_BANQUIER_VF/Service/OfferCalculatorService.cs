using LE_BANQUIER_VF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LE_BANQUIER_VF.Service
{
    /// <summary>
    /// Service to calculate the smart offer based on the remaining prizes and the game progress
    /// </summary>
    public static class OfferCalculatorService
    {
        public static int CalculateOffer(List<int> prizes)
        {
            if (!SettingService.Settings.IsSmartOfferEnabled)
            {
                return CalculateSimpleOffer(prizes);
            }
            else
            {
                return CalculateSmartOffer(prizes);
            }
        }
        private static int CalculateSmartOffer(List<int> prizes)
        {
            int round = GameProgressService.Instance.Round;
            int totalRounds = 26;

            if (prizes == null || !prizes.Any() || !GameProgressService.Instance.IsOfferRound)
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


        /// <summary>
        /// Calculate a simple offer based on the average of the remaining prizes
        /// </summary>
        /// <param name="prizes"></param>
        /// <returns></returns>
        private static int CalculateSimpleOffer(List<int> prizes)
        {
            if (prizes == null || !prizes.Any() || !GameProgressService.Instance.IsOfferRound)
            {
                MessageBox.Show("Une erreur est survenue, veuillez lancer une autre partie", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                GameProgressService.Instance.Reset();
                NavigationServiceLocator.NavigationService.NavigateTo("Home");
            }

            //Average of all the remaining prizes
            double offer = prizes.Average();

            return (int)offer;
        }

    }
}
