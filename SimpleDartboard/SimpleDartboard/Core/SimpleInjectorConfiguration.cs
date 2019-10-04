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
            return container;
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
        }
    }
}