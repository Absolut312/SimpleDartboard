using System.Collections.Generic;
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
        private List<ScoreAction> _roundScoreActionHistory;

        public DartGameViewModel(IPlayerScoreBoardViewModel playerOne,
            IPlayerScoreBoardViewModel playerTwo,
            IDartBoardScoreControlViewModel dartBoardScoreControlViewModel)
        {
            _dartGameSetting = new DartGameSetting();
            _roundScoreActionHistory = new List<ScoreAction>();
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            DartBoardScoreControl = dartBoardScoreControlViewModel;
            ResetGameCommand = new RelayCommand(ResetGame);
            SwitchPlayerCommand = new RelayCommand(SwitchSelectedPlayer);
            UndoLastScoreActionCommand =
                new RelayCommand(UndoLastScoreAction, () => _roundScoreActionHistory.Count > 0);
            SelectedPlayer = _playerOne;
            OpponentPlayer = _playerTwo;
            Mediator.Register(MessageType.ReduceScoreForSelectedPlayer, ReduceScoreForSelectedPlayer);
            Mediator.Register(MessageType.StartGame, InitializeGameSetting);
        }

        private void UndoLastScoreAction()
        {
            var lastScoreActionIndex = _roundScoreActionHistory.Count - 1;
            var scoreActionToRevert = _roundScoreActionHistory[lastScoreActionIndex];
            Mediator.NotifyColleagues(MessageType.RemoveLastActionToken, scoreActionToRevert);
            scoreActionToRevert.Multiplier *= -1;
            SelectedPlayer.AddScoreAction(scoreActionToRevert);
            if (_roundScoreActionHistory.Count == 3)
            {
                Mediator.NotifyColleagues(MessageType.SetIsDartboardScoreInputActive, true);
            }
            _roundScoreActionHistory.RemoveAt(lastScoreActionIndex);
        }

        private void InitializeGameSetting(object gameSetting)
        {
            var dartGameSetting = gameSetting as DartGameSetting;
            if (dartGameSetting == null) return;
            _dartGameSetting = dartGameSetting;
            _playerOne.Name = dartGameSetting.PlayerOneName;
            _playerOne.CurrentScore = dartGameSetting.StartingScore;
            _playerTwo.Name = dartGameSetting.PlayerTwoName;
            _playerTwo.CurrentScore = dartGameSetting.StartingScore;
            _roundScoreActionHistory.Clear();
            Mediator.NotifyColleagues(MessageType.SetIsDartboardScoreInputActive, true);
        }

        private void ReduceScoreForSelectedPlayer(object scoreActionObject)
        {
            if (!(scoreActionObject is ScoreAction scoreAction)) return;
            SelectedPlayer.AddScoreAction(scoreAction);
            _roundScoreActionHistory.Add(scoreAction);
            if (_roundScoreActionHistory.Count == 3)
            {
                Mediator.NotifyColleagues(MessageType.SetIsDartboardScoreInputActive, false);
            }
            if (SelectedPlayer.CurrentScore == 1 ||
                SelectedPlayer.CurrentScore == 0 && scoreAction.Multiplier != 2)
            {
                scoreAction.Multiplier *= -1;
                SelectedPlayer.AddScoreAction(scoreAction);
            }
        }

        private void SwitchSelectedPlayer()
        {
            var currentSelectedPlayer = SelectedPlayer;
            SelectedPlayer = OpponentPlayer;
            OpponentPlayer = currentSelectedPlayer;
            _roundScoreActionHistory.Clear();
        }

        private void ResetGame()
        {
            Mediator.NotifyColleagues(MessageType.StartGame, _dartGameSetting);
        }
    }
}