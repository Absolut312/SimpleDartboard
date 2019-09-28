using System;
using System.Diagnostics;
using SimpleDartboard.Core;
using SimpleInjector;

namespace SimpleDartboard
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var container = SimpleInjectorConfiguration.RegisterComponents();
            RunApplication(container);
        }


        private static void RunApplication(Container container)
        {
            try
            {
                var app = new App();
                var mainWindow = container.GetInstance<MainWindow>();
                app.Run(mainWindow);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
    }
}