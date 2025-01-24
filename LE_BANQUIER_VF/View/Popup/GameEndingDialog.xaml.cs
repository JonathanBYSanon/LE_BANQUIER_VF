using LE_BANQUIER_VF.Model;
using LE_BANQUIER_VF.Service;
using LE_BANQUIER_VF.ViewModel.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LE_BANQUIER_VF.View.Popup
{
    /// <summary>
    /// Interaction logic for GameEndingDialog.xaml
    /// </summary>
    public partial class GameEndingDialog : Window
    {
        public GameEndingDialog(GameResume gameResume, Host host, Briefcase briefcase)
        {
            InitializeComponent();
            var fadeIn = (Storyboard)Resources["FadeInStoryboard"];
            fadeIn.Begin(this);

            host.Message = MessageGeneratorService.GetEndGameMessage(gameResume.FinalPrize,gameResume.HighestPrizeRemaining, briefcase.Prize.Amount, gameResume.LastOffer);

            var vm = new GameEndingViewModel();
            vm.Resume = gameResume;
            vm.Briefcase = briefcase;
            vm.DialogWindow = this;
            DataContext = vm;
        }
    }
}
