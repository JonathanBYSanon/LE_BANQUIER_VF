using LE_BANQUIER_VF.Core;
using System;
using System.Windows;

namespace LE_BANQUIER_VF.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        public RelayCommand StartNewGame { get; set; }
        public RelayCommand ShowRules { get; set; }
        public RelayCommand ShowSettings { get; set; }
        public RelayCommand Exit { get; set; }

        public MenuViewModel()
        {
            StartNewGame = new RelayCommand(StartNewGameExecute);
            ShowRules = new RelayCommand(ShowRulesExecute);
            ShowSettings = new RelayCommand(ShowSettingsExecute);
            Exit = new RelayCommand(ExitExecute);
        }

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
        private void ExitExecute(Object o)
        {
            MessageBoxResult result = MessageBox.Show("Etes-vous sur de vouloir quitter?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }
    }
}
