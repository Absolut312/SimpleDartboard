using System.Collections.ObjectModel;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartBoardScoreControlViewModel : IDartBoardScoreControlViewModel
    {
        public DartBoardScoreControlViewModel()
        {
            ScoreInputActions = new ObservableCollection<IDartBoardScoreInputViewModel>();
            for (var i = 1; i < 21; i++)
            {
                AddScoreInputAction(i, 1);
                AddScoreInputAction(i, 2);
                AddScoreInputAction(i, 3);
            }
            AddScoreInputAction(25,1);
            AddScoreInputAction(25,2);
        }

        private void AddScoreInputAction(int scoreAction, int multiplier)
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