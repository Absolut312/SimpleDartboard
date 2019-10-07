using System.Collections.Generic;
using System.Linq;
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

        public string AverageScore
        {
            get
            {
                if (_scoreActionHistoryPerRound.Count == 0) return "--";
                var summedScoreActions = _scoreActionHistoryPerRound.Sum(x => x.Score * x.Multiplier) /
                                         _scoreActionHistoryPerRound.Count;
                return "Durchschnitt pro Runde: " + summedScoreActions;
            }
        }
        
        public string TotalScore
        {
            get { return TransformScoreActionToToalScorePerRound(); }
        }

        private string TransformScoreActionToToalScorePerRound()
        {
            if (_scoreActionHistoryPerRound.Count == 0) return "--";
            var summedScoreActions = _scoreActionHistoryPerRound.Sum(x => x.Score * x.Multiplier) /
                                     _scoreActionHistoryPerRound.Count;
            var scoreActionCommaSeperatedText = "";
            foreach (var scoreAction in _scoreActionHistoryPerRound)
            {
                scoreActionCommaSeperatedText = TransformScoreActionToText(scoreAction, scoreActionCommaSeperatedText);
            }

            scoreActionCommaSeperatedText = scoreActionCommaSeperatedText.TrimEnd();
            scoreActionCommaSeperatedText = scoreActionCommaSeperatedText.TrimEnd(',');
            return "Runde: " + scoreActionCommaSeperatedText + " Gesamt: " + summedScoreActions;
        }

        private static string TransformScoreActionToText(ScoreAction scoreAction, string scoreActionCommaSeperatedText)
        {
            switch (scoreAction.Multiplier)
            {
                case 2:
                {
                    scoreActionCommaSeperatedText += "D";
                    break;
                }
                case 3:
                {
                    scoreActionCommaSeperatedText += "T";
                    break;
                }
            }

            scoreActionCommaSeperatedText += scoreAction.Score + ", ";
            return scoreActionCommaSeperatedText;
        }

        public ICommand SwitchPlayerCommand { get; set; }
        public ICommand ResetGameCommand { get; set; }
        public ICommand UndoLastScoreActionCommand { get; set; }
        private List<ScoreAction> _scoreActionHistoryPerRound;

        public DartGameViewModel(IPlayerScoreBoardViewModel playerOne,
            IPlayerScoreBoardViewModel playerTwo,
            IDartBoardScoreControlViewModel dartBoardScoreControlViewModel)
        {
            _dartGameSetting = new DartGameSetting();
            _scoreActionHistoryPerRound = new List<ScoreAction>();
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            DartBoardScoreControl = dartBoardScoreControlViewModel;
            ResetGameCommand = new RelayCommand(ResetGame);
            SwitchPlayerCommand = new RelayCommand(SwitchSelectedPlayer);
            UndoLastScoreActionCommand =
                new RelayCommand(UndoLastScoreAction, () => _scoreActionHistoryPerRound.Count > 0);
            SelectedPlayer = _playerOne;
            OpponentPlayer = _playerTwo;
            Mediator.Register(MessageType.ReduceScoreForSelectedPlayer, ReduceScoreForSelectedPlayer);
            Mediator.Register(MessageType.StartGame, InitializeGameSetting);
        }

        private void UndoLastScoreAction()
        {
            var lastScoreActionIndex = _scoreActionHistoryPerRound.Count - 1;
            var scoreActionToRevert = _scoreActionHistoryPerRound[lastScoreActionIndex];
            Mediator.NotifyColleagues(MessageType.RemoveLastActionToken, scoreActionToRevert);
            scoreActionToRevert.Multiplier *= -1;
            SelectedPlayer.AddScoreAction(scoreActionToRevert);
            if (_scoreActionHistoryPerRound.Count == 3)
            {
                Mediator.NotifyColleagues(MessageType.SetIsDartboardScoreInputActive, true);
            }

            _scoreActionHistoryPerRound.RemoveAt(lastScoreActionIndex);
            RaiseScoreActionChanges();
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
            ClearActionTokens();
        }

        private void ReduceScoreForSelectedPlayer(object scoreActionObject)
        {
            if (!(scoreActionObject is ScoreAction scoreAction)) return;
            SelectedPlayer.AddScoreAction(scoreAction);
            _scoreActionHistoryPerRound.Add(scoreAction);
            if (_scoreActionHistoryPerRound.Count == 3)
            {
                Mediator.NotifyColleagues(MessageType.SetIsDartboardScoreInputActive, false);
            }

            if (SelectedPlayer.CurrentScore == 0 && scoreAction.Multiplier != 2)
            {
                var lastScoreActionIndex = _scoreActionHistoryPerRound.Count - 1;
                var scoreActionToRevert = _scoreActionHistoryPerRound[lastScoreActionIndex];
                scoreActionToRevert.Multiplier *= -1;
                SelectedPlayer.AddScoreAction(scoreActionToRevert);
            }
            else if (SelectedPlayer.CurrentScore == 1 || SelectedPlayer.CurrentScore < 0)
            {
                while (_scoreActionHistoryPerRound.Count > 0)
                {
                    UndoLastScoreAction();
                }

                SwitchSelectedPlayer();
                return;
            }

            if (_scoreActionHistoryPerRound.Count == 3)
            {
                SwitchSelectedPlayer();
            }

            RaiseScoreActionChanges();
        }

        private void SwitchSelectedPlayer()
        {
            var currentSelectedPlayer = SelectedPlayer;
            SelectedPlayer = OpponentPlayer;
            OpponentPlayer = currentSelectedPlayer;
            ClearActionTokens();
        }

        private void ClearActionTokens()
        {
            _scoreActionHistoryPerRound.ForEach(x => Mediator.NotifyColleagues(MessageType.RemoveLastActionToken, x));
            _scoreActionHistoryPerRound.Clear();
            Mediator.NotifyColleagues(MessageType.SetIsDartboardScoreInputActive, true);
            RaiseScoreActionChanges();
        }

        private void RaiseScoreActionChanges()
        {
            OnPropertyChanged("AverageScore");
            OnPropertyChanged("TotalScore");
        }

        private void ResetGame()
        {
            Mediator.NotifyColleagues(MessageType.StartGame, _dartGameSetting);
        }
    }
}