using System.Windows;
using System.Windows.Controls;


namespace LE_BANQUIER_VF.View
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.GameViewModel();
        }
    }
}
