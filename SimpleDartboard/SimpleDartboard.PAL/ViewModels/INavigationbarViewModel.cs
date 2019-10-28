using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface INavigationbarViewModel
    {
        ICommand ChangeToDartGameCommand { get; set; }
        ICommand StartNewGameCommand { get; set; }
    }
}