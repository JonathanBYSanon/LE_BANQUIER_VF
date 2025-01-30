using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.Model;
using LE_BANQUIER_VF.Service;
using System.Windows;

namespace LE_BANQUIER_VF.ViewModel
{
    /// <summary>
    /// ViewModel for managing game settings.
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        // Bind directly to the singleton's Settings instance
        public GameSetting Settings => SettingService.Settings;

        // Commands
        public RelayCommand SaveSettingsCommand => new RelayCommand(SaveSettings);
        public RelayCommand SoundToggleCommand => new RelayCommand(SoundToggle);
        public RelayCommand SmartOfferToggleCommand => new RelayCommand(SmartOfferToggle);
        public RelayCommand SmartPrizesGeneratorToggleCommand => new RelayCommand(SmartPrizesGeneratorToggle);

        private void SoundToggle(object parameter)
        {
            SettingService.Settings.IsSoundEnabled = !SettingService.Settings.IsSoundEnabled;

        }

        private void SmartOfferToggle(object parameter)
        {
            SettingService.Settings.IsSmartOfferEnabled = !SettingService.Settings.IsSmartOfferEnabled;
        }

        private void SmartPrizesGeneratorToggle(object parameter)
        {
            SettingService.Settings.IsSmartPrizeGeneratorEnabled = !SettingService.Settings.IsSmartPrizeGeneratorEnabled;
        }

        private void SaveSettings(object parameter)
        {
            NavigationServiceLocator.NavigationService.NavigateTo("Home");
        }
    }
}
