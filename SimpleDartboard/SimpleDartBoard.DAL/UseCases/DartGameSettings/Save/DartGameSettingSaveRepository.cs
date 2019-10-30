using System;
using System.IO;
using Newtonsoft.Json;
using SimpleDartBoard.BLL.UseCases.DartGameSettings.Save;
using SimpleDartboard.PAL.Models;

namespace SimpleDartBoard.DAL.UseCases.DartGameSettings.Save
{
    public class DartGameSettingSaveRepository : IDartGameSettingSaveRepository
    {
        public void Save(DartGameSetting dartGameSetting, string fileName)
        {
            var serializedDartGameSetting = JsonConvert.SerializeObject(dartGameSetting);
            if (!File.Exists(DartGameSettingDirectory + fileName))
            {
                if (!Directory.Exists(DartGameSettingDirectory))
                {
                    Directory.CreateDirectory(DartGameSettingDirectory);
                }

                File.Create(DartGameSettingDirectory + fileName).Close();

                File.WriteAllText(DartGameSettingDirectory + fileName, serializedDartGameSetting);
            }
            else
            {
                File.WriteAllText(DartGameSettingDirectory + fileName, serializedDartGameSetting);
            }
        }

        public static string DartGameSettingDirectory =>
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SimpleDartboard/";
    }
}