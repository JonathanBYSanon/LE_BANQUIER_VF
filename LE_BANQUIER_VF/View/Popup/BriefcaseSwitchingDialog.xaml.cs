using LE_BANQUIER_VF.Model;
using LE_BANQUIER_VF.Service;
using LE_BANQUIER_VF.ViewModel.Popup;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;


namespace LE_BANQUIER_VF.View.Popup
{
    /// <summary>
    /// Interaction logic for BriefcaseSwitchingDialog.xaml
    /// </summary>
    public partial class BriefcaseSwitchingDialog : Window
    {
        public BriefcaseSwitchingDialog(Player player, ObservableCollection<Briefcase> briefcases, Host host)
        {
            InitializeComponent();
            var fadeIn = (Storyboard)Resources["FadeInStoryboard"];
            fadeIn.Begin(this);

            host.Message = MessageGeneratorService.GetSwitchBriefcaseMessage();

            var vm = new BriefcaseSwitchingViewModel();
            vm.Player = player;
            vm.Briefcases = briefcases;
            vm.LastBriefcase = briefcases.FirstOrDefault(b => !b.IsOpened);
            vm.LastBriefcaseIndex = briefcases.IndexOf(vm.LastBriefcase);
            vm.DialogWindow = this;
            DataContext = vm;

        }
    }
}
