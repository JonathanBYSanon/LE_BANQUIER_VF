using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Animation;

namespace LE_BANQUIER_VF.ViewModel.Popup
{
    internal class PlayerBriefcaseSelectionViewModel : BaseViewModel
    {
        public Briefcase Briefcase { get; set; }
        public Player Player { get; set; }
        public ObservableCollection<Briefcase> Briefcases { get; set; }
        public Window DialogWindow { get; set; }
        public RelayCommand ConfirmSelectionCommand { get; set; }
        public RelayCommand CloseWindowCommand { get; set; }

        public PlayerBriefcaseSelectionViewModel() 
        {
            ConfirmSelectionCommand = new RelayCommand(ConfirmSelection);
            CloseWindowCommand = new RelayCommand(CloseWindow);
        }

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
