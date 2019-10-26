using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartGameViewModel : IContentViewModel
    {
        IPlayerScoreBoardViewModel SelectedPlayer { get; set; }
        IPlayerScoreBoardViewModel OpponentPlayer { get; set; }
        IDartBoardScoreControlWrapperViewModel DartBoardScoreControl { get; set; }
        IDartGameControlViewModel DartGameControlViewModel { get; set; }
    }
}