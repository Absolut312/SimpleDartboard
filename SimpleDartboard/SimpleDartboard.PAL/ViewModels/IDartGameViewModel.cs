using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartGameViewModel : IContentViewModel
    {
        IPlayerScoreBoardViewModel SelectedPlayer { get; set; }
        IPlayerScoreBoardViewModel OpponentPlayer { get; set; }
        IDartBoardScoreControlViewModel DartBoardScoreControl { get; set; }
        ICommand SwitchPlayerCommand { get; set; }
        ICommand ResetGameCommand { get; set; }
        ICommand UndoLastScoreActionCommand { get; set; }
        string AverageScore { get; }
    }
}