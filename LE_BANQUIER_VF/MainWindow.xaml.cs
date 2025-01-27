using System.Windows;
using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.ViewModel;
using LE_BANQUIER_VF.View;
using LE_BANQUIER_VF.Service;

namespace LE_BANQUIER_VF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SoundManagerService.Instance.PlayMusic("BackgroundSound.mp3", true); // Play the background music

            // Initialization of the navigation service with the contentcontrol
            NavigationServiceLocator.NavigationService = new NavigationService(ContentSpace);

            // Registering the views
            NavigationServiceLocator.NavigationService.RegisterView("Home", typeof(MenuView));
            NavigationServiceLocator.NavigationService.RegisterView("Game",typeof(GameView));
            NavigationServiceLocator.NavigationService.RegisterView("Rules", typeof(RulesView));
            NavigationServiceLocator.NavigationService.RegisterView("Settings", typeof(SettingsView));

            // Navigating to the home view automatically
            NavigationServiceLocator.NavigationService.NavigateTo("Home");
        }
    }
}
