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
    /// View model for the game ending dialog
    /// </summary>
    internal class GameEndingViewModel
    {
        // Game resume
        public GameResume Resume { get; set; }
        // Player chosen briefcase
        public Briefcase Briefcase { get; set; }
        // Dialog window, use to close the dialog and play the fade out animation
        public Window DialogWindow { get; set; }
        // Close command to close the dialog
        public RelayCommand CloseCommand { get; set; }
        public GameEndingViewModel()
        {
            CloseCommand = new RelayCommand(close);
        }

        /// <summary>
        /// Close the dialog, stop the game ending sound and play the fade out animation
        /// </summary>
        /// <param name="parameter"></param>
        private void close(object parameter)
        {
            var fadeOut = (Storyboard)DialogWindow.Resources["FadeOutStoryboard"];
            fadeOut.Completed += (s, _) =>
            {
                SoundManagerService.Instance.StopEffect();
                DialogWindow.Close();
            };
            fadeOut.Begin(DialogWindow);
        }

    }
}
