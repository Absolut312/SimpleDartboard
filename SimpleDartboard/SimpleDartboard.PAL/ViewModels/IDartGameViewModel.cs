using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartGameViewModel : IContentViewModel
    {
        IPlayerScoreBoardViewModel SelectedPlayer { get; set; }
        IPlayerScoreBoardViewModel OpponentPlayer { get; set; }
        IDartBoardScoreInputViewModel DartBoardScoreInput { get; set; }
        ICommand SwitchPlayerCommand { get; set; }
        ICommand ResetGameCommand { get; set; }
    }
}