using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.Models;
using SimpleDartboard.PAL.UseCases.DartGameSettings.Load;
using SimpleDartboard.PAL.UseCases.DartGameSettings.Save;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartGameSettingViewModel : BaseViewModel, IDartGameSettingViewModel
    {
        private DartGameSetting _dartGameSetting;
        public ICommand StartGameCommand { get; set; }
        private readonly IDartGameSettingLoadService _dartGameSettingLoadService;
        private readonly IDartGameSettingSaveService _dartGameSettingSaveService;

        public string PlayerOneName
        {
            get { return _dartGameSetting.PlayerOne.Name; }
            set
            {
                _dartGameSetting.PlayerOne.Name = value;
                OnPropertyChanged("PlayerOneName");
            }
        }

        public string PlayerTwoName
        {
            get { return _dartGameSetting.PlayerTwo.Name; }
            set
            {
                _dartGameSetting.PlayerTwo.Name = value;
                OnPropertyChanged("PlayerTwoName");
            }
        }

        public int PlayerOneScore
        {
            get { return _dartGameSetting.PlayerOne.Score; }
            set
            {
                _dartGameSetting.PlayerOne.Score = value;
                OnPropertyChanged("PlayerOneScore");
            }
        }

        public int PlayerTwoScore
        {
            get { return _dartGameSetting.PlayerTwo.Score; }
            set
            {
                _dartGameSetting.PlayerTwo.Score = value;
                OnPropertyChanged("PlayerTwoScore");
            }
        }

        public int PlayerOneLegAmount
        {
            get { return _dartGameSetting.PlayerOne.LegAmount; }
            set
            {
                _dartGameSetting.PlayerOne.LegAmount = value;
                OnPropertyChanged("PlayerOneLegAmount");
            }
        }

        public int PlayerTwoLegAmount
        {
            get { return _dartGameSetting.PlayerTwo.LegAmount; }
            set
            {
                _dartGameSetting.PlayerTwo.LegAmount = value;
                OnPropertyChanged("PlayerTwoLegAmount");
            }
        }

        public bool PlayerOneIsFirstSelected
        {
            get { return _dartGameSetting.PlayerOne.IsFirstSelected; }
            set
            {
                _dartGameSetting.PlayerOne.IsFirstSelected = value;
                _dartGameSetting.PlayerTwo.IsFirstSelected = !value;
                OnPropertyChanged("PlayerOneIsFirstSelected");
                OnPropertyChanged("PlayerTwoIsFirstSelected");
            }
        }

        public bool PlayerTwoIsFirstSelected
        {
            get { return _dartGameSetting.PlayerTwo.IsFirstSelected; }
            set
            {
                _dartGameSetting.PlayerOne.IsFirstSelected = !value;
                _dartGameSetting.PlayerTwo.IsFirstSelected = value;
                OnPropertyChanged("PlayerOneIsFirstSelected");
                OnPropertyChanged("PlayerTwoIsFirstSelected");
            }
        }

        public DartGameSettingViewModel(IDartGameSettingLoadService dartGameSettingLoadService,
            IDartGameSettingSaveService dartGameSettingSaveService)
        {
            _dartGameSettingLoadService = dartGameSettingLoadService;
            _dartGameSettingSaveService = dartGameSettingSaveService;
            InitializeDartGameSetting(null);
            StartGameCommand = new RelayCommand(StartGame, () =>
            {
                return PlayerOneName != PlayerTwoName && PlayerOneName != "" && PlayerTwoName != "" &&
                       PlayerOneScore > 1 && PlayerTwoScore > 1;
            });
            Mediator.Register(MessageType.ChangeMainViewContent, InitializeDartGameSetting);
        }

        private void InitializeDartGameSetting(object obj)
        {
            _dartGameSetting = _dartGameSettingLoadService.Load(DartGameSettingFileName);
        }

        private static string DartGameSettingFileName => "DartGameSetting.json";

        private void StartGame()
        {
            _dartGameSetting.PlayerOne.ScoreActions.Clear();
            _dartGameSetting.PlayerTwo.ScoreActions.Clear();
            _dartGameSettingSaveService.Save(_dartGameSetting,DartGameSettingFileName);
            Mediator.NotifyColleagues(MessageType.StartGame, _dartGameSetting);
        }
    }
}