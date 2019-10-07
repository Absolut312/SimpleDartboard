using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartGameControlViewModel
    {
        ICommand SwitchPlayerCommand { get; set; }
        ICommand ResetGameCommand { get; set; }
        ICommand UndoLastScoreActionCommand { get; set; }
    }
}