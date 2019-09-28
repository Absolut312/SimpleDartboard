using System.Collections.ObjectModel;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IPlayerScoreBoardViewModel: IContentViewModel
    {
        string Name { get; set; }
        int CurrentScore { get; set; }
    }
}