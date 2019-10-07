using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartGameWinnerViewModel : BaseViewModel, IDartGameWinnerViewModel
    {
        private IPlayerScoreBoardViewModel _playerScoreBoardViewModel;

        public IPlayerScoreBoardViewModel PlayerScoreBoardViewModel
        {
            get => _playerScoreBoardViewModel;
            set
            {
                _playerScoreBoardViewModel = value;
                OnPropertyChanged("PlayerScoreBoardViewModel");
            }
        }

        public ICommand StartNewGameCommand { get; set; }

        public DartGameWinnerViewModel()
        {
            StartNewGameCommand = new RelayCommand(StartNewGame);
            Mediator.Register(MessageType.ShowWinner, ShowWinner);
        }

        private void ShowWinner(object playerToShow)
        {
            PlayerScoreBoardViewModel = playerToShow as IPlayerScoreBoardViewModel;
        }

        private void StartNewGame()
        {
            Mediator.NotifyColleagues(MessageType.InitializeNewGame, null);
        }
    }
}