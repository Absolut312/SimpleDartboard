using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.Models;
using SimpleDartboard.PAL.UseCases.DartGameSettings.Load;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartGameSettingViewModel : BaseViewModel, IDartGameSettingViewModel
    {
        private DartGameSetting _dartGameSetting;
        public ICommand StartGameCommand { get; set; }
        private readonly IDartGameSettingLoadService _dartGameSettingLoadService;

        public string PlayerOneName
        {
            get { return _dartGameSetting.PlayerOneName; }
            set
            {
                _dartGameSetting.PlayerOneName = value;
                OnPropertyChanged("PlayerOneName");
            }
        }

        public string PlayerTwoName
        {
            get { return _dartGameSetting.PlayerTwoName; }
            set
            {
                _dartGameSetting.PlayerTwoName = value;
                OnPropertyChanged("PlayerTwoName");
            }
        }

        public int StartingScore
        {
            get { return _dartGameSetting.StartingScore; }
            set
            {
                _dartGameSetting.StartingScore = value;
                OnPropertyChanged("StartingScore");
            }
        }

        public DartGameSettingViewModel(IDartGameSettingLoadService dartGameSettingLoadService)
        {
            _dartGameSettingLoadService = dartGameSettingLoadService;
            InitializeDartGameSetting(null);
            StartGameCommand = new RelayCommand(StartGame, () =>
            {
                return PlayerOneName != PlayerTwoName && PlayerOneName != "" && PlayerTwoName != "" &&
                       StartingScore > 0;
            });
            Mediator.Register(MessageType.ChangeMainViewContent, InitializeDartGameSetting);
        }

        private void InitializeDartGameSetting(object obj)
        {
            _dartGameSetting = _dartGameSettingLoadService.Load();
        }

        private void StartGame()
        {
            Mediator.NotifyColleagues(MessageType.StartGame, _dartGameSetting);
        }
    }
}