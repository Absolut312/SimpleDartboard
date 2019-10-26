using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartBoardScoreInputViewModel : BaseViewModel, IDartBoardScoreInputViewModel
    {
        public ICommand AddScoreActionForSelectedPlayerCommand { get; set; }
        public ObservableCollection<IActionTokenViewModel> ActionTokens { get; set; }

        public void RemoveLastActionToken()
        {
            if (ActionTokens.Count <= 0) return;
            ActionTokens.RemoveAt(ActionTokens.Count - 1);
        }

        private bool _isInputEnabled = true;

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
            AddScoreActionForSelectedPlayerCommand =
                new RelayCommand(ReduceCurrentScoreForSelectedPlayer, () => ActionTokens.Count < 3 && _isInputEnabled);
            ActionTokens = new ObservableCollection<IActionTokenViewModel>();
            Mediator.Register(MessageType.RemoveLastActionToken, RemoveLastActionToken);
            Mediator.Register(MessageType.ClearActionTokens, ClearActionTokens);
            Mediator.Register(MessageType.SetIsDartboardScoreInputActive, SetIsDartboardInputActive);
        }

        private void ClearActionTokens(object obj)
        {
            ActionTokens.Clear();
        }

        private void SetIsDartboardInputActive(object booleanObject)
        {
            _isInputEnabled = (bool) booleanObject;
        }

        private void RemoveLastActionToken(object scoreActionObject)
        {
            if (!(scoreActionObject is ScoreAction scoreAction)) return;
            if (scoreAction.Score == _selectedScoreAction.Score &&
                scoreAction.Multiplier == _selectedScoreAction.ScoreMultiplier)
            {
                RemoveLastActionToken();
            }
        }

        private void ReduceCurrentScoreForSelectedPlayer()
        {
            var actionToken = new ActionTokenViewModel();
            ActionTokens.Add(actionToken);
            Mediator.NotifyColleagues(MessageType.ReduceScoreForSelectedPlayer,
                new ScoreAction
                    {Multiplier = _selectedScoreAction.ScoreMultiplier, Score = _selectedScoreAction.Score});
        }
    }
}