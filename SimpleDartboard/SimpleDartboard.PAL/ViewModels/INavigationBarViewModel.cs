using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface INavigationBarViewModel
    {
        ICommand ChangeToDartGame { get; set; }
    }
}