using System;
using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartBoardScoreInputViewModel: ICloneable
    {
        int SelectedScoreAction { get; set; }
        ICommand AddScoreActionForSelectedPlayerCommand { get; set; }
    }
}