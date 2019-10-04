using System;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.Models
{
    public class ScoreAction : BaseViewModel
    {
        private int _actionIndex;

        public string ActionIndex
        {
            get { return _actionIndex.ToString() + "."; }
            set
            {
                _actionIndex = Convert.ToInt32(value);
                OnPropertyChanged("ActionIndex");
            }
        }

        private int _score;

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }
    }
}