using SimpleDartboard.PAL.Models;

namespace SimpleDartboard.PAL.UseCases.DartGameSettings.Save
{
    public interface IDartGameSettingSaveService
    {
        void Save(DartGameSetting dartGameSetting, string fileName);
    }
}