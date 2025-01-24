using LE_BANQUIER_VF.Model;
using LE_BANQUIER_VF.Service;
using LE_BANQUIER_VF.ViewModel.Popup;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Animation;


namespace LE_BANQUIER_VF.View.Popup
{
    /// <summary>
    /// Interaction logic for PlayerBriefcaseSelectionDialag.xaml
    /// </summary>
    public partial class PlayerBriefcaseSelectionDialag : Window
    {
        public PlayerBriefcaseSelectionDialag(ObservableCollection<Briefcase> Briefcases, Player player, Briefcase selectedBriefcase, Host host)
        {
            InitializeComponent();
            host.Message = MessageGeneratorService.GetBriefcaseSelectionMessage(selectedBriefcase.Number.ToString());
            var fadeIn = (Storyboard)Resources["FadeInStoryboard"];
            fadeIn.Begin(this);

            var vm = new PlayerBriefcaseSelectionViewModel();
            vm.Briefcases = Briefcases;
            vm.Player = player;
            vm.Briefcase = selectedBriefcase;
            vm.DialogWindow = this;
            DataContext = vm;
        }
    }
}
