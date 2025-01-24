using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.Model;
using LE_BANQUIER_VF.Properties;
using LE_BANQUIER_VF.Service;
using System.Windows;
using System.Windows.Controls;


namespace LE_BANQUIER_VF.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public GameSetting Settings { get; set; }
        public SettingsView()
        {
            InitializeComponent();
            LoadSettings();
            DataContext = this;
        }

        private void LoadSettings()
        {
            Settings = SettingService.LoadSettings();
            UpdateSoundToggleContent();
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingService.SaveSettings(Settings);
            NavigationServiceLocator.NavigationService.NavigateTo("Home");
        }

        private void SoundToggle_Checked(object sender, RoutedEventArgs e)
        {
            SoundManagerService.IsMuted = false;
            UpdateSoundToggleContent();
        }

        private void SoundToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            SoundManagerService.IsMuted = true;
            UpdateSoundToggleContent();
        }

        private void UpdateSoundToggleContent()
        {
            SoundToggle.Content = SoundManagerService.IsMuted ? "🔇 Muet" : "🔊 Activer";
        }
    }
}
