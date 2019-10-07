using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartGameWinnerViewModel : IContentViewModel
    {
        IPlayerScoreBoardViewModel PlayerScoreBoardViewModel { get; set; }
        ICommand StartNewGameCommand { get; set; }
    }
}