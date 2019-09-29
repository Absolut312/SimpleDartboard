using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        public INavigationBarViewModel NavigationBarViewModel { get; set; }
        private IContentViewModel _contentViewModel;

        public IContentViewModel ContentViewModel
        {
            get { return _contentViewModel; }
            set
            {
                _contentViewModel = value;
                OnPropertyChanged("ContentViewModel");
            }
        }

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