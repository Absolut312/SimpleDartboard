using SimpleDartboard.PAL.Models;

namespace SimpleDartBoard.BLL.UseCases.DartGameSettings.Load
{
    public interface IDartGameSettingLoadRepository
    {
        DartGameSetting Load();
    }
}