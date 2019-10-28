using System;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        public ITopbarViewModel TopbarViewModel
        {
            get => _topbarViewModel;
            set
            {
                _topbarViewModel = value;
                OnPropertyChanged("TopbarViewModel");
            }
        }

        private int _navigationIndex;
        private int _contentAnimationIndex;

        public int NavigationIndex
        {
            get => _navigationIndex;
            set
            {
                _navigationIndex = value;
                OnPropertyChanged("NavigationIndex");
            }
        }

        public int ContentAnimationIndex
        {
            get => _contentAnimationIndex;
            set
            {
                _contentAnimationIndex = value;
                OnPropertyChanged("ContentAnimationIndex");
            }
        }

        public INavigationbarViewModel NavigationbarViewModel
        {
            get => _navigationbarViewModel;
            set
            {
                _navigationbarViewModel = value;
                NavigationIndex = _navigationbarViewModel == null
                    ? UndefinedTransitionIndex
                    : SelectedNavigationTransitionIndex;
                OnPropertyChanged("NavigationbarViewModel");
            }
        }

        private IContentViewModel _contentViewModel;
        private ITopbarViewModel _topbarViewModel;
        private INavigationbarViewModel _navigationbarViewModel;

        public IContentViewModel ContentViewModel
        {
            get { return _contentViewModel; }
            set
            {
                var animationIndex = ContentAnimationIndex == 0 ? 1 : 0;
                _contentViewModel = value;
                OnPropertyChanged("ContentViewModel");
                ContentAnimationIndex = animationIndex;
            }
        }

        public MainViewModel(ITopbarViewModel topbarViewModel)
        {
            TopbarViewModel = topbarViewModel;
            NavigationIndex = UndefinedTransitionIndex;
            Mediator.Register(MessageType.ChangeMainViewContent, ChangeContentViewModel);
            Mediator.Register(MessageType.UpdateNavigationbar, UpdateNavigationbar);
            ContentAnimationIndex = UndefinedTransitionIndex;
        }
        private static int UndefinedTransitionIndex => -1;
        private static int SelectedNavigationTransitionIndex => 0;

        private void ChangeContentViewModel(object obj)
        {
            ContentViewModel = obj as IContentViewModel;
        }

        private void UpdateNavigationbar(object obj)
        {
            var navigationbarViewModel = obj as INavigationbarViewModel;
            NavigationbarViewModel = navigationbarViewModel;
        }
    }
}