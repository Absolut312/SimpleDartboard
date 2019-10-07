using SimpleDartBoard.BLL.UseCases.DartGameSettings.Load;
using SimpleDartBoard.BLL.UseCases.DartGameSettings.Save;
using SimpleDartBoard.DAL.UseCases.DartGameSettings.Load;
using SimpleDartBoard.DAL.UseCases.DartGameSettings.LoadDefaults;
using SimpleDartBoard.DAL.UseCases.DartGameSettings.Save;
using SimpleDartboard.PAL.UseCases.DartGameSettings.Load;
using SimpleDartboard.PAL.UseCases.DartGameSettings.Save;
using SimpleDartboard.PAL.ViewModels;
using SimpleInjector;

namespace SimpleDartboard.Core
{
    public static class SimpleInjectorConfiguration
    {
        public static Container RegisterComponents()
        {
            var container = new SimpleInjector.Container();
            RegisterViewModels(container);
            RegisterUseCases(container);
            return container;
        }

        private static void RegisterUseCases(Container container)
        {
            RegisterDartBoardSettingLoad(container);
            RegisterDartBoardSettingSave(container);
        }

        private static void RegisterDartBoardSettingSave(Container container)
        {
            container.Register<IDartGameSettingSaveService, DartGameSettingSaveService>();
            container.Register<IDartGameSettingSaveRepository, DartGameSettingSaveRepository>();
        }

        private static void RegisterDartBoardSettingLoad(Container container)
        {
            container.Register<IDartGameSettingLoadService, DartGameSettingLoadService>();
            container.Register<IDartGameSettingLoadRepository, DartGameSettingLoadRepository>();
            container.Register<IDartGameSettingLoadDefaultsRepository, DartGameSettingLoadDefaultsRepository>();
        }

        private static void RegisterViewModels(Container container)
        {
            container.Register<IMainViewModel, MainViewModel>();
            container.Register<INavigationBarViewModel, NavigationBarViewModel>();
            container.Register<IPlayerScoreBoardViewModel, PlayerScoreBoardViewModel>();
            container.Register<IDartBoardScoreInputViewModel, DartBoardScoreInputViewModel>();
            container.Register<IDartGameViewModel, DartGameViewModel>();
            container.Register<IDartGameSettingViewModel, DartGameSettingViewModel>();
            container.Register<IDartBoardScoreControlViewModel, DartBoardScoreControlViewModel>();
            container.Register<IDartGameControlViewModel, DartGameControlViewModel>();
            container.Register<IDartGameWinnerViewModel, DartGameWinnerViewModel>();
        }
    }
}