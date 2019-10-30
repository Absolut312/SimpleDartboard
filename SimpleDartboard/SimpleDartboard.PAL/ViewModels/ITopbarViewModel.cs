using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface ITopbarViewModel
    {
        bool IsMenuButtonVisible { get; set; }
        ICommand ToggleMenuButtonCommand { get; set; }
        ICommand ShutdownCommand { get; set; }
        ICommand ResumeLastGameCommand { get; set; }
    }
}