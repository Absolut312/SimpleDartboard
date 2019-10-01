using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartGameSettingViewModel : BaseViewModel, IDartGameSettingViewModel
    {
        private DartGameSetting _dartGameSetting;
        public ICommand StartGameCommand { get; set; }

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

        public DartGameSettingViewModel()
        {
            _dartGameSetting = new DartGameSetting();
            StartGameCommand = new RelayCommand(StartGame, () =>
            {
                return PlayerOneName != PlayerTwoName && PlayerOneName != "" && PlayerTwoName != "" &&
                       StartingScore > 0;
            });
        }

        private void StartGame()
        {
            Mediator.NotifyColleagues(MessageType.StartGame, _dartGameSetting);
        }
    }
}