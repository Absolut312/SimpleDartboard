using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class ScoreActionViewModel : BaseViewModel, IScoreActionViewModel
    {
        private int _scoreMultiplier = 1;
        private int _score = 1;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged("Score");
                OnPropertyChanged("ScoreActionString");
            }
        }

        public int ScoreMultiplier
        {
            get => _scoreMultiplier;
            set
            {
                _scoreMultiplier = value;
                OnPropertyChanged("ScoreMultiplier");
                OnPropertyChanged("ScoreActionString");
            }
        }

        public string ScoreActionString
        {
            get
            {
                var muliplierText = "";
                switch (ScoreMultiplier)
                {
                    case 2:
                    {
                        muliplierText += "D";
                        break;
                    }
                    case 3:
                    {
                        muliplierText += "T";
                        break;
                    }
                }
                return muliplierText + Score;
            }
        }
    }
}