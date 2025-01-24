using LE_BANQUIER_VF.Core;
using System.Windows;
using System.Windows.Controls;


namespace LE_BANQUIER_VF.View
{
    /// <summary>
    /// Interaction logic for RulesView.xaml
    /// </summary>
    public partial class RulesView : UserControl
    {
        public RulesView()
        {
            InitializeComponent();
        }
         private void BackToMenu_Click(object sender, RoutedEventArgs e)
         {
            // Navigate back to the main menu
            NavigationServiceLocator.NavigationService.NavigateTo("Home");
         }
    }
}
