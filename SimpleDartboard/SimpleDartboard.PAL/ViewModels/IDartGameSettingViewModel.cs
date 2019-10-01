using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartGameSettingViewModel: IContentViewModel
    {
        ICommand StartGameCommand { get; set; }
        string PlayerOneName { get; set; }
        string PlayerTwoName { get; set; }
        int StartingScore { get; set; }
        
    }
}