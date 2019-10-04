using System.IO;
using Newtonsoft.Json;
using SimpleDartBoard.BLL.UseCases.DartGameSettings.Load;
using SimpleDartBoard.DAL.UseCases.DartGameSettings.Save;
using SimpleDartboard.PAL.Models;

namespace SimpleDartBoard.DAL.UseCases.DartGameSettings.Load
{
    public class DartGameSettingLoadRepository: IDartGameSettingLoadRepository
    {
        public DartGameSetting Load()
        {
            var serializedDartGameSetting = "";
            if (File.Exists(DartGameSettingSaveRepository.DartGameSettingJsonFileName))
            {
                serializedDartGameSetting = File.ReadAllText(DartGameSettingSaveRepository.DartGameSettingJsonFileName);
            }
            var dartGameSetting = JsonConvert.DeserializeObject<DartGameSetting>(serializedDartGameSetting);
            return dartGameSetting;

        }
    }
}