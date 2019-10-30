using System.Collections.Generic;
using System.Windows.Media;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.Models;
using SimpleDartboard.PAL.UseCases.DartGameSettings.Save;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartGameViewModel : BaseViewModel, IDartGameViewModel
    {
        private DartGameSetting _dartGameSetting;
        private IPlayerScoreBoardViewModel _playerOne;
        private IPlayerScoreBoardViewModel _playerTwo;
        private IPlayerScoreBoardViewModel _selectedPlayer;
        private IPlayerScoreBoardViewModel _opponentPlayer;
        private readonly IDartGameSettingSaveService _dartGameSettingSaveService;

        public IPlayerScoreBoardViewModel LeftPlayer
        {
            get { return _playerOne; }
            set
            {
                _playerOne = value;
                OnPropertyChanged("LeftPlayer");
            }
        }

        public IPlayerScoreBoardViewModel RightPlayer
        {
            get { return _playerTwo; }
            set
            {
                _playerTwo = value;
                OnPropertyChanged("RightPlayer");
            }
        }

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

        private IDartBoardScoreControlWrapperViewModel _dartBoardScoreControl;

        public IDartBoardScoreControlWrapperViewModel DartBoardScoreControl
        {
            get { return _dartBoardScoreControl; }
            set
            {
                _dartBoardScoreControl = value;
                OnPropertyChanged("DartBoardScoreControl");
            }
        }

        public IDartGameControlViewModel DartGameControlViewModel
        {
            get => _dartGameControlViewModel;
            set
            {
                _dartGameControlViewModel = value;
                OnPropertyChanged("DartGameControlViewModel");
            }
        }

        public int LeftPlayerSelectedAnimationIndex
        {
            get => _leftPlayerSelectedAnimationIndex;
            set
            {
                _leftPlayerSelectedAnimationIndex = value;
                OnPropertyChanged("LeftPlayerSelectedAnimationIndex");
            }
        }

        public int RightPlayerSelectedAnimationIndex
        {
            get => _rightPlayerSelectedAnimationIndex;
            set
            {
                _rightPlayerSelectedAnimationIndex = value;
                OnPropertyChanged("RightPlayerSelectedAnimationIndex");
            }
        }

        public IActionTokenViewModel SelectedPlayerToken
        {
            get => _selectedPlayerToken;
            set
            {
                _selectedPlayerToken = value;
                OnPropertyChanged("SelectedPlayerToken");
            }
        }

        private IDartGameControlViewModel _dartGameControlViewModel;
        private IActionTokenViewModel _selectedPlayerToken;
        private int _rightPlayerSelectedAnimationIndex;
        private int _leftPlayerSelectedAnimationIndex;

        public DartGameViewModel(IPlayerScoreBoardViewModel playerOne,
            IPlayerScoreBoardViewModel playerTwo,
            IDartBoardScoreControlWrapperViewModel dartBoardScoreControlViewModel,
            IDartGameControlViewModel dartGameControlViewModel, IDartGameSettingSaveService dartGameSettingSaveService)
        {
            var selectedPlayerToken = new ActionTokenViewModel();
            selectedPlayerToken.TokenColor = Brushes.Red;
            selectedPlayerToken.Size = 48;
            SelectedPlayerToken = selectedPlayerToken;
            LeftPlayerSelectedAnimationIndex = UnvisbileContent;
            RightPlayerSelectedAnimationIndex = UnvisbileContent;
            _dartGameSetting = new DartGameSetting();
            LeftPlayer = playerOne;
            RightPlayer = playerTwo;
            DartBoardScoreControl = dartBoardScoreControlViewModel;
            DartGameControlViewModel = dartGameControlViewModel;
            _dartGameSettingSaveService = dartGameSettingSaveService;
            SelectedPlayer = LeftPlayer;
            OpponentPlayer = RightPlayer;
            RegisterMediatorMessages();
        }

        private static int UnvisbileContent => -1;
        private static int VisibleSelectedTokenIndex => 0;

        private void RegisterMediatorMessages()
        {
            Mediator.Register(MessageType.SwichtSelectedPlayer, SwitchSelectedPlayer);
            Mediator.Register(MessageType.ReduceScoreForSelectedPlayer, ReduceScoreForSelectedPlayer);
            Mediator.Register(MessageType.StartGame, InitializeGameSetting);
            Mediator.Register(MessageType.UndoLastScoreAction, UndoLastScoreAction);
        }

        private void UndoLastScoreAction(object obj)
        {
            SelectedPlayer.UndoLastScoreAction();
        }

        private void InitializeGameSetting(object gameSetting)
        {
            var dartGameSetting = gameSetting as DartGameSetting;
            if (dartGameSetting == null) return;
            _dartGameSetting = dartGameSetting;
            _playerOne.Name = dartGameSetting.PlayerOne.Name;
            _playerOne.CurrentScore = dartGameSetting.PlayerOne.Score;
            InitializePlayerAverageScoreActions(_playerOne, dartGameSetting.PlayerOne.ScoreActions);
            InitializePlayerLegs(_playerOne, dartGameSetting.PlayerOne.LegAmount);
            _playerTwo.Name = dartGameSetting.PlayerTwo.Name;
            _playerTwo.CurrentScore = dartGameSetting.PlayerTwo.Score;
            InitializePlayerAverageScoreActions(_playerTwo, dartGameSetting.PlayerTwo.ScoreActions);
            InitializePlayerLegs(_playerTwo, dartGameSetting.PlayerTwo.LegAmount);
            if (_dartGameSetting.PlayerOne.IsFirstSelected)
            {
                SelectedPlayer = LeftPlayer;
                OpponentPlayer = RightPlayer;
                LeftPlayerSelectedAnimationIndex = VisibleSelectedTokenIndex;
                RightPlayerSelectedAnimationIndex = UnvisbileContent;
            }
            else
            {
                SelectedPlayer = RightPlayer;
                OpponentPlayer = LeftPlayer;
                LeftPlayerSelectedAnimationIndex = UnvisbileContent;
                RightPlayerSelectedAnimationIndex = VisibleSelectedTokenIndex;
            }

            ClearActionTokens();
        }

        private void InitializePlayerAverageScoreActions(IPlayerScoreBoardViewModel player, List<ScoreAction> scoreActions)
        {
            foreach (var scoreAction in scoreActions)
            {
                player.AverageScoreActionsTotal.AddScoreAction(scoreAction);
            }
        }

        private void InitializePlayerLegs(IPlayerScoreBoardViewModel player, int legAmount)
        {
            player.Legs.Clear();
            for (var i = 0; i < legAmount; i++)
            {
                player.AddLeg();
            }
        }

        private void ReduceScoreForSelectedPlayer(object scoreActionObject)
        {
            if (!(scoreActionObject is ScoreAction scoreAction)) return;
            SelectedPlayer.AddScoreAction(scoreAction);
        }

        private void SwitchSelectedPlayer(object obj)
        {
            SelectedPlayer.Checkout();
            if (SelectedPlayer.CurrentScore == 0)
            {
                SelectedPlayer.AddLeg();
                Mediator.NotifyColleagues(MessageType.ShowWinner, SelectedPlayer);
                return;
            }

            var currentSelectedPlayer = SelectedPlayer;
            SelectedPlayer = OpponentPlayer;
            OpponentPlayer = currentSelectedPlayer;
            LeftPlayerSelectedAnimationIndex = LeftPlayerSelectedAnimationIndex == UnvisbileContent
                ? VisibleSelectedTokenIndex
                : UnvisbileContent;
            RightPlayerSelectedAnimationIndex = RightPlayerSelectedAnimationIndex == UnvisbileContent
                ? VisibleSelectedTokenIndex
                : UnvisbileContent;
            ClearActionTokens();
            UpdateCurrentGameStateInGameSetting();
            _dartGameSettingSaveService.Save(_dartGameSetting, "LastGameState.json");
        }

        private void UpdateCurrentGameStateInGameSetting()
        {
            _dartGameSetting.PlayerOne.LegAmount = LeftPlayer.Legs.Count;
            _dartGameSetting.PlayerOne.ScoreActions = LeftPlayer.GetScoreActions();
            _dartGameSetting.PlayerOne.IsFirstSelected = LeftPlayerSelectedAnimationIndex == VisibleSelectedTokenIndex;
            _dartGameSetting.PlayerTwo.LegAmount = RightPlayer.Legs.Count;
            _dartGameSetting.PlayerTwo.ScoreActions = RightPlayer.GetScoreActions();
            _dartGameSetting.PlayerTwo.IsFirstSelected = RightPlayerSelectedAnimationIndex == VisibleSelectedTokenIndex;
        }

        private void ClearActionTokens()
        {
            Mediator.NotifyColleagues(MessageType.SetIsDartboardScoreInputActive, true);
            Mediator.NotifyColleagues(MessageType.ClearActionTokens, null);
        }
    }
}