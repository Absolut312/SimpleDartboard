using System;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IMainViewModel
    {
        ITopbarViewModel TopbarViewModel { get; set; }
        INavigationbarViewModel NavigationbarViewModel { get; set; }
        IContentViewModel ContentViewModel { get; set; }
    }
}