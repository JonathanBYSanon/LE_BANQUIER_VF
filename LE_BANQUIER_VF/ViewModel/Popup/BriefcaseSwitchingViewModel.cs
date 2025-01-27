using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;

namespace LE_BANQUIER_VF.ViewModel.Popup
{
    /// <summary>
    /// View model for the briefcase switching dialog
    /// </summary>
    internal class BriefcaseSwitchingViewModel : BaseViewModel
    {
        // The player, use to access the player's briefcase
        public Player Player { get; set; }
        // List the briefcases, and use to access the briefcase that the player can switch to
        public ObservableCollection<Briefcase> Briefcases { get; set; }
        // The last briefcase that the player can switch to, found in the list of briefcases
        private Briefcase _lastBriefcase;
        public Briefcase LastBriefcase
        {
            get => _lastBriefcase;
            set
            {
                _lastBriefcase = value;
                OnPropertyChanged();
            }
        }
        // The index of the last briefcase in the list of briefcases, use to access the briefcase in the list quicker
        public int LastBriefcaseIndex { get; set; }
        // The dialog window, use to close the dialog window and play the fade out animation
        public Window DialogWindow { get; set; }
        // The command to switch the briefcase
        public RelayCommand SwitchCaseCommand { get; set; }
        // The command to close the dialog window
        public RelayCommand CloseDialogCommand { get; set; }
        public BriefcaseSwitchingViewModel()
        {
            SwitchCaseCommand = new RelayCommand(SwitchCase, CanSwitchCase);
            CloseDialogCommand = new RelayCommand(CloseDialog);
        }
        /// <summary>
        /// Switch the player's briefcase with the last briefcase in the list of briefcases
        /// </summary>
        /// <param name="parameter"></param>
        private void SwitchCase(object parameter)
        {
            Briefcase temp = Player.Briefcase;
            Player.Briefcase = LastBriefcase;
            Briefcases[LastBriefcaseIndex] = temp;
            LastBriefcase = Briefcases[LastBriefcaseIndex];

            OnPropertyChanged(nameof(Player));
        }
        /// <summary>
        /// Check if the player can switch the briefcase
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanSwitchCase(object parameter)
        {
            return LastBriefcase != null;
        }
        /// <summary>
        /// Close the dialog window and play the fade out animation
        /// </summary>
        /// <param name="parameter"></param>
        private void CloseDialog(object parameter)
        {
            if (Player.Briefcase != null && Briefcases[LastBriefcaseIndex] != null && Player.Briefcase != Briefcases[LastBriefcaseIndex])
            {
                var fadeOut = (Storyboard)DialogWindow.Resources["FadeOutStoryboard"];
                fadeOut.Completed += (s, _) => DialogWindow.Close();
                fadeOut.Begin(DialogWindow);
            } 
        }
    }
}
