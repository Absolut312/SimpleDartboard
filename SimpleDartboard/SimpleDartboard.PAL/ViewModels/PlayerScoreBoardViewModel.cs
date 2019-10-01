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
            ScoreActions = new ObservableCollection<int>();
        }

        private ObservableCollection<int> _scoreActions;

        public ObservableCollection<int> ScoreActions
        {
            get => _scoreActions;
            set
            {
                _scoreActions = value;
                OnPropertyChanged("ScoreActions");
                OnPropertyChanged("CurrentScore");
            }
        }

        public void AddScoreAction(int scoreAction)
        {
            if (scoreAction <= 0) return;
            ScoreActions.Add(scoreAction);
            if (CurrentScore < 0 || CurrentScore == 0 && !IsLastScoreActionDoubleHit())
            {
                ScoreActions.RemoveAt(ScoreActions.Count - 1);
            }
            OnPropertyChanged("CurrentScore");
        }

        private bool IsLastScoreActionDoubleHit()
        {
            return ScoreActions[ScoreActions.Count - 1] == ScoreActions[ScoreActions.Count - 2];
        }

        public void ClearScoreActions()
        {
            ScoreActions.Clear();
            OnPropertyChanged("CurrentScore");
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
            get { return _currentScore - ScoreActions.Sum(); }

            set
            {
                _currentScore = value;
                OnPropertyChanged("CurrentScore");
            }
        }
    }
}