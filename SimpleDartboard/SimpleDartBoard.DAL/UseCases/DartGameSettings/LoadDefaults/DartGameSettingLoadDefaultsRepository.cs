using SimpleDartBoard.BLL.UseCases.DartGameSettings.Load;
using SimpleDartboard.PAL.Models;

namespace SimpleDartBoard.DAL.UseCases.DartGameSettings.LoadDefaults
{
    public class DartGameSettingLoadDefaultsRepository : IDartGameSettingLoadDefaultsRepository
    {
        public DartGameSetting Load()
        {
            return new DartGameSetting
            {
                PlayerOne = new PlayerGameSetting
                {
                    Name = "Player 1",
                    Score = 501
                },
                PlayerTwo = new PlayerGameSetting
                {
                    Name = "Player 2",
                    Score = 501
                }
            };
        }
    }
}