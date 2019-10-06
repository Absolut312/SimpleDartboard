using System;
using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartBoardScoreInputViewModel
    {
        IScoreActionViewModel SelectedScoreAction { get; set; }
        ICommand AddScoreActionForSelectedPlayerCommand { get; set; }
    }
}