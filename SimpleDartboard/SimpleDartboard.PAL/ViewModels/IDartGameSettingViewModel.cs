using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartGameSettingViewModel: IContentViewModel
    {
        ICommand StartGameCommand { get; set; }
        string PlayerOneName { get; set; }
        string PlayerTwoName { get; set; }
        int PlayerOneScore { get; set; }
        int PlayerTwoScore { get; set; }
        
        int PlayerOneLegAmount { get; set; }
        int PlayerTwoLegAmount { get; set; }
        
        bool PlayerOneIsFirstSelected { get; set; }
        bool PlayerTwoIsFirstSelected { get; set; }
        
    }
}