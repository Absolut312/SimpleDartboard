using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartGameViewModel : BaseViewModel, IDartGameViewModel
    {
        private DartGameSetting _dartGameSetting;
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

        public ICommand SwitchPlayerCommand { get; set; }
        public ICommand ResetGameCommand { get; set; }

        public DartGameViewModel(IPlayerScoreBoardViewModel playerOne, IPlayerScoreBoardViewModel playerTwo,
            IDartBoardScoreInputViewModel dartBoardScoreInputViewModel)
        {
            DartBoardScoreInput = dartBoardScoreInputViewModel;
            _dartGameSetting = new DartGameSetting();
            _playerOne = playerOne;
            _playerTwo = playerTwo;

            ResetGameCommand = new RelayCommand(ResetGame);
            SwitchPlayerCommand = new RelayCommand(SwitchSelectedPlayer);
            SelectedPlayer = _playerOne;
            OpponentPlayer = _playerTwo;
            Mediator.Register(MessageType.ReduceScoreForSelectedPlayer, ReduceScoreForSelectedPlayer);
            Mediator.Register(MessageType.StartGame, InitializeGameSetting);
        }

        private void InitializeGameSetting(object gameSetting)
        {
            var dartGameSetting = gameSetting as DartGameSetting;
            if (dartGameSetting == null) return;
            _dartGameSetting = dartGameSetting;
            _playerOne.Name = dartGameSetting.PlayerOneName;
            _playerOne.CurrentScore = dartGameSetting.StartingScore;
            _playerOne.ClearScoreActions();
            _playerTwo.Name = dartGameSetting.PlayerTwoName;
            _playerTwo.CurrentScore = dartGameSetting.StartingScore;
            _playerTwo.ClearScoreActions();
        }

        private void ReduceScoreForSelectedPlayer(object scoreAction)
        {
            SelectedPlayer.AddScoreAction(scoreAction is int ? (int) scoreAction : 0);
        }

        private void SwitchSelectedPlayer()
        {
            var currentSelectedPlayer = SelectedPlayer;
            SelectedPlayer = OpponentPlayer;
            OpponentPlayer = currentSelectedPlayer;
        }

        private void ResetGame()
        {
            Mediator.NotifyColleagues(MessageType.StartGame, _dartGameSetting);
        }
    }
}