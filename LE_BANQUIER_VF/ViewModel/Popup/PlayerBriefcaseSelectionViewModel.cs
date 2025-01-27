using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Animation;

namespace LE_BANQUIER_VF.ViewModel.Popup
{
    /// <summary>
    /// View model for the player briefcase selection popup
    /// </summary>
    internal class PlayerBriefcaseSelectionViewModel : BaseViewModel
    {
        // The briefcase selected by the player
        public Briefcase Briefcase { get; set; }
        // The player, used to store the selected briefcase
        public Player Player { get; set; }
        // The list of briefcases, the selected briefcase will be removed from this list
        public ObservableCollection<Briefcase> Briefcases { get; set; }
        // Window of the dialog, use to close it and play animations
        public Window DialogWindow { get; set; }
        // Command to confirm the selection
        public RelayCommand ConfirmSelectionCommand { get; set; }
        // Command to close the window
        public RelayCommand CloseWindowCommand { get; set; }

        public PlayerBriefcaseSelectionViewModel() 
        {
            ConfirmSelectionCommand = new RelayCommand(ConfirmSelection);
            CloseWindowCommand = new RelayCommand(CloseWindow);
        }

        /// <summary>
        /// Confirm the selection of the briefcase, put the briefcase in the player's briefcase and remove it from the list and close the window while returning true with an animation
        /// </summary>
        /// <param name="parameter"></param>
        private void ConfirmSelection(object parameter)
        {
            Player.Briefcase = Briefcase;
            if(Player.Briefcase != null && Player.Briefcase == Briefcase)
            {
                if(Briefcases.Remove(Briefcase))
                {
                    var fadeOut = (Storyboard)DialogWindow.Resources["FadeOutStoryboard"];
                    fadeOut.Completed += (s, _) =>
                    {
                        DialogWindow.DialogResult = true; // Ferme la fenêtre après l'animation
                    };
                    fadeOut.Begin(DialogWindow);
                }
            }
            
        }
        /// <summary>
        /// Close the window while returning false with an animation
        /// </summary>
        /// <param name="parameter"></param>
        private void CloseWindow(object parameter)
        {
            var fadeOut = (Storyboard)DialogWindow.Resources["FadeOutStoryboard"];
            fadeOut.Completed += (s, _) =>
            {
                DialogWindow.DialogResult = !(Player.Briefcase == null); // Ferme la fenêtre après l'animation
            };
            fadeOut.Begin(DialogWindow);
        }
    }
}
