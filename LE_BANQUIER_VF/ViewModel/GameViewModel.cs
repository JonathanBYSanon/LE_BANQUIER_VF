
using LE_BANQUIER_VF.Core;
using LE_BANQUIER_VF.Model;
using LE_BANQUIER_VF.Service;
using LE_BANQUIER_VF.View.Popup;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace LE_BANQUIER_VF.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        private bool isOver = false;
        // The list of briefcases
        private ObservableCollection<Briefcase> _briefcases;
        // The list of prizes
        private ObservableCollection<Prize> _prizes;
        // The player
        private Player _player;
        // The banker
        private Banker _banker;
        // The host
        private Host _host;
        // The selected briefcase
        private Briefcase _selectedBriefcase;

        public ObservableCollection<Briefcase> Briefcases
        {
            get => _briefcases;
            set
            {
                _briefcases = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Prize> Prizes
        {
            get => _prizes;
            set
            {
                _prizes = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<Prize> LowPrizes => Prizes?.Take(13) ?? Enumerable.Empty<Prize>();
        public IEnumerable<Prize> HighPrizes => Prizes?.Skip(13).Take(13) ?? Enumerable.Empty<Prize>();

        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                OnPropertyChanged();
            }
        }

        public Banker Banker
        {
            get => _banker;
            set
            {
                _banker = value;
                OnPropertyChanged();
            }
        }

        public Host Host
        {
            get => _host;
            set
            {
                _host = value;
                OnPropertyChanged();
            }
        }

        public Briefcase SelectedBriefcase
        {
            get => _selectedBriefcase;
            set
            {
                _selectedBriefcase = value;
                OnPropertyChanged();
                if(value != null && !value.IsOpened)
                    onBriefcaseSelected();
            }
        }

        public RelayCommand leaveCommand => new RelayCommand(leave);

        public GameViewModel()
        {
            // Load the settings of the game
            GameSetting gameSetting = SettingService.LoadSettings();

            // Generate the prizes and the briefcases
            Prizes = PrizesGeneratorService.GeneratePrizes(gameSetting.MaxAmount);
            Briefcases = BriefcasesGeneratorService.GenerateBriefcases(Prizes);

            // Initialize the player, banker, and host
            Player = new Player { Name = gameSetting.PlayerName };
            Banker = new Banker { Offers = new ObservableCollection<int>() };
            Host = new Host();

            Host.Message = MessageGeneratorService.GetWelcomeMessage(Player.Name);
        }

        private void onBriefcaseSelected()
        {

            if (GameProgressService.Instance.Round == 0)
            {
                onPlayerBriefcaseSelection();
            }
            else if(GameProgressService.Instance.Round >= 1 && GameProgressService.Instance.Round <= 24)
            {
                onBriefcaseOpening();
            }
            else 
            { 
                onBriefcaseSwitching();
            }
            clearSelection();
        }
        private void onPlayerBriefcaseSelection()
        {
            PlayerBriefcaseSelectionDialag dialog = new PlayerBriefcaseSelectionDialag(Briefcases,Player,SelectedBriefcase,Host);
            bool result = dialog.ShowDialog() ?? false;

            Host.Message = MessageGeneratorService.GetBriefcaseSelectionReaction(SelectedBriefcase.Number, result);
            if (result) nextRound();
        }
        private void onBriefcaseOpening()
        {
            BriefcaseOpeningDialog dialog1 = new BriefcaseOpeningDialog(SelectedBriefcase, Host, getRemainingPrizes());
            bool result = dialog1.ShowDialog() ?? false;

            if (result) nextRound();
            else Host.Message = MessageGeneratorService.GetCancelBriefcaseEliminationMessage();
        }
        private void onOfferMaking()
        {
            Banker.MakeSmartOffer(getRemainingPrizes());
            OfferReceivingDialog dialog2 = new OfferReceivingDialog(Banker, Host);
            bool result = dialog2.ShowDialog() ?? false;

            if (result) onEndGame();
        }
        private void onBriefcaseSwitching()
        {
            BriefcaseSwitchingDialog dialog3 = new BriefcaseSwitchingDialog(Player, Briefcases, Host);
            dialog3.ShowDialog();

            onEndGame();
        }
        private void onEndGame()
        {
            GameResume gameResume = new GameResume(Player, Banker, Prizes);
            Player.Briefcase.IsOpened = true;
            GameEndingDialog dialog5 = new GameEndingDialog(gameResume,Host,Player.Briefcase);
            dialog5.ShowDialog();

            leave(null);
            isOver = true;
        }
        private void nextRound()
        {
            if(GameProgressService.Instance.IsOfferRound)
            {
                OfferTransitionDialog dialog6 = new OfferTransitionDialog(Host);
                if(dialog6.ShowDialog() ?? false)
                {
                    onOfferMaking();
                    if (isOver) return;

                    if (GameProgressService.Instance.Round < 24) Host.Message = MessageGeneratorService.GetPlayerDecisionReaction();
                }
            }
            else if(GameProgressService.Instance.Round != 0)
            {
                Host.Message = MessageGeneratorService.GetNextRoundMessage();
            }

            clearSelection();
            GameProgressService.Instance.NextRound();
            if (GameProgressService.Instance.Round == 25) onBriefcaseSwitching();
        }
        private void clearSelection()
        {
            SelectedBriefcase = null;
        }

       private List<int> getRemainingPrizes()
       {
            return Prizes
                .Where(p => p.IsAvailable)
                .Select(p => p.Amount)
                .ToList();
       }

       private void leave(object paramater)
       {
            GameProgressService.Instance.Reset();
            NavigationServiceLocator.NavigationService.NavigateTo("Home");
       }
    }
}
