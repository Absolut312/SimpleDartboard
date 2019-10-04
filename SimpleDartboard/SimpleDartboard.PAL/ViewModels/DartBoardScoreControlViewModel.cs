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
                AddScoreInputAction(i);
            }
            AddScoreInputAction(25);
        }

        private void AddScoreInputAction(int scoreAction)
        {
            var inputAction = (IDartBoardScoreInputViewModel) new DartBoardScoreInputViewModel();
            inputAction.SelectedScoreAction = scoreAction;
            ScoreInputActions.Add(inputAction);
        }

        public ObservableCollection<IDartBoardScoreInputViewModel> ScoreInputActions { get; set; }
    }
}