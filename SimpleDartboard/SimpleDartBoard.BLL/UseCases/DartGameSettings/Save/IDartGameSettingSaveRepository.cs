using SimpleDartboard.PAL.Models;

namespace SimpleDartBoard.BLL.UseCases.DartGameSettings.Save
{
    public interface IDartGameSettingSaveRepository
    {
        void Save(DartGameSetting dartGameSetting);
    }
}