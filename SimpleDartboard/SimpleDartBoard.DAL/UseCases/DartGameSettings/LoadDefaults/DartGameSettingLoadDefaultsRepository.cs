using SimpleDartBoard.BLL.UseCases.DartGameSettings.Load;
using SimpleDartboard.PAL.Models;

namespace SimpleDartBoard.DAL.UseCases.DartGameSettings.LoadDefaults
{
    public class DartGameSettingLoadDefaultsRepository: IDartGameSettingLoadDefaultsRepository
    {
        public DartGameSetting Load()
        {
            return new DartGameSetting
            {
                StartingScore = 501,
                PlayerOneName = "Player 1",
                PlayerTwoName = "Player 2"
            };
        }
    }
}