using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        public INavigationBarViewModel NavigationBarViewModel { get; set; }
        public IContentViewModel ContentViewModel { get; set; }

        public MainViewModel(INavigationBarViewModel navigationBarViewModel)
        {
            NavigationBarViewModel = navigationBarViewModel;
            Mediator.Register(MessageType.ChangeMainViewContent, ChangeContentViewModel);
        }

        private void ChangeContentViewModel(object obj)
        {
            ContentViewModel = obj as IContentViewModel;
        }
    }
}