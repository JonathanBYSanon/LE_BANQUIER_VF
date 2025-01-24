using LE_BANQUIER_VF.Model;
using LE_BANQUIER_VF.Service;
using LE_BANQUIER_VF.ViewModel.Popup;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;

namespace LE_BANQUIER_VF.View.Popup
{
    /// <summary>
    /// Interaction logic for BriefcaseOpeningDialog.xaml
    /// </summary>
    public partial class BriefcaseOpeningDialog : Window
    {
        public BriefcaseOpeningDialog(Briefcase selectedBriefcase, Host host, List<int> prizes)
        {
            InitializeComponent();
            var fadeIn = (Storyboard)Resources["FadeInStoryboard"];
            fadeIn.Begin(this);

            host.Message = MessageGeneratorService.GetOpenBriefcaseMessage(selectedBriefcase.Number.ToString());

            var vm = new BriefcaseOpeningViewModel();
            vm.Briefcase = selectedBriefcase;
            vm.DialogWindow = this;
            vm.Prizes = prizes;
            vm.Host = host;
            DataContext = vm;

        }
    }
}
