using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IDartBoardScoreInputViewModel
    {
        IScoreActionViewModel SelectedScoreAction { get; set; }
        ICommand AddScoreActionForSelectedPlayerCommand { get; set; }
        ObservableCollection<IActionTokenViewModel> ActionTokens { get; set; }
        void RemoveLastActionToken();
    }
}