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
        public ObservableCollection<Circle2D> Open2D(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new ObservableCollection<Circle2D>();
            }
            using (StreamReader reader = File.OpenText(filePath))
            {
                var data = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ObservableCollection<Circle2D>>(data);
            }
        }
        public void Save2D(string filePath, ObservableCollection<Circle2D> itemsList)
        {
            using (StreamWriter writer = File.CreateText(filePath))
            {
                var data = JsonConvert.SerializeObject(itemsList);
                writer.Write(data);
            }
        }

        public Setting OpenSetting(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new Setting(0,0,0);
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
