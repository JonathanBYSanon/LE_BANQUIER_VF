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
    internal class OfferReceivingViewModel
    {
        public Banker Banker { get; set; }
        public Window DialogWindow { get; set; }

        public RelayCommand AcceptOfferCommand { get; set; }
        public RelayCommand RefuseOfferCommand { get; set; }
        public OfferReceivingViewModel()
        {
            AcceptOfferCommand = new RelayCommand(AcceptOffer);
            RefuseOfferCommand = new RelayCommand(RefuseOffer);
        }
        private void AcceptOffer(object parameter)
        {
            var fadeOut = (Storyboard)DialogWindow.Resources["FadeOutStoryboard"];
            fadeOut.Completed += (s, _) =>
            {
                DialogWindow.DialogResult = true; // Ferme la fenêtre après l'animation
            };
            fadeOut.Begin(DialogWindow);
        }
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
