using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface INavigationBarViewModel
    {
        ICommand ChangeToDummyPlayerScoreboard { get; set; }
    }
}