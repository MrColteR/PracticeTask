using Newtonsoft.Json;
using PracticeTask.Model;
using PracticeTask.Model.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask
{
    public class JsonFileService
    {
        public Setting OpenSetting(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new Setting(0,0,0,0,500,0);
            }
            using (StreamReader reader = File.OpenText(filePath))
            {
                var data = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<Setting>(data);
            }
        }
        public void SaveSetting(string filePath, Setting setting)
        {
            using (StreamWriter writer = File.CreateText(filePath))
            {
                var data = JsonConvert.SerializeObject(setting);
                writer.Write(data);
            }
        }
    }
}
