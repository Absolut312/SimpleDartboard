using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class TopbarViewModel : BaseViewModel, ITopbarViewModel
    {
        private readonly INavigationbarViewModel _navigationbarViewModel;
        private bool _isMenuButtonVisible;

        public TopbarViewModel(INavigationbarViewModel navigationbarViewModel)
        {
            _navigationbarViewModel = navigationbarViewModel;
            ToggleMenuButtonCommand = new RelayCommand(ToggleMenuButton);
        }

        private void ToggleMenuButton()
        {
            Mediator.NotifyColleagues(MessageType.UpdateNavigationbar,
                _isMenuButtonVisible ? _navigationbarViewModel : null);
        }

        public bool IsMenuButtonVisible
        {
            get => _isMenuButtonVisible;
            set
            {
                _isMenuButtonVisible = value;
                OnPropertyChanged("IsMenuButtonVisible");
            }
        }

        public ICommand ToggleMenuButtonCommand { get; set; }
    }
}