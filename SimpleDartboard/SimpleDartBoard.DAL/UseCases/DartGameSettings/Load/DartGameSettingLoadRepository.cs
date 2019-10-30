using System.IO;
using Newtonsoft.Json;
using SimpleDartBoard.BLL.UseCases.DartGameSettings.Load;
using SimpleDartBoard.DAL.UseCases.DartGameSettings.Save;
using SimpleDartboard.PAL.Models;

namespace SimpleDartBoard.DAL.UseCases.DartGameSettings.Load
{
    public class DartGameSettingLoadRepository: IDartGameSettingLoadRepository
    {
        public DartGameSetting Load(string fileName)
        {
            var serializedDartGameSetting = "";
            if (File.Exists(DartGameSettingSaveRepository.DartGameSettingDirectory+fileName))
            {
                serializedDartGameSetting = File.ReadAllText(DartGameSettingSaveRepository.DartGameSettingDirectory+fileName);
            }
            var dartGameSetting = JsonConvert.DeserializeObject<DartGameSetting>(serializedDartGameSetting);
            return dartGameSetting;

        }
    }
}