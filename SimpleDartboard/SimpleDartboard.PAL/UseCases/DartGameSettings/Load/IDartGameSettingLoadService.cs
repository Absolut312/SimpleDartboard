using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.UseCases.DartGameSettings.Load
{
    public interface IDartGameSettingLoadService
    {
        DartGameSetting Load(string fileName);
    }
}