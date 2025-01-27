using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace LE_BANQUIER_VF.ViewModel.Popup
{
    /// <summary>
    /// ViewModel for the offer receiving popup
    /// </summary>
    internal class OfferReceivingViewModel
    {
        // Banker of the game
        public Banker Banker { get; set; }
        // Dialog window, use to close the window and play the fade out animation
        public Window DialogWindow { get; set; }
        // Command to accept the offer
        public RelayCommand AcceptOfferCommand { get; set; }
        // Command to refuse the offer
        public RelayCommand RefuseOfferCommand { get; set; }
        public OfferReceivingViewModel()
        {
            AcceptOfferCommand = new RelayCommand(AcceptOffer);
            RefuseOfferCommand = new RelayCommand(RefuseOffer);
        }
        /// <summary>
        /// Accept the offer and return true to close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void AcceptOffer(object parameter)
        {
            var fadeOut = (Storyboard)DialogWindow.Resources["FadeOutStoryboard"];
            fadeOut.Completed += (s, _) =>
            {
                DialogWindow.DialogResult = true; // Ferme la fenêtre après l'animation
            };
            fadeOut.Begin(DialogWindow);
        }
        /// <summary>
        /// Refuse the offer and return false to close the window
        /// </summary>
        /// <param name="parameter"></param>
        private void RefuseOffer(object parameter)
        {
            var fadeOut = (Storyboard)DialogWindow.Resources["FadeOutStoryboard"];
            fadeOut.Completed += (s, _) =>
            {
                DialogWindow.DialogResult = false; // Ferme la fenêtre après l'animation
            };
            fadeOut.Begin(DialogWindow);
        }
    }
}
