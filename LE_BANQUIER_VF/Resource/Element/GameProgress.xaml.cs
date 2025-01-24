using LE_BANQUIER_VF.Service;
using System.Windows.Controls;


namespace LE_BANQUIER_VF.Resource.Element
{
    /// <summary>
    /// Interaction logic for GameProgress.xaml
    /// </summary>
    public partial class GameProgress : UserControl
    {
        public GameProgress()
        {
            InitializeComponent();
            DataContext = GameProgressService.Instance;
        }
    }
}
