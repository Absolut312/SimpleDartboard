using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        public INavigationBarViewModel NavigationBarViewModel { get; set; }

        public MainViewModel(INavigationBarViewModel navigationBarViewModel)
        {
            NavigationBarViewModel = navigationBarViewModel;
        }
    }
}