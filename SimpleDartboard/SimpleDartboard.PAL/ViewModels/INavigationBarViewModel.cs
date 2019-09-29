using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface INavigationBarViewModel
    {
        ICommand ChangeToDummyPlayerScoreBoard { get; set; }
        ICommand ChangeToDummyDartBoardInput { get; set; }
    }
}