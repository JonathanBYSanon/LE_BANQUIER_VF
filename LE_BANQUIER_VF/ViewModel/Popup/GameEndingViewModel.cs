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
    internal class GameEndingViewModel
    {
        public GameResume Resume { get; set; }
        public Briefcase Briefcase { get; set; }
        public Window DialogWindow { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public GameEndingViewModel()
        {
            CloseCommand = new RelayCommand(close);
        }
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
