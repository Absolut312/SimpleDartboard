using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartBoardScoreInputViewModel : BaseViewModel, IDartBoardScoreInputViewModel
    {
        public ICommand AddScoreActionForSelectedPlayerCommand { get; set; }
        private IScoreActionViewModel _selectedScoreAction;

        public IScoreActionViewModel SelectedScoreAction
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
            _selectedScoreAction = new ScoreActionViewModel();
            AddScoreActionForSelectedPlayerCommand = new RelayCommand(ReduceCurrentScoreForSelectedPlayer);
        }

        private void ReduceCurrentScoreForSelectedPlayer()
        {
            Mediator.NotifyColleagues(MessageType.ReduceScoreForSelectedPlayer, _selectedScoreAction.Score*_selectedScoreAction.ScoreMultiplier);
        }
    }
}