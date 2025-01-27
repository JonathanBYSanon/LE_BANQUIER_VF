using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LE_BANQUIER_VF.Service
{
    /// <summary>
    /// Service to generate the 26 briefcases with shuffled prizes
    /// </summary>
    public static class BriefcasesGeneratorService
    {
        public static ObservableCollection<Briefcase> GenerateBriefcases(ObservableCollection<Prize> prizes)
        {
            if (prizes == null || prizes.Count != 26)
            {
                MessageBox.Show("Une erreur s'est produite, veuillez relancer un nouveau jeu.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                GameProgressService.Instance.Reset();
                NavigationServiceLocator.NavigationService.NavigateTo("Home");
            }

            // Shuffle prizes using Fisher-Yates shuffle
            var shuffledPrizes = FisherYatesShuffle(prizes.ToList());

            // Create 26 briefcases and assign shuffled prizes
            var briefcases = new ObservableCollection<Briefcase>();
            for (int i = 1; i <= 26; i++)
            {
                briefcases.Add(new Briefcase
                {
                    Number = i,
                    Prize = shuffledPrizes[i - 1],
                    IsOpened = false
                });
            }

            return briefcases;
        }

        // Fisher-Yates Shuffle Algorithm
        private static List<Prize> FisherYatesShuffle(List<Prize> prizes)
        {
            Random rng = new Random();
            for (int i = prizes.Count - 1; i > 0; i--)
            {
                int j = rng.Next(0, i + 1);  // Random index between 0 and i (inclusive)
                (prizes[i], prizes[j]) = (prizes[j], prizes[i]);  // Swap elements
            }
            return prizes;
        }

    }
}
