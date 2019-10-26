using System.Collections.ObjectModel;
using SimpleDartboard.PAL.Core;

namespace SimpleDartboard.PAL.ViewModels
{
    public class DartBoardScoreControlWrapperViewModel : BaseViewModel, IDartBoardScoreControlWrapperViewModel
    {
        public DartBoardScoreControlWrapperViewModel(
            IDartBoardScoreControlViewModel dartBoardScoreControlViewModelOneToFive,
            IDartBoardScoreControlViewModel dartBoardScoreControlViewModelSixToTen,
            IDartBoardScoreControlViewModel dartBoardScoreControlViewModelElevenToFifteen,
            IDartBoardScoreControlViewModel dartBoardScoreControlViewModelSixteenToLeft)
        {
            DartBoardScoreControlViewModels = new ObservableCollection<IDartBoardScoreControlViewModel>();
            AddScoreInputRange(dartBoardScoreControlViewModelOneToFive, 1);
            AddScoreInputRange(dartBoardScoreControlViewModelSixToTen, 6);
            AddScoreInputRange(dartBoardScoreControlViewModelElevenToFifteen, 11);
            AddScoreInputRange(dartBoardScoreControlViewModelSixteenToLeft, 16);

            dartBoardScoreControlViewModelSixteenToLeft.AddScoreInputAction(25, 1);
            dartBoardScoreControlViewModelSixteenToLeft.AddScoreInputAction(25, 2);
            dartBoardScoreControlViewModelSixteenToLeft.AddScoreInputAction(0, 1);

            DartBoardScoreControlViewModels.Add(dartBoardScoreControlViewModelOneToFive);
            DartBoardScoreControlViewModels.Add(dartBoardScoreControlViewModelSixToTen);
            DartBoardScoreControlViewModels.Add(dartBoardScoreControlViewModelElevenToFifteen);
            DartBoardScoreControlViewModels.Add(dartBoardScoreControlViewModelSixteenToLeft);
        }

        private void AddScoreInputRange(IDartBoardScoreControlViewModel dartBoardScoreControlViewModel, int start)
        {
            for (var i = 0; i < DefaultScoreActionAmountPerControlViewModel; i++)
            {
                dartBoardScoreControlViewModel.AddScoreInputAction(start + i, 1);
                dartBoardScoreControlViewModel.AddScoreInputAction(start + i, 2);
                dartBoardScoreControlViewModel.AddScoreInputAction(start + i, 3);
            }
        }
        private static int DefaultScoreActionAmountPerControlViewModel => 5;

        public ObservableCollection<IDartBoardScoreControlViewModel> DartBoardScoreControlViewModels { get; }
    }
}