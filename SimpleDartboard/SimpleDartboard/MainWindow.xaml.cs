using MaterialDesignThemes.Wpf;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.ViewModels;
using SimpleDartboard.Theme;

namespace SimpleDartboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(IMainViewModel mainViewModel)
        {
            var paletteHelper = new PaletteHelper();
            paletteHelper.SetTheme(new CustomizedMaterialDesign());
            InitializeComponent();
            this.DataContext = mainViewModel;
            Mediator.Register(MessageType.Shutdown,Shutdown);
        }

        private void Shutdown(object obj)
        {
            Close();
        }
    }
}