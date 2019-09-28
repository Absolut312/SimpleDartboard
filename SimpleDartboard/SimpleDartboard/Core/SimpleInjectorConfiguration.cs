using SimpleInjector;

namespace SimpleDartboard.Core
{
    public static class SimpleInjectorConfiguration
    {
        public static Container RegisterComponents()
        {
            var container = new SimpleInjector.Container();
            return container;
        }
    }
}