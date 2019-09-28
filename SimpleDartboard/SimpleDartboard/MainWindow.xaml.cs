using SimpleDartboard.PAL.ViewModels;

namespace SimpleDartboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(IMainViewModel mainViewModel)
        {
            InitializeComponent();
            this.DataContext = mainViewModel;
        }
    }
}