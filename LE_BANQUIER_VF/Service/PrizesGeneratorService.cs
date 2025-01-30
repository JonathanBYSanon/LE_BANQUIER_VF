using LE_BANQUIER_VF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace LE_BANQUIER_VF.Service
{
    /// <summary>
    /// Service to generate a list of 26 prizes based on a maximum amount
    /// </summary>
    public static class PrizesGeneratorService
    {

        public static ObservableCollection<Prize> GeneratePrizes(int maxAmount)
        {
            if(!SettingService.Settings.IsSmartPrizeGeneratorEnabled)
            {
                return GenerateSimplePrizes(maxAmount);
            }
            else
            {
                return GenerateSmartPrizes(maxAmount);
            }
        }
        private static ObservableCollection<Prize> GenerateSmartPrizes(int maxAmount)
        {
            if (maxAmount <= 0)
                throw new ArgumentException("maxAmount must be greater than 0.");

            const int totalPrizes = 26;

            // Calculate key values
            int middle = CalculateMiddle(maxAmount);
            int fortyPercentMiddle = (int)(middle * 0.40);
            int fiftyPercentMiddle = (int)(middle * 0.50);
            int seventyFivePercentMiddle = (int)(middle * 0.75);
            int fortyPercentMax = (int)(maxAmount * 0.40);
            int fiftyPercentMax = (int)(maxAmount * 0.50);
            int seventyFivePercentMax = (int)(maxAmount * 0.75);

            // Initialize prizes
            var prizes = new List<int> ();

            // Region 1: From 0 to 40% of middle (10 values)
            var region1 = GenerateIncreasingIntervals(0, fortyPercentMiddle, 11);
            prizes.AddRange(region1);

            // Add fixed points in Region 1
            prizes.Add(fiftyPercentMiddle);       // 50% of middle
            prizes.Add(seventyFivePercentMiddle); // 75% of middle

            // Region 2: From middle to 40% of max (9 values)
            var region2 = GenerateIncreasingIntervals(middle, fortyPercentMax, 10);
            prizes.AddRange(region2);

            // Add fixed points in Region 2
            prizes.Add(fiftyPercentMax);       // 50% of max
            prizes.Add(seventyFivePercentMax); // 75% of max

            // Add max amount
            prizes.Add(maxAmount);

            // Validate total prizes
            if (prizes.Count != totalPrizes)
                throw new InvalidOperationException("Generated prizes do not total 26 values.");

            // Return as ObservableCollection
            return new ObservableCollection<Prize>(
                prizes.Select(amount => new Prize
                {
                    Amount = amount,
                    IsAvailable = true
                })
            );
        }


        /// <summary>
        /// Calculate the middle value based on the maximum amount
        /// </summary>
        /// <param name="maxAmount"></param>
        /// <returns></returns>
        private static int CalculateMiddle(int maxAmount)
        {
            if(maxAmount < 100)
            {
                return maxAmount * 1 / 10;
            }

            string maxStr = maxAmount.ToString();
            int middleLength = maxStr.Length / 2;

            string middleStr = maxStr[0].ToString().PadRight(middleLength+1, '0');

            return int.Parse(middleStr);
        }

        /// <summary>
        /// Generate a list of increasing intervals based on a start and end value
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static List<int> GenerateIncreasingIntervals(int start, int end, int count)
        {
            var prizes = new List<int> { start };  // S'assurer que la première valeur est le début
            double totalRange = end - start;

            // Générer des poids croissants pour assurer une distribution progressive
            double[] weights = Enumerable.Range(1, count - 1).Select(i => (double)i).ToArray();
            double totalWeight = weights.Sum();

            // Calculer les intervalles en fonction des poids
            double[] intervals = weights.Select(w => (w / totalWeight) * totalRange).ToArray();

            // Générer les valeurs de prix en arrondissant de manière adaptée
            double current = start;
            HashSet<int> uniquePrizes = new HashSet<int> { start };

            for (int i = 0; i < intervals.Length; i++)
            {
                current += intervals[i];
                int proposedValue = (int)Math.Round(current);

                // Déterminer le facteur d'arrondi en fonction de l'intervalle précédent
                int previousValue = prizes.Last();
                int roundingFactor = CalculateRoundingFactor(previousValue, proposedValue, start, end);

                // Appliquer l'arrondi tout en conservant l'ordre et les limites
                int finalValue = (int)Math.Round((double)proposedValue / roundingFactor) * roundingFactor;

                // Garantir l'unicité et respecter la borne supérieure
                if (uniquePrizes.Contains(finalValue) || finalValue > end)
                {
                    finalValue -= roundingFactor;  // Ajustement vers le bas
                    if (finalValue <= start) finalValue = start;  // Éviter de descendre sous la borne inférieure
                }

                if (finalValue < previousValue)
                {
                    finalValue = previousValue;
                }

                uniquePrizes.Add(finalValue);
                prizes.Add(finalValue);
            }

            return prizes;
        }

        /// <summary>
        /// Calculate the rounding factor based on the previous and current values
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="current"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static int CalculateRoundingFactor(int previous, int current, int start, int end)
        {
            int gap = current - previous;
            int range = end - start;

            if (gap < 10)
                return 1;
            else if (gap < 15)
                return 2;
            else if (gap < 25)
                return 5;
            else if (gap < 50)
                return 10; 
            else if (gap < 500)
                return 50;
            else if (gap < 5000)
                return 500; 
            else if (gap < 50000)
                return 5000; 
            else
                return 25000;
        }

        /// <summary>
        /// Generate a simple list of prizes based on a maximum amount with fixed pourcentages
        /// </summary>
        /// <param name="maxAmount"></param>
        /// <returns></returns>
        private static ObservableCollection<Prize> GenerateSimplePrizes(int maxAmount)
        {
            double[] pourcentages = new double[] { 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5, 6, 7, 8, 9, 10, 12.5, 15, 17.5, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            ObservableCollection<Prize> prizes = new ObservableCollection<Prize>();
            foreach (double pourcentage in pourcentages)
            {
                Prize prize = new Prize
                {
                    Amount = (int)(maxAmount * pourcentage / 100),
                    IsAvailable = true
                };
                prizes.Add(prize);
            }

            return prizes;
        }
    }
}
