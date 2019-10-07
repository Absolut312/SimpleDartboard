namespace SimpleDartboard.PAL.Models
{
    public class ScoreAction
    {
        public int Score;
        public int Multiplier;
        public bool IsReverted;

        public int TotalScoreAction
        {
            get { return Score * Multiplier; }
        }
    }
}