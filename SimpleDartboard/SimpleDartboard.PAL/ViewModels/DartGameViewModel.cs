using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartGameViewModel : BaseViewModel, IDartGameViewModel
    {
        private const int StartingScore = 501;
        private IPlayerScoreBoardViewModel _playerOne;
        private IPlayerScoreBoardViewModel _playerTwo;
        private IPlayerScoreBoardViewModel _selectedPlayer;
        private IPlayerScoreBoardViewModel _opponentPlayer;

        public IPlayerScoreBoardViewModel SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                _selectedPlayer = value;
                OnPropertyChanged("SelectedPlayer");
            }
        }

        public IPlayerScoreBoardViewModel OpponentPlayer
        {
            get { return _opponentPlayer; }
            set
            {
                _opponentPlayer = value;
                OnPropertyChanged("OpponentPlayer");
            }
        }

        private IDartBoardScoreInputViewModel _dartBoardScoreInput;

        public IDartBoardScoreInputViewModel DartBoardScoreInput
        {
            get { return _dartBoardScoreInput; }
            set
            {
                _dartBoardScoreInput = value;
                OnPropertyChanged("DartBoardScoreInput");
            }
        }

        public ICommand SwitchPlayer { get; set; }
        public ICommand ResetGame { get; set; }

        public DartGameViewModel(IPlayerScoreBoardViewModel playerOne, IPlayerScoreBoardViewModel playerTwo,
            IDartBoardScoreInputViewModel dartBoardScoreInputViewModel)
        {
            DartBoardScoreInput = dartBoardScoreInputViewModel;
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            _playerOne.Name = "Player 1";
            _playerTwo.Name = "Player 2";

            ResetGame = new RelayCommand(StartNewGame);
            SwitchPlayer = new RelayCommand(SwitchSelectedPlayer);
            SelectedPlayer = _playerOne;
            OpponentPlayer = _playerTwo;
            StartNewGame();
            Mediator.Register(MessageType.ReduceScoreForSelectedPlayer, ReduceScoreForSelectedPlayer);
        }

        private void ReduceScoreForSelectedPlayer(object scoreAction)
        {
            SelectedPlayer.CurrentScore -= scoreAction is int ? (int) scoreAction : 0;
        }

        private void SwitchSelectedPlayer()
        {
            var currentSelectedPlayer = SelectedPlayer;
            SelectedPlayer = OpponentPlayer;
            OpponentPlayer = currentSelectedPlayer;
        }

        private void StartNewGame()
        {
            SelectedPlayer.CurrentScore = StartingScore;
            OpponentPlayer.CurrentScore = StartingScore;
        }
    }
}