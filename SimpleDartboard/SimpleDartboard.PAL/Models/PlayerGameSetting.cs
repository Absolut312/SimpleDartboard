using System.Collections.Generic;

namespace SimpleDartboard.PAL.Models
{
    public class PlayerGameSetting
    {
        public string Name = "Player";
        public int Score = 501;
        public int LegAmount = 0;
        public bool IsFirstSelected;
        public List<ScoreAction> ScoreActions = new List<ScoreAction>();
    }
}