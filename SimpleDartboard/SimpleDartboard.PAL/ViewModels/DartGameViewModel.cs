using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartGameViewModel : BaseViewModel, IDartGameViewModel
    {
        private DartGameSetting _dartGameSetting;
        private IPlayerScoreBoardViewModel _playerOne;
        private IPlayerScoreBoardViewModel _playerTwo;
        private IPlayerScoreBoardViewModel _selectedPlayer;
        private IPlayerScoreBoardViewModel _opponentPlayer;

        public IPlayerScoreBoardViewModel SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                _selectedPlayer = value;
                OnPropertyChanged("SelectedPlayer");
            }
        }

        public IPlayerScoreBoardViewModel OpponentPlayer
        {
            get { return _opponentPlayer; }
            set
            {
                _opponentPlayer = value;
                OnPropertyChanged("OpponentPlayer");
            }
        }

        private IDartBoardScoreControlViewModel _dartBoardScoreControl;

        public IDartBoardScoreControlViewModel DartBoardScoreControl
        {
            get { return _dartBoardScoreControl; }
            set
            {
                _dartBoardScoreControl = value;
                OnPropertyChanged("DartBoardScoreControl");
            }
        }

        public ICommand SwitchPlayerCommand { get; set; }
        public ICommand ResetGameCommand { get; set; }
        public ICommand UndoLastScoreActionCommand { get; set; }

        public DartGameViewModel(IPlayerScoreBoardViewModel playerOne,
            IPlayerScoreBoardViewModel playerTwo,
            IDartBoardScoreControlViewModel dartBoardScoreControlViewModel)
        {
            _dartGameSetting = new DartGameSetting();
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            DartBoardScoreControl = dartBoardScoreControlViewModel;
            ResetGameCommand = new RelayCommand(ResetGame);
            SwitchPlayerCommand = new RelayCommand(SwitchSelectedPlayer);
            UndoLastScoreActionCommand = new RelayCommand(UndoLastScoreActionCommant);
            SelectedPlayer = _playerOne;
            OpponentPlayer = _playerTwo;
            Mediator.Register(MessageType.ReduceScoreForSelectedPlayer, ReduceScoreForSelectedPlayer);
            Mediator.Register(MessageType.StartGame, InitializeGameSetting);
        }

        private void UndoLastScoreActionCommant()
        {
            //Todo: Reimplement
        }

        private void InitializeGameSetting(object gameSetting)
        {
            var dartGameSetting = gameSetting as DartGameSetting;
            if (dartGameSetting == null) return;
            _dartGameSetting = dartGameSetting;
            _playerOne.Name = dartGameSetting.PlayerOneName;
            _playerOne.CurrentScore = dartGameSetting.StartingScore;
            _playerTwo.Name = dartGameSetting.PlayerTwoName;
            _playerTwo.CurrentScore = dartGameSetting.StartingScore;
        }

        private void ReduceScoreForSelectedPlayer(object scoreActionObject)
        {
            if (!(scoreActionObject is ScoreAction scoreAction)) return;
            SelectedPlayer.AddScoreAction(scoreAction);
            if (SelectedPlayer.CurrentScore == 1 || 
                SelectedPlayer.CurrentScore == 0 && scoreAction.Multiplier != 2)
            {
                scoreAction.Multiplier *= -1;
                SelectedPlayer.AddScoreAction(scoreAction);
            }
            //TODO: Revert all Round ScoreActions
            
        }

        private void SwitchSelectedPlayer()
        {
            var currentSelectedPlayer = SelectedPlayer;
            SelectedPlayer = OpponentPlayer;
            OpponentPlayer = currentSelectedPlayer;
        }

        private void ResetGame()
        {
            Mediator.NotifyColleagues(MessageType.StartGame, _dartGameSetting);
        }
    }
}