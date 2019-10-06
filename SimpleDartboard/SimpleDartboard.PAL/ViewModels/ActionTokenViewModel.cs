using System.Windows.Media;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class ActionTokenViewModel : BaseViewModel, IActionTokenViewModel
    {
        private Color _tokenColor;
        private int _actionIndex;

        public Color TokenColor
        {
            get => _tokenColor;
            set
            {
                _tokenColor = value;
                OnPropertyChanged("TokenColor");
            }
        }

        public int ActionIndex
        {
            get => _actionIndex;
            set
            {
                _actionIndex = value;
                OnPropertyChanged("ActionIndex");
            }
        }
    }
}