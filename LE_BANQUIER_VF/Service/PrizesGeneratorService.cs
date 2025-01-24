using LE_BANQUIER_VF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace LE_BANQUIER_VF.Service
{
    public static class PrizesGeneratorService
    {
        public static ObservableCollection<Prize> GeneratePrizes(int maxAmount)
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

        // Détermine le facteur d'arrondi basé sur les écarts
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
                return 10;   // Arrondi à 10 pour les petits écarts
            else if (gap < 500)
                return 50;   // Arrondi à 50 pour les moyennes valeurs
            else if (gap < 5000)
                return 500;  // Arrondi à 500 pour les grands intervalles
            else if (gap < 50000)
                return 5000;  // Arrondi à 5000 pour les très grands écarts
            else
                return 25000; // Arrondi à 25000 pour des écarts énormes
        }
    }
}
