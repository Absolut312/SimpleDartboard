using SimpleDartboard.PAL.Models;

namespace SimpleDartBoard.BLL.UseCases.DartGameSettings.Load
{
    public interface IDartGameSettingLoadDefaultsRepository
    {
        DartGameSetting Load();
    }
}