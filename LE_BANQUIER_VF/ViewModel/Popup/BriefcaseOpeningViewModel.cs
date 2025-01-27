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
    /// <summary>
    /// View model for the briefcase opening dialog
    /// </summary>
    internal class BriefcaseOpeningViewModel : BaseViewModel
    {
        // Briefcase to Open
        public Briefcase Briefcase { get; set; }
        // The window of the pop up, use to close the window and play the fade out animation
        public Window DialogWindow { get; set; }
        // List of the remaining prizes in the game, use to generate the host message
        public List<int> Prizes { get; set; }
        // Host of the game, use to assign the host message
        public Host Host { get; set; }
        // Command to open the briefcase
        public RelayCommand OpenCaseCommand { get; set; }
        // Command to close the dialog
        public RelayCommand CloseDialogCommand { get; set; }
        public BriefcaseOpeningViewModel()
        {
            OpenCaseCommand = new RelayCommand(OpenCase, CanOpenCase);
            CloseDialogCommand = new RelayCommand(CloseDialog);
        }
        /// <summary>
        /// Open the briefcase, make prize unavailable, and generate the message of the host
        /// </summary>
        /// <param name="parameter"></param>
        private void OpenCase(object parameter)
        {
            Briefcase.IsOpened = true;
            Briefcase.Prize.IsAvailable = false;
            if(!Briefcase.Prize.IsAvailable && Briefcase.IsOpened)
            {
                Host.Message = MessageGeneratorService.GetOpenBriefcaseReaction(Briefcase.Prize.Amount, Prizes);
            }
            
        }
        /// <summary>
        /// Check if the briefcase is not already opened
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanOpenCase(object parameter)
        {
            return !Briefcase.IsOpened;
        }

        /// <summary>
        /// Close the dialog and play the fade out animation
        /// </summary>
        /// <param name="parameter"></param>
        private void CloseDialog(object parameter)
        {
            var fadeOut = (Storyboard)DialogWindow.Resources["FadeOutStoryboard"];
            fadeOut.Completed += (s, _) => DialogWindow.Close();
            fadeOut.Begin(DialogWindow);
        }
    }
}
