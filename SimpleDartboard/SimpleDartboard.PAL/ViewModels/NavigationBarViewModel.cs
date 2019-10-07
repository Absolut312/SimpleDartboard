using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class NavigationBarViewModel : BaseViewModel, INavigationBarViewModel
    {
        private IDartGameSettingViewModel _dartGameSettingViewModel;
        private IDartGameViewModel _dartGameViewModel;
        public NavigationBarViewModel(IDartGameViewModel dartGameViewModel, 
            IDartGameSettingViewModel dartGameSettingViewModel)
        {
            _dartGameViewModel = dartGameViewModel;
            _dartGameSettingViewModel = dartGameSettingViewModel;
            ChangeToDartGameCommand = new RelayCommand(ChangeToDartGame);
            StartNewGameCommand = new RelayCommand(StartNewGame);
            Mediator.Register(MessageType.StartGame, StartGame);
            Mediator.Register(MessageType.InitializeNewGame, InitializeNewGame);
            
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