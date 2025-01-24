using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;

namespace LE_BANQUIER_VF.ViewModel.Popup
{
    internal class BriefcaseSwitchingViewModel : BaseViewModel
    {
        public Player Player { get; set; }
        public ObservableCollection<Briefcase> Briefcases { get; set; }

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
        public int LastBriefcaseIndex { get; set; }
        public Window DialogWindow { get; set; }

        public RelayCommand SwitchCaseCommand { get; set; }
        public RelayCommand CloseDialogCommand { get; set; }
        public BriefcaseSwitchingViewModel()
        {
            SwitchCaseCommand = new RelayCommand(SwitchCase, CanSwitchCase);
            CloseDialogCommand = new RelayCommand(CloseDialog);
        }
        private void SwitchCase(object parameter)
        {
            Briefcase temp = Player.Briefcase;
            Player.Briefcase = LastBriefcase;
            Briefcases[LastBriefcaseIndex] = temp;
            LastBriefcase = Briefcases[LastBriefcaseIndex];

            OnPropertyChanged(nameof(Player));
        }

        private bool CanSwitchCase(object parameter)
        {
            return LastBriefcase != null;
        }

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
