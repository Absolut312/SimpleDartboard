namespace SimpleDartboard.PAL.ViewModels
{
    public interface IScoreActionViewModel
    {
        int Score { get; set; }
        int ScoreMultiplier { get; set; }
        string ScoreActionString { get; }
    }
}