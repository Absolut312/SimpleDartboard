using System.Collections.ObjectModel;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartBoardScoreControlWrapperViewModel
    {
        ObservableCollection<IDartBoardScoreControlViewModel> DartBoardScoreControlViewModels { get; }
    }
}