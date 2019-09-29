using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class NavigationBarViewModel : BaseViewModel, INavigationBarViewModel
    {
        private IPlayerScoreBoardViewModel _playerScoreBoardViewModel;
        public ICommand ChangeToDummyPlayerScoreboard { get; set; }

        public NavigationBarViewModel(IPlayerScoreBoardViewModel playerScoreBoardViewModel)
        {
            _playerScoreBoardViewModel = playerScoreBoardViewModel;
            ChangeToDummyPlayerScoreboard = new RelayCommand(SendRouteMessageForPlayerScoreboard);
        }

        private void SendRouteMessageForPlayerScoreboard()
        {
            _playerScoreBoardViewModel.CurrentScore += 1;
            _playerScoreBoardViewModel.Name = "Player";
            Mediator.NotifyColleagues(MessageType.ChangeMainViewContent, _playerScoreBoardViewModel);
        }
    }
}