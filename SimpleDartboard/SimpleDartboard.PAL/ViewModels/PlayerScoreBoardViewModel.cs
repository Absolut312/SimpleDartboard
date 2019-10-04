using System;
using System.Collections.ObjectModel;
using System.Linq;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class PlayerScoreBoardViewModel : BaseViewModel, IPlayerScoreBoardViewModel
    {
        private string _name;

        public PlayerScoreBoardViewModel()
        {
            ScoreActions = new ObservableCollection<ScoreAction>();
        }

        private ObservableCollection<ScoreAction> _scoreActions;

        public ObservableCollection<ScoreAction> ScoreActions
        {
            get => _scoreActions;
            set
            {
                _scoreActions = value;
                OnPropertyChanged("ScoreActions");
                OnPropertyChanged("CurrentScore");
            }
        }

        public void AddScoreAction(int score)
        {
            if (score <= 0) return;
            var scoreActionToAdd = new ScoreAction();
            scoreActionToAdd.Score = score;
            scoreActionToAdd.ActionIndex = Convert.ToString(ScoreActions.Count + 1);
            ScoreActions.Insert(0, scoreActionToAdd);
            if (CurrentScore < 0 || CurrentScore == 0 && !IsLastScoreActionDoubleHit())
            {
                UndoLastScoreAction();
            }

            OnPropertyChanged("CurrentScore");
        }

        private bool IsLastScoreActionDoubleHit()
        {
            return ScoreActions[0] == ScoreActions[1];
        }

        public void ClearScoreActions()
        {
            ScoreActions.Clear();
            OnPropertyChanged("CurrentScore");
        }

        public void UndoLastScoreAction()
        {
            ScoreActions.RemoveAt(0);
            OnPropertyChanged("CurrentScore");
        }

        public bool HasScoreActions()
        {
            return ScoreActions.Count > 0;
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
            get { return _currentScore - ScoreActions.Sum(x => x.Score); }

            set
            {
                _currentScore = value;
                OnPropertyChanged("CurrentScore");
            }
        }
    }
}