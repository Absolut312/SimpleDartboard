using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        public INavigationBarViewModel NavigationBarViewModel { get; set; }
        public IContentViewModel ContentViewModel { get; set; }

        public MainViewModel(INavigationBarViewModel navigationBarViewModel,
            IPlayerScoreBoardViewModel playerScoreBoardViewModel)
        {
            NavigationBarViewModel = navigationBarViewModel;
            playerScoreBoardViewModel.Name = "Player 1";
            playerScoreBoardViewModel.CurrentScore = 501;
            ContentViewModel = playerScoreBoardViewModel;
            Mediator.Register(MessageType.ChangeMainViewContent, ChangeContentViewModel);
        }

        private void ChangeContentViewModel(object obj)
        {
            ContentViewModel = obj as IContentViewModel;
        }
    }
}