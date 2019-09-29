using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartBoardScoreInputViewModel : BaseViewModel, IDartBoardScoreInputViewModel
    {
        public ICommand AddScoreActionForSelectedPlayer { get; set; }
        private int _selectedScoreAction;

        public int SelectedScoreAction
        {
            get { return _selectedScoreAction; }
            set
            {
                _selectedScoreAction = value;
                OnPropertyChanged("SelectedScoreAction");
            }
        }

        public DartBoardScoreInputViewModel()
        {
            _selectedScoreAction = 0;
            AddScoreActionForSelectedPlayer = new RelayCommand(ReduceCurrentScoreForSelectedPlayer);
        }

        private void ReduceCurrentScoreForSelectedPlayer()
        {
            Mediator.NotifyColleagues(MessageType.ReduceScoreForSelectedPlayer, _selectedScoreAction);
        }
    }
}