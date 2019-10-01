using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface INavigationBarViewModel
    {
        ICommand ChangeToDartGameCommand { get; set; }
        ICommand StartNewGameCommand { get; set; }
    }
}