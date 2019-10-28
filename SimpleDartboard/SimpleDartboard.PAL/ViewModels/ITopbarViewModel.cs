using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface ITopbarViewModel
    {
        bool IsMenuButtonVisible { get; set; }
        ICommand ToggleMenuButtonCommand { get; set; }
    }
}