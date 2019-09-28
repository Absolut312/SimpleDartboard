using System;

namespace SimpleDartboard.PAL.ViewModels
{
    public interface IMainViewModel
    {
        INavigationBarViewModel NavigationBarViewModel { get; set; }
        IContentViewModel ContentViewModel { get; set; }
    }
}