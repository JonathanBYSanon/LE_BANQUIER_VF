
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
    /// <summary>
    /// The view model of the game
    /// </summary>
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
        // The low prizes, the first 13 prizes
        public IEnumerable<Prize> LowPrizes => Prizes?.Take(13) ?? Enumerable.Empty<Prize>();
        // The high prizes, the last 13 prizes
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
        // The selected briefcase, when a briefcase is selected the game will react to it depending on the round
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
        // Command to leave the game
        public RelayCommand leaveCommand => new RelayCommand(leave);

        public GameViewModel()
        {

            // Generate the prizes and the briefcases
            Prizes = PrizesGeneratorService.GeneratePrizes(SettingService.Settings.MaxAmount);
            Briefcases = BriefcasesGeneratorService.GenerateBriefcases(Prizes);

            // Initialize the player, banker, and host
            Player = new Player { Name = SettingService.Settings.PlayerName };
            Banker = new Banker { Offers = new ObservableCollection<int>() };
            Host = new Host();

            Host.Message = MessageGeneratorService.GetWelcomeMessage(Player.Name);
        }

        /// <summary>
        /// Method to react to the selection of a briefcase, the reaction depends on the round. The selectedBriefcase is cleared after the reaction
        /// </summary>
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
           
            clearSelection();
        }
        /// <summary>
        /// Method to allow the player to select a briefcase, it's called in round 0
        /// </summary>
        private void onPlayerBriefcaseSelection()
        {
            PlayerBriefcaseSelectionDialag dialog = new PlayerBriefcaseSelectionDialag(Briefcases,Player,SelectedBriefcase,Host);
            dialog.ShowDialog() ;

            bool isConfirmed = (Player.Briefcase == SelectedBriefcase) && Briefcases.Count == 25;

            Host.Message = MessageGeneratorService.GetBriefcaseSelectionReaction(SelectedBriefcase.Number, isConfirmed);
            if (isConfirmed) nextRound();
        }
        /// <summary>
        /// Method to allow the player to eliminate a briefcase, it's called from round 1 to 24
        /// </summary>
        private void onBriefcaseOpening()
        {
            BriefcaseOpeningDialog dialog1 = new BriefcaseOpeningDialog(SelectedBriefcase, Host, getRemainingPrizes());
           dialog1.ShowDialog();

            if (SelectedBriefcase.IsOpened) nextRound();
            else Host.Message = MessageGeneratorService.GetCancelBriefcaseEliminationMessage();
        }
        /// <summary>
        /// Method the make an offer to the player by the banker, it will end the game if the offer is accepted, it's called in the offer rounds
        /// </summary>
        private void onOfferMaking()
        {
            Banker.Offer = OfferCalculatorService.CalculateOffer(getRemainingPrizes());
            OfferReceivingDialog dialog2 = new OfferReceivingDialog(Banker, Host);
            dialog2.ShowDialog();

            if (GameProgressService.Instance.isOverOnOffer) onEndGame();
        }

        /// <summary>
        /// Method to allow the player to switch briefcases, it's called in round 25, the game will end after the switch
        /// </summary>
        private void onBriefcaseSwitching()
        {
            BriefcaseSwitchingDialog dialog3 = new BriefcaseSwitchingDialog(Player, Briefcases, Host);
            dialog3.ShowDialog();

            onEndGame();
        }
        /// <summary>
        /// Method to end the game, it will show the game resume and the player's briefcase will be revealed
        /// </summary>
        private void onEndGame()
        {
            GameResume gameResume = new GameResume(Player, Banker, Prizes);
            Player.Briefcase.IsOpened = true;
            GameEndingDialog dialog5 = new GameEndingDialog(gameResume,Host,Player.Briefcase);
            dialog5.ShowDialog();

            leave(null);
            isOver = true;
        }
        /// <summary>
        /// Method to go to the next round, it will call the offer making method if the round is an offer round, it will call briefcase switching method if it's round 25 and genereate a message for the player
        /// </summary>
        private void nextRound()
        {
            if(GameProgressService.Instance.IsOfferRound)
            {
                OfferTransitionDialog dialog6 = new OfferTransitionDialog(Host);
                dialog6.ShowDialog();
                
                onOfferMaking();
                if (isOver) return;

                if (GameProgressService.Instance.Round < 24) Host.Message = MessageGeneratorService.GetPlayerDecisionReaction();

            }
            else if(GameProgressService.Instance.Round != 0)
            {
                Host.Message = MessageGeneratorService.GetNextRoundMessage();
            }

            clearSelection();
            GameProgressService.Instance.NextRound();
            if (GameProgressService.Instance.Round == 25) onBriefcaseSwitching();
        }
        /// <summary>
        /// Method to clear the selected briefcase
        /// </summary>
        private void clearSelection()
        {
            SelectedBriefcase = null;
        }
        /// <summary>
        /// Method to get the remaining prizes in a list
        /// </summary>
        /// <returns></returns>
        private List<int> getRemainingPrizes()
        {
            return Prizes
                .Where(p => p.IsAvailable)
                .Select(p => p.Amount)
                .ToList();
        }
        /// <summary>
        /// Method to leave the game, it will reset the game progress and navigate to the home page
        /// </summary>
        /// <param name="paramater"></param>
        private void leave(object paramater)
        {
            GameProgressService.Instance.Reset();
            NavigationServiceLocator.NavigationService.NavigateTo("Home");
        }
    }
}
