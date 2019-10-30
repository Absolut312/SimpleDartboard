using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.UseCases.DartGameSettings.Load;

namespace SimpleDartboard.PAL.ViewModels
{
    public class TopbarViewModel : BaseViewModel, ITopbarViewModel
    {
        private readonly INavigationbarViewModel _navigationbarViewModel;
        private bool _isMenuButtonVisible;
        private readonly IDartGameSettingLoadService _dartGameSettingLoadService;

        public TopbarViewModel(INavigationbarViewModel navigationbarViewModel,
            IDartGameSettingLoadService dartGameSettingLoadService)
        {
            _navigationbarViewModel = navigationbarViewModel;
            _dartGameSettingLoadService = dartGameSettingLoadService;
            ToggleMenuButtonCommand = new RelayCommand(ToggleMenuButton);
            ShutdownCommand = new RelayCommand(Shutdown);
            ResumeLastGameCommand = new RelayCommand(ResumeLastGame);
            Mediator.Register(MessageType.HideNavigationbar, HideNavigationbar);
        }

        private void ResumeLastGame()
        {
            var dartGameSetting = _dartGameSettingLoadService.Load("LastGameState.json");
            Mediator.NotifyColleagues(MessageType.StartGame, dartGameSetting);
        }

        private void Shutdown()
        {
            Mediator.NotifyColleagues(MessageType.Shutdown, null);
        }

        private void HideNavigationbar(object obj)
        {
            IsMenuButtonVisible = false;
            ToggleMenuButton();
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
        public ICommand ShutdownCommand { get; set; }
        public ICommand ResumeLastGameCommand { get; set; }
    }
}