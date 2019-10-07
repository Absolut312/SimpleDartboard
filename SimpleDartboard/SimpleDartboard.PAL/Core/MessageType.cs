namespace SimpleDartboard.PAL.Core
{
    public enum MessageType
    {
        ChangeMainViewContent,
        ReduceScoreForSelectedPlayer,
        StartGame,
        RemoveLastActionToken,
        SetIsDartboardScoreInputActive,
        SwichtSelectedPlayer,
        DisableUndoLastScoreAction,
        UndoLastScoreAction
    }
}