using LE_BANQUIER_VF.Core;
using System;
using System.Windows;

namespace LE_BANQUIER_VF.ViewModel
{
    /// <summary>
    /// View model for the menu view
    /// </summary>
    public class MenuViewModel : BaseViewModel
    {
        // command to start a new game
        public RelayCommand StartNewGame { get; set; }
        // Command to show the rules
        public RelayCommand ShowRules { get; set; }
        // Command to show the settings
        public RelayCommand ShowSettings { get; set; }
        // Command to exit the application
        public RelayCommand Exit { get; set; }

        public MenuViewModel()
        {
            StartNewGame = new RelayCommand(StartNewGameExecute);
            ShowRules = new RelayCommand(ShowRulesExecute);
            ShowSettings = new RelayCommand(ShowSettingsExecute);
            Exit = new RelayCommand(ExitExecute);
        }

        // ##### Methods that navigate to the different views #####
        private void StartNewGameExecute(Object o)
        {
            NavigationServiceLocator.NavigationService.NavigateTo("Game");
        }
        private void ShowRulesExecute(Object o)
        {
            NavigationServiceLocator.NavigationService.NavigateTo("Rules");
        }
        private void ShowSettingsExecute(Object o)
        {
            NavigationServiceLocator.NavigationService.NavigateTo("Settings");
        }

        /// <summary>
        /// Method to exit the application after a confirmation dialog
        /// </summary>
        /// <param name="o"></param>
        private void ExitExecute(Object o)
        {
            MessageBoxResult result = MessageBox.Show("Etes-vous sur de vouloir quitter?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }
    }
}
