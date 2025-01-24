using LE_BANQUIER_VF.Model;
using LE_BANQUIER_VF.Service;
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
    /// Interaction logic for OfferTransitionDialog.xaml
    /// </summary>
    public partial class OfferTransitionDialog : Window
    {
        public OfferTransitionDialog(Host host)
        {
            InitializeComponent();
            var fadeIn = (Storyboard)Resources["FadeInStoryboard"];
            fadeIn.Begin(this);

            host.Message = MessageGeneratorService.GetBankerCallMessage();
        }

        private void Decrocher(object sender, RoutedEventArgs e)
        {
            SoundManagerService.Instance.StopEffect();
            var fadeOut = (Storyboard)Resources["FadeOutStoryboard"];
            fadeOut.Completed += (s, _) =>
            {
                this.DialogResult = true;
            };
            fadeOut.Begin(this);
            
        }
    }
}
