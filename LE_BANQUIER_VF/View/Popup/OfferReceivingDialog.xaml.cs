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
    /// Interaction logic for OfferReceivingDialog.xaml
    /// </summary>
    public partial class OfferReceivingDialog : Window
    {
        public OfferReceivingDialog(Banker banker, Host host)
        {
            InitializeComponent();
            var fadeIn = (Storyboard)Resources["FadeInStoryboard"];
            fadeIn.Begin(this);

            host.Message = MessageGeneratorService.GetOfferAcceptanceMessage(banker.Offer);

            var vm = new OfferReceivingViewModel();
            vm.Banker = banker;
            vm.DialogWindow = this;
            DataContext = vm;
        }
    }
}
