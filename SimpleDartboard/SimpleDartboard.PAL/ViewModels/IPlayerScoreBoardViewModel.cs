using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IPlayerScoreBoardViewModel : IContentViewModel
    {
        ObservableCollection<ActionTokenViewModel> Legs { get; }
        void AddLeg();
        string Name { get; set; }
        int CurrentScore { get; set; }
        void AddScoreAction(ScoreAction scoreAction);
        List<ScoreAction> GetScoreActions();
        void Checkout();
        void UndoLastScoreAction();
        IAverageScoreActionViewModel AverageScoreActionsPerRound { get; set; }
        IAverageScoreActionViewModel AverageScoreActionsTotal { get; set; }
    }
}