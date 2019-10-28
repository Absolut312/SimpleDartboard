using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class NavigationbarViewModel : BaseViewModel, INavigationbarViewModel
    {
        private IDartGameSettingViewModel _dartGameSettingViewModel;
        private IDartGameViewModel _dartGameViewModel;
        private IDartGameWinnerViewModel _dartGameWinnerViewModel;

        public NavigationbarViewModel(IDartGameViewModel dartGameViewModel,
            IDartGameSettingViewModel dartGameSettingViewModel,
            IDartGameWinnerViewModel dartGameWinnerViewModel)
        {
            _dartGameViewModel = dartGameViewModel;
            _dartGameSettingViewModel = dartGameSettingViewModel;
            _dartGameWinnerViewModel = dartGameWinnerViewModel;
            ChangeToDartGameCommand = new RelayCommand(ChangeToDartGame);
            StartNewGameCommand = new RelayCommand(StartNewGame);
            Mediator.Register(MessageType.StartGame, StartGame);
            Mediator.Register(MessageType.InitializeNewGame, InitializeNewGame);
            Mediator.Register(MessageType.ShowWinner, NavigateToShowWinnerView);
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
        }

        public ICommand ChangeToDartGameCommand { get; set; }
        public ICommand StartNewGameCommand { get; set; }
    }
}