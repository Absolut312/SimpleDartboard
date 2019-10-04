using System.IO;
using Newtonsoft.Json;
using SimpleDartBoard.BLL.UseCases.DartGameSettings.Save;
using SimpleDartboard.PAL.Models;

namespace SimpleDartBoard.DAL.UseCases.DartGameSettings.Save
{
    public class DartGameSettingSaveRepository: IDartGameSettingSaveRepository
    {
        public void Save(DartGameSetting dartGameSetting)
        {
            var serializedDartGameSetting = JsonConvert.SerializeObject(dartGameSetting);
            if (!File.Exists(DartGameSettingJsonFileName))
            {
                File.Create(DartGameSettingJsonFileName).Close();
                
                File.WriteAllText(DartGameSettingJsonFileName, serializedDartGameSetting);
            }
            else
            {
                File.WriteAllText(DartGameSettingJsonFileName, serializedDartGameSetting);
            }
        }

        public static string DartGameSettingJsonFileName => "DartGameSetting.json";
    }
}