using System.Collections.Generic;
using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IAverageScoreActionViewModel
    {
        ScoreAction CheckoutScoreActions();
        void AddScoreAction(ScoreAction scoreAction);
        void UndoLastScoreAction();
        void Reset();
        void RevertAllScoreActions();
        void RevertLastScoreActions();
        int AverageScoreActions { get; }
        int ScoreActionSum { get; }
        List<ScoreAction> GetAllScoreActions();
        string CommaSeparatedScoreActions { get; }
        int GetScoreActionsSum();
    }
}