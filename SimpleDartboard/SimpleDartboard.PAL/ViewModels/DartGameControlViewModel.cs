using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartGameControlViewModel : BaseViewModel, IDartGameControlViewModel
    {
        public ICommand SwitchPlayerCommand { get; set; }
        public ICommand ResetGameCommand { get; set; }
        public ICommand UndoLastScoreActionCommand { get; set; }


        private bool _isUndoLastActionDisabled = true;
        private DartGameSetting _dartGameSetting;

        public DartGameControlViewModel()
        {
            ResetGameCommand = new RelayCommand(ResetGame);
            SwitchPlayerCommand = new RelayCommand(SwitchPlayer);
            UndoLastScoreActionCommand = new RelayCommand(UndoLastScoreAction, () => !_isUndoLastActionDisabled);
            Mediator.Register(MessageType.StartGame, InitializeGameSetting);
            Mediator.Register(MessageType.DisableUndoLastScoreAction, DisableUndoLastScoreActionCommand);
        }

        private void DisableUndoLastScoreActionCommand(object obj)
        {
            _isUndoLastActionDisabled = (bool) obj;
        }

        private void UndoLastScoreAction()
        {
            Mediator.NotifyColleagues(MessageType.UndoLastScoreAction, null);
        }

        private void SwitchPlayer()
        {
            Mediator.NotifyColleagues(MessageType.SwichtSelectedPlayer, null);
        }

        private void InitializeGameSetting(object gameSetting)
        {
            var dartGameSetting = gameSetting as DartGameSetting;
            if (dartGameSetting == null) return;
            _dartGameSetting = dartGameSetting;
        }

        private void ResetGame()
        {
            _dartGameSetting.PlayerOne.ScoreActions.Clear();
            _dartGameSetting.PlayerTwo.ScoreActions.Clear();
            Mediator.NotifyColleagues(MessageType.StartGame, _dartGameSetting);
        }
    }
}