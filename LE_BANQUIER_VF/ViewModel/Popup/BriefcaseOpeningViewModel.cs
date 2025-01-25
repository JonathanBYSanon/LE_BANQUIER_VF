using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.Model;
using LE_BANQUIER_VF.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace LE_BANQUIER_VF.ViewModel.Popup
{
    internal class BriefcaseOpeningViewModel : BaseViewModel
    {
        public Briefcase Briefcase { get; set; }
        public Window DialogWindow { get; set; }
        public List<int> Prizes { get; set; }
        public Host Host { get; set; }
        public RelayCommand OpenCaseCommand { get; set; }
        public RelayCommand CloseDialogCommand { get; set; }
        public BriefcaseOpeningViewModel()
        {
            OpenCaseCommand = new RelayCommand(OpenCase, CanOpenCase);
            CloseDialogCommand = new RelayCommand(CloseDialog);
        }
        private void OpenCase(object parameter)
        {
            Briefcase.IsOpened = true;
            Briefcase.Prize.IsAvailable = false;
            if(!Briefcase.Prize.IsAvailable && Briefcase.IsOpened)
            {
                Host.Message = MessageGeneratorService.GetOpenBriefcaseReaction(Briefcase.Prize.Amount, Prizes);
            }
            
        }

        private bool CanOpenCase(object parameter)
        {
            return !Briefcase.IsOpened;
        }

        private void CloseDialog(object parameter)
        {
            var fadeOut = (Storyboard)DialogWindow.Resources["FadeOutStoryboard"];
            fadeOut.Completed += (s, _) => DialogWindow.Close();
            fadeOut.Begin(DialogWindow);
        }
    }
}
