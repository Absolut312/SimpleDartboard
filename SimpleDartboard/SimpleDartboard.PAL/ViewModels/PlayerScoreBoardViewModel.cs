using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.ViewModels
{
    public class PlayerScoreBoardViewModel : BaseViewModel, IPlayerScoreBoardViewModel
    {
        private string _name;

        public PlayerScoreBoardViewModel()
        {
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
        }
    }
}