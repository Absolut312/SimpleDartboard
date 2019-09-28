using System.Collections.ObjectModel;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class PlayerScoreBoardViewModel : BaseViewModel, IPlayerScoreBoardViewModel
    {
        public string Name { get; set; }
        public int CurrentScore { get; set; }
        public ObservableCollection<int> ScoreActions { get; set; }
    }
}