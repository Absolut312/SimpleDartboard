using System.Collections.ObjectModel;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartBoardScoreControlViewModel : IDartBoardScoreControlViewModel
    {
        public DartBoardScoreControlViewModel()
        {
            ScoreInputActions = new ObservableCollection<IDartBoardScoreInputViewModel>();
        }

        public void AddScoreInputAction(int scoreAction, int multiplier)
        {
            var inputAction = (IDartBoardScoreInputViewModel) new DartBoardScoreInputViewModel();
            inputAction.SelectedScoreAction = new ScoreActionViewModel();
            inputAction.SelectedScoreAction.Score = scoreAction;
            inputAction.SelectedScoreAction.ScoreMultiplier = multiplier;
            ScoreInputActions.Add(inputAction);
        }

        public ObservableCollection<IDartBoardScoreInputViewModel> ScoreInputActions { get; set; }
    }
}