using System.Windows.Media;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IActionTokenViewModel
    {
        Brush TokenColor { get; set; }
        int Size { get; set; }
    }
}