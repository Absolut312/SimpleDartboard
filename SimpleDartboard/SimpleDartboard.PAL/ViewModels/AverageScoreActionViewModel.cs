using System.Collections.Generic;
using System.Linq;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.ViewModels
{
    public class AverageScoreActionViewModel : BaseViewModel, IAverageScoreActionViewModel
    {
        public AverageScoreActionViewModel()
        {
            _scoreActions = new List<ScoreAction>();
        }

        public ScoreAction CheckoutScoreActions()
        {
            var scoreActionToCheckout = new ScoreAction
            {
                Multiplier = 1,
                Score = GetScoreActionsSum()
            };
            Reset();
            return scoreActionToCheckout;
        }

        public void AddScoreAction(ScoreAction scoreAction)
        {
            _scoreActions.Add(scoreAction);
            if (_scoreActions.Count == 3)
            {
                Mediator.NotifyColleagues(MessageType.SetIsDartboardScoreInputActive, false);
            }
            Mediator.NotifyColleagues(MessageType.DisableUndoLastScoreAction, false);
            OnPropertyChanged("AverageScoreActions");
            OnPropertyChanged("CommaSeparatedScoreActions");
        }

        public void UndoLastScoreAction()
        {
            if (_scoreActions.Count == 0) return;

            var scoreActionToUndo = _scoreActions[_scoreActions.Count - 1];
            Mediator.NotifyColleagues(MessageType.RemoveLastActionToken, scoreActionToUndo);
            
            Mediator.NotifyColleagues(MessageType.SetIsDartboardScoreInputActive, true);
            _scoreActions.RemoveAt(_scoreActions.Count - 1);
            Mediator.NotifyColleagues(MessageType.DisableUndoLastScoreAction, _scoreActions.Count == 0);
            //TODO: Recalculate Current valid Scoreactions
            
            OnPropertyChanged("AverageScoreActions");
            OnPropertyChanged("CommaSeparatedScoreActions");
        }

        public void Reset()
        {
            _scoreActions.Clear();
            Mediator.NotifyColleagues(MessageType.DisableUndoLastScoreAction, true);
            OnPropertyChanged("AverageScoreActions");
            OnPropertyChanged("CommaSeparatedScoreActions");
        }

        public void RevertAllScoreActions()
        {
            _scoreActions.ForEach(x => x.IsReverted = true);
            OnPropertyChanged("AverageScoreActions");
        }

        public void RevertLastScoreActions()
        {
            _scoreActions[_scoreActions.Count].IsReverted = true;
            OnPropertyChanged("AverageScoreActions");
        }

        private readonly List<ScoreAction> _scoreActions;

        public int AverageScoreActions
        {
            get { return _scoreActions.Count != 0 ? GetScoreActionsSum() / _scoreActions.Count : 0; }
        }

        public string CommaSeparatedScoreActions
        {
            get
            {
                var commaSeparatedScoreActions = "";
                foreach (var scoreAction in _scoreActions)
                {
                    commaSeparatedScoreActions += GetScoreActionPrefix(scoreAction);
                    commaSeparatedScoreActions += scoreAction.Score + ", ";
                }

                commaSeparatedScoreActions = commaSeparatedScoreActions.TrimEnd();
                commaSeparatedScoreActions = commaSeparatedScoreActions.TrimEnd(',');
                return commaSeparatedScoreActions;
            }
        }

        private static string GetScoreActionPrefix(ScoreAction scoreAction)
        {
            string commaSeparatedScoreActions = "";
            switch (scoreAction.Multiplier)
            {
                case 2:
                {
                    commaSeparatedScoreActions += "D";
                    break;
                }
                case 3:
                {
                    commaSeparatedScoreActions += "T";
                    break;
                }
            }

            return commaSeparatedScoreActions;
        }

        public int GetScoreActionsSum()
        {
            return _scoreActions.Sum(x => x.IsReverted ? 0 : x.Multiplier * x.Score);
        }
    }
}