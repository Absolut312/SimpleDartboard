using System.Collections.Generic;
using System.Linq;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.ViewModels
{
    public class PlayerScoreBoardViewModel : BaseViewModel, IPlayerScoreBoardViewModel
    {
        private string _name;
        private List<ScoreAction> _scoreActions;

        public PlayerScoreBoardViewModel()
        {
            _scoreActions = new List<ScoreAction>();
            Mediator.Register(MessageType.StartGame, ClearScoreActions);
        }

        private void ClearScoreActions(object obj)
        {
            _scoreActions.Clear();
            OnPropertyChanged("AverageScore");
        }


        public string Name
        {
            get { return _name; }

            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private int _currentScore;

        public int CurrentScore
        {
            get { return _currentScore; }

            set
            {
                _currentScore = value;
                OnPropertyChanged("CurrentScore");
            }
        }

        public void AddScoreAction(ScoreAction scoreAction)
        {
            CurrentScore -= scoreAction.TotalScoreAction;
            if (scoreAction.Multiplier < 0)
            {
                _scoreActions.RemoveAt(_scoreActions.Count - 1);
            }
            else
            {
                _scoreActions.Add(scoreAction);
            }

            OnPropertyChanged("AverageScore");
        }

        public string AverageScore
        {
            get
            {
                if (_scoreActions.Count == 0) return "--";
                var summedScoreActions = _scoreActions.Sum(x => x.Score * x.Multiplier) / _scoreActions.Count;
                return "Durchschnitt: " + summedScoreActions;
            }
        }
    }
}