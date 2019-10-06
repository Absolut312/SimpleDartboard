using System.Windows.Media;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class ActionTokenViewModel : BaseViewModel, IActionTokenViewModel
    {
        private Brush _tokenColor = Brushes.Black;

        public Brush TokenColor
        {
            get => _tokenColor;
            set
            {
                _tokenColor = value;
                OnPropertyChanged("TokenColor");
            }
        }
    }
}