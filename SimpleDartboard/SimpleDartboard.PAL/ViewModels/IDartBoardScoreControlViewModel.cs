using System.Collections.ObjectModel;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartBoardScoreControlViewModel
    {
        ObservableCollection<IDartBoardScoreInputViewModel> ScoreInputActions { get; set; }
        void AddScoreInputAction(int scoreAction, int multiplier);
    }
}