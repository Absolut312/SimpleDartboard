using System.Windows.Media;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IActionTokenViewModel
    {
        Color TokenColor { get; set; }
        int ActionIndex { get; set; }
    }
}