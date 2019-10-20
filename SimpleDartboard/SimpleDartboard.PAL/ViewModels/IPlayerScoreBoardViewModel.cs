using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IPlayerScoreBoardViewModel : IContentViewModel
    {
        string Name { get; set; }
        int CurrentScore { get; set; }
        void AddScoreAction(ScoreAction scoreAction);
        void Checkout();
        void UndoLastScoreAction();
        IAverageScoreActionViewModel AverageScoreActionsPerRound { get; set; }
        IAverageScoreActionViewModel AverageScoreActionsTotal { get; set; }
    }
}