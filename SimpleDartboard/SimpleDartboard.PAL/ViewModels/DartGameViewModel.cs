using System.Collections.Generic;
using System.Linq;
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

        public IDartGameControlViewModel DartGameControlViewModel
        {
            get => _dartGameControlViewModel;
            set
            {
                _dartGameControlViewModel = value;
                OnPropertyChanged("DartGameControlViewModel");
            }
        }

        public string AverageScore
        {
            get
            {
                if (_scoreActionHistoryPerRound.Count == 0) return "--";
                var summedScoreActions = _scoreActionHistoryPerRound.Sum(x => x.Score * x.Multiplier) /
                                         _scoreActionHistoryPerRound.Count;
                return "Durchschnitt in dieser Runde: " + summedScoreActions;
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
            return "Treffer: " + scoreActionCommaSeperatedText + " Gesamt: " + summedScoreActions;
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

        private List<ScoreAction> _scoreActionHistoryPerRound;
        private IDartGameControlViewModel _dartGameControlViewModel;

        public DartGameViewModel(IPlayerScoreBoardViewModel playerOne,
            IPlayerScoreBoardViewModel playerTwo,
            IDartBoardScoreControlViewModel dartBoardScoreControlViewModel,
            IDartGameControlViewModel dartGameControlViewModel)
        {
            _dartGameSetting = new DartGameSetting();
            _scoreActionHistoryPerRound = new List<ScoreAction>();
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            DartBoardScoreControl = dartBoardScoreControlViewModel;
            DartGameControlViewModel = dartGameControlViewModel;
            SelectedPlayer = _playerOne;
            OpponentPlayer = _playerTwo;
            RegisterMediatorMessages();
        }

        private void RegisterMediatorMessages()
        {
            Mediator.Register(MessageType.SwichtSelectedPlayer, SwitchSelectedPlayer);
            Mediator.Register(MessageType.ReduceScoreForSelectedPlayer, ReduceScoreForSelectedPlayer);
            Mediator.Register(MessageType.StartGame, InitializeGameSetting);
            Mediator.Register(MessageType.UndoLastScoreAction, UndoLastScoreAction);
        }

        private void UndoLastScoreAction(object obj)
        {
            var lastScoreActionIndex = _scoreActionHistoryPerRound.Count - 1;
            var scoreActionToRevert = _scoreActionHistoryPerRound[lastScoreActionIndex];
            Mediator.NotifyColleagues(MessageType.RemoveLastActionToken, scoreActionToRevert);
            if (!scoreActionToRevert.IsReverted)
            {
                SelectedPlayer.AddScoreAction(new ScoreAction
                    {Multiplier = scoreActionToRevert.Multiplier * (-1), Score = scoreActionToRevert.Score});
            }

            Mediator.NotifyColleagues(MessageType.SetIsDartboardScoreInputActive, true);
            _scoreActionHistoryPerRound.RemoveAt(lastScoreActionIndex);
            if (AreAllScoreActionsRevertedBecauseOfUnderScore())
            {
                ReAddRevertedScoreActions();
            }

            RaiseScoreActionChanges();
        }

        private void ReAddRevertedScoreActions()
        {
            _scoreActionHistoryPerRound.ForEach(x =>
            {
                SelectedPlayer.AddScoreAction(new ScoreAction
                    {Multiplier = x.Multiplier, Score = x.Score});
                x.IsReverted = false;
            });
        }

        private bool AreAllScoreActionsRevertedBecauseOfUnderScore()
        {
            return !_scoreActionHistoryPerRound.Exists(x => !x.IsReverted) &&
                   _scoreActionHistoryPerRound.Sum(x => x.Multiplier * x.Score) < SelectedPlayer.CurrentScore + 1;
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

            if (IsInvalidScoreActionToWin(scoreAction))
            {
                UndoInvalidScoreAction();
            }
            else if (IsInvalidScoreAction())
            {
                UndoRoundScoreActions();
            }
            else if (SelectedPlayer.CurrentScore == 0)
            {
                Mediator.NotifyColleagues(MessageType.ShowWinner, SelectedPlayer);
            }

            RaiseScoreActionChanges();
        }

        private void UndoInvalidScoreAction()
        {
            var lastScoreActionIndex = _scoreActionHistoryPerRound.Count - 1;
            var scoreActionToRevert = _scoreActionHistoryPerRound[lastScoreActionIndex];
            scoreActionToRevert.IsReverted = true;
            SelectedPlayer.AddScoreAction(new ScoreAction
                {Multiplier = scoreActionToRevert.Multiplier * (-1), Score = scoreActionToRevert.Score});
        }

        private void UndoRoundScoreActions()
        {
            _scoreActionHistoryPerRound.ForEach(x =>
            {
                x.IsReverted = true;
                SelectedPlayer.AddScoreAction(new ScoreAction
                    {Multiplier = x.Multiplier * (-1), Score = x.Score});
            });

            Mediator.NotifyColleagues(MessageType.DisableUndoLastScoreAction, true);
            Mediator.NotifyColleagues(MessageType.SetIsDartboardScoreInputActive, false);
        }

        private bool IsInvalidScoreAction()
        {
            return SelectedPlayer.CurrentScore == 1 || SelectedPlayer.CurrentScore < 0;
        }

        private bool IsInvalidScoreActionToWin(ScoreAction scoreAction)
        {
            return SelectedPlayer.CurrentScore == 0 && scoreAction.Multiplier != 2;
        }

        private void SwitchSelectedPlayer(object obj)
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
            Mediator.NotifyColleagues(MessageType.DisableUndoLastScoreAction, _scoreActionHistoryPerRound.Count == 0);
            OnPropertyChanged("AverageScore");
            OnPropertyChanged("TotalScore");
        }
    }
}