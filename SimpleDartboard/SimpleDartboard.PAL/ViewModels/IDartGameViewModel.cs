using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartGameViewModel : IContentViewModel
    {
        IPlayerScoreBoardViewModel LeftPlayer { get; set; }
        IPlayerScoreBoardViewModel RightPlayer { get; set; }
        IPlayerScoreBoardViewModel SelectedPlayer { get; set; }
        IPlayerScoreBoardViewModel OpponentPlayer { get; set; }
        IDartBoardScoreControlWrapperViewModel DartBoardScoreControl { get; set; }
        IDartGameControlViewModel DartGameControlViewModel { get; set; }
        int LeftPlayerSelectedAnimationIndex { get; set; }
        int RightPlayerSelectedAnimationIndex { get; set; }

        IActionTokenViewModel SelectedPlayerToken { get; set; }
    }
}