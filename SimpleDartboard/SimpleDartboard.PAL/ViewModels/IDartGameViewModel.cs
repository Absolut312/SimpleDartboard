using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartGameViewModel : IContentViewModel
    {
        IPlayerScoreBoardViewModel SelectedPlayer { get; set; }
        IPlayerScoreBoardViewModel OpponentPlayer { get; set; }
        IDartBoardScoreControlViewModel DartBoardScoreControl { get; set; }
        IDartGameControlViewModel DartGameControlViewModel { get; set; }
        string AverageScore { get; }
        string TotalScore { get; }
    }
}