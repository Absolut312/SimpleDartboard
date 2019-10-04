using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.Models;

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

        private IDartBoardScoreControlViewModel _dartBoardScoreControl;

        public IDartBoardScoreControlViewModel DartBoardScoreControl
        {
            get { return _dartBoardScoreControl; }
            set
            {
                _dartBoardScoreControl = value;
                OnPropertyChanged("DartBoardScoreControl");
            }
        }

        public ICommand SwitchPlayerCommand { get; set; }
        public ICommand ResetGameCommand { get; set; }
        public ICommand UndoLastScoreActionCommand { get; set; }

        public DartGameViewModel(IPlayerScoreBoardViewModel playerOne,
            IPlayerScoreBoardViewModel playerTwo,
            IDartBoardScoreControlViewModel dartBoardScoreControlViewModel)
        {
            _dartGameSetting = new DartGameSetting();
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            DartBoardScoreControl = dartBoardScoreControlViewModel;
            ResetGameCommand = new RelayCommand(ResetGame);
            SwitchPlayerCommand = new RelayCommand(SwitchSelectedPlayer);
            UndoLastScoreActionCommand = new RelayCommand(UndoLastScoreActionCommant,
                () => { return SelectedPlayer.HasScoreActions(); });
            SelectedPlayer = _playerOne;
            OpponentPlayer = _playerTwo;
            Mediator.Register(MessageType.ReduceScoreForSelectedPlayer, ReduceScoreForSelectedPlayer);
            Mediator.Register(MessageType.StartGame, InitializeGameSetting);
        }

        private void UndoLastScoreActionCommant()
        {
            SelectedPlayer.UndoLastScoreAction();
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