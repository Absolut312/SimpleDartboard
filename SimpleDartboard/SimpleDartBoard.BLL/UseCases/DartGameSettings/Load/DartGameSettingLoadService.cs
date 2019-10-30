using SimpleDartboard.PAL.Models;
using SimpleDartboard.PAL.UseCases.DartGameSettings.Load;

namespace SimpleDartBoard.BLL.UseCases.DartGameSettings.Load
{
    public class DartGameSettingLoadService : IDartGameSettingLoadService
    {
        private readonly IDartGameSettingLoadRepository _dartGameSettingLoadRepository;
        private readonly IDartGameSettingLoadDefaultsRepository _dartGameSettingLoadDefaultsRepository;

        public DartGameSettingLoadService(IDartGameSettingLoadRepository dartGameSettingLoadRepository,
            IDartGameSettingLoadDefaultsRepository dartGameSettingLoadDefaultsRepository)
        {
            _dartGameSettingLoadRepository = dartGameSettingLoadRepository;
            _dartGameSettingLoadDefaultsRepository = dartGameSettingLoadDefaultsRepository;
        }

        public DartGameSetting Load(string fileName)
        {
            var dartGameSetting =
                _dartGameSettingLoadRepository.Load(fileName) ?? _dartGameSettingLoadDefaultsRepository.Load();
            return dartGameSetting;
        }
    }
}