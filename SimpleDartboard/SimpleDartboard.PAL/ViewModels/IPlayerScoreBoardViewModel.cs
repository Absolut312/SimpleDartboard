using System.Collections.ObjectModel;
using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IPlayerScoreBoardViewModel: IContentViewModel
    {
        string Name { get; set; }
        int CurrentScore { get; set; }
        void AddScoreAction(ScoreAction scoreAction);
    }
}