using System.Windows.Media;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class ActionTokenViewModel : BaseViewModel, IActionTokenViewModel
    {
        private Brush _tokenColor = Brushes.Black;
        private int _size = DefaultTokenSize;

        private static int DefaultTokenSize => 7;

        public Brush TokenColor
        {
            get => _tokenColor;
            set
            {
                _tokenColor = value;
                OnPropertyChanged("TokenColor");
            }
        }

        public int Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged("Size");
            }
        }
    }
}