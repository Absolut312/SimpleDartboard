using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartGameViewModel : IContentViewModel
    {
        IPlayerScoreBoardViewModel SelectedPlayer { get; set; }
        IPlayerScoreBoardViewModel OpponentPlayer { get; set; }
        IDartBoardScoreInputViewModel DartBoardScoreInput { get; set; }
        ICommand SwitchPlayer { get; set; }
        ICommand ResetGame { get; set; }
    }
}