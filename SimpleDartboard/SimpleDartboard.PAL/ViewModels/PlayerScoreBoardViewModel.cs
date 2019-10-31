using System.Collections.Generic;
using System.Collections.ObjectModel;
using SimpleDartboard.PAL.Core;
using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.ViewModels
{
    public class PlayerScoreBoardViewModel : BaseViewModel, IPlayerScoreBoardViewModel
    {
        private string _name;

        public PlayerScoreBoardViewModel(IAverageScoreActionViewModel averageScoreActionsTotal,
            IAverageScoreActionViewModel averageScoreActionsPerRound)
        {
            AverageScoreActionsTotal = averageScoreActionsTotal;
            AverageScoreActionsPerRound = averageScoreActionsPerRound;
            Legs = new ObservableCollection<ActionTokenViewModel>();
            Mediator.Register(MessageType.StartGame, ClearScoreActions);
        }

        private void ClearScoreActions(object obj)
        {
            AverageScoreActionsTotal.Reset();
            AverageScoreActionsPerRound.Reset();
        }


        public ObservableCollection<ActionTokenViewModel> Legs { get; }

        public void AddLeg()
        {
            var actionTokenViewModel = new ActionTokenViewModel();
            actionTokenViewModel.Size = 20;
            Legs.Add(actionTokenViewModel);
        }

        public string Name
        {
            get { return _name; }

            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private int _currentScore;
        private IAverageScoreActionViewModel _averageScoreActionsPerRound;
        private IAverageScoreActionViewModel _averageScoreActionsTotal;

        public int CurrentScore
        {
            get
            {
                return _currentScore - (AverageScoreActionsTotal.GetScoreActionsSum() +
                                        AverageScoreActionsPerRound.GetScoreActionsSum());
            }
            set
            {
                _currentScore = value;
                OnPropertyChanged("CurrentScore");
            }
        }

        public void AddScoreAction(ScoreAction scoreAction)
        {
            AverageScoreActionsPerRound.AddScoreAction(scoreAction);
            OnPropertyChanged("AverageScoreActionsPerRound");
            if (CurrentScore > 1) return;
            if (CurrentScore < 0 || CurrentScore == 1 || (CurrentScore == 0 && scoreAction.Multiplier != 2)) AverageScoreActionsPerRound.RevertAllScoreActions();
            OnPropertyChanged("AverageScoreActionsPerRound");
            OnPropertyChanged("CurrentScore");
        }

        public List<ScoreAction> GetScoreActions()
        {
            return AverageScoreActionsTotal.GetAllScoreActions();
        }

        public void Checkout()
        {
            var currentRoundScoreAction = AverageScoreActionsPerRound.CheckoutScoreActions();
            if(currentRoundScoreAction == null) return;
            AverageScoreActionsTotal.AddScoreAction(currentRoundScoreAction);
            OnPropertyChanged("CurrentScore");
        }

        public void UndoLastScoreAction()
        {
            AverageScoreActionsPerRound.UndoLastScoreAction();
            var scoreActionsToCheck = AverageScoreActionsPerRound.GetAllScoreActions();
            AverageScoreActionsPerRound.Reset();
            foreach (var scoreAction in scoreActionsToCheck)
            {
                AddScoreAction(new ScoreAction{Multiplier = scoreAction.Multiplier, Score = scoreAction.Score});
            }
            
            OnPropertyChanged("CurrentScore");
        }

        public IAverageScoreActionViewModel AverageScoreActionsPerRound
        {
            get => _averageScoreActionsPerRound;

            set
            {
                _averageScoreActionsPerRound = value;
                OnPropertyChanged("AverageScoreActionsPerRound");
            }
        }

        public IAverageScoreActionViewModel AverageScoreActionsTotal
        {
            get => _averageScoreActionsTotal;

            set
            {
                _averageScoreActionsTotal = value;
                OnPropertyChanged("AverageScoreActionsTotal");
            }
        }
    }
}