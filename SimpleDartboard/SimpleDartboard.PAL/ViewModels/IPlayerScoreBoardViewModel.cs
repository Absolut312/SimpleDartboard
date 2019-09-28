using System.Collections.ObjectModel;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IPlayerScoreBoardViewModel
    {
        string Name { get; set; }
        int CurrentScore { get; set; }
        ObservableCollection<int> ScoreActions { get; set; }
    }
}