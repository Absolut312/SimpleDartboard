using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class NavigationBarViewModel : BaseViewModel, INavigationBarViewModel
    {
        private IPlayerScoreBoardViewModel _playerScoreBoardViewModel;
        private IDartBoardScoreInputViewModel _dartBoardScoreInputViewModel;
        public ICommand ChangeToDummyPlayerScoreBoard { get; set; }
        public ICommand ChangeToDummyDartBoardInput { get; set; }

        public NavigationBarViewModel(IPlayerScoreBoardViewModel playerScoreBoardViewModel,
            IDartBoardScoreInputViewModel dartBoardScoreInputViewModel)
        {
            _playerScoreBoardViewModel = playerScoreBoardViewModel;
            _dartBoardScoreInputViewModel = dartBoardScoreInputViewModel;
            ChangeToDummyPlayerScoreBoard = new RelayCommand(SendRouteMessageForPlayerScoreboard);
            ChangeToDummyDartBoardInput = new RelayCommand(SendRouteMessageForDartBoardInput);
        }

        private void SendRouteMessageForPlayerScoreboard()
        {
            _playerScoreBoardViewModel.Name = "Player";
            _playerScoreBoardViewModel.CurrentScore += 1;
            Mediator.NotifyColleagues(MessageType.ChangeMainViewContent, _playerScoreBoardViewModel);
        }

        private void SendRouteMessageForDartBoardInput()
        {
            Mediator.NotifyColleagues(MessageType.ChangeMainViewContent, _dartBoardScoreInputViewModel);
        }
    }
}