using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class NavigationBarViewModel : BaseViewModel, INavigationBarViewModel
    {
        private IDartGameViewModel _dartGameViewModel;

        public NavigationBarViewModel(IDartGameViewModel dartGameViewModel)
        {
            _dartGameViewModel = dartGameViewModel;
            ChangeToDartGame = new RelayCommand(SendChangeRouteToDartGameRequest);
        }

        public ICommand ChangeToDartGame { get; set; }

        private void SendChangeRouteToDartGameRequest()
        {
            Mediator.NotifyColleagues(MessageType.ChangeMainViewContent, _dartGameViewModel);
        }
    }
}