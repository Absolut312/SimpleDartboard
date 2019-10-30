using SimpleDartboard.PAL.Models;
using SimpleDartboard.PAL.UseCases.DartGameSettings.Save;

namespace SimpleDartBoard.BLL.UseCases.DartGameSettings.Save
{
    public class DartGameSettingSaveService : IDartGameSettingSaveService
    {
        private readonly IDartGameSettingSaveRepository _dartGameSettingSaveRepository;

        public DartGameSettingSaveService(IDartGameSettingSaveRepository dartGameSettingSaveRepository)
        {
            _dartGameSettingSaveRepository = dartGameSettingSaveRepository;
        }

        public void Save(DartGameSetting dartGameSetting, string fileName)
        {
            _dartGameSettingSaveRepository.Save(dartGameSetting, fileName);
        }
    }
}