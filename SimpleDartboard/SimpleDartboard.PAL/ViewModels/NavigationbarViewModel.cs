using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.UseCases.DartGameSettings.Load;

namespace SimpleDartboard.PAL.ViewModels
{
    public class NavigationbarViewModel : BaseViewModel, INavigationbarViewModel
    {
        private IDartGameSettingViewModel _dartGameSettingViewModel;
        private IDartGameViewModel _dartGameViewModel;
        private IDartGameWinnerViewModel _dartGameWinnerViewModel;
        private readonly IDartGameSettingLoadService _dartGameSettingLoadService;

        public NavigationbarViewModel(IDartGameViewModel dartGameViewModel,
            IDartGameSettingViewModel dartGameSettingViewModel,
            IDartGameWinnerViewModel dartGameWinnerViewModel, 
            IDartGameSettingLoadService dartGameSettingLoadService)
        {
            _dartGameViewModel = dartGameViewModel;
            _dartGameSettingViewModel = dartGameSettingViewModel;
            _dartGameWinnerViewModel = dartGameWinnerViewModel;
            _dartGameSettingLoadService = dartGameSettingLoadService;
            ChangeToDartGameCommand = new RelayCommand(ChangeToDartGame);
            StartNewGameCommand = new RelayCommand(StartNewGame);
            ResumeLastGameCommand = new RelayCommand(ResumeLastGame);
            Mediator.Register(MessageType.StartGame, StartGame);
            Mediator.Register(MessageType.InitializeNewGame, InitializeNewGame);
            Mediator.Register(MessageType.ShowWinner, NavigateToShowWinnerView);
        }

        private void ResumeLastGame()
        {
            var dartGameSetting = _dartGameSettingLoadService.Load("LastGameState.json");
            Mediator.NotifyColleagues(MessageType.StartGame, dartGameSetting);
            Mediator.NotifyColleagues(MessageType.HideNavigationbar, null);
        }

        private void NavigateToShowWinnerView(object obj)
        {
            Mediator.NotifyColleagues(MessageType.ChangeMainViewContent, _dartGameWinnerViewModel);
        }

        private void InitializeNewGame(object obj)
        {
            StartNewGame();
        }

        private void StartGame(object obj)
        {
            ChangeToDartGame();
        }

        private void ChangeToDartGame()
        {
            Mediator.NotifyColleagues(MessageType.ChangeMainViewContent, _dartGameViewModel);
        }

        private void StartNewGame()
        {
            Mediator.NotifyColleagues(MessageType.ChangeMainViewContent, _dartGameSettingViewModel);
            Mediator.NotifyColleagues(MessageType.HideNavigationbar, null);
        }

        public ICommand ChangeToDartGameCommand { get; set; }
        public ICommand StartNewGameCommand { get; set; }
        public ICommand ResumeLastGameCommand { get; set; }
    }
}