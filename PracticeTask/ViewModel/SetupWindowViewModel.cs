using PracticeTask.Model;
using PracticeTask.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.ViewModel
{
    public class SetupWindowViewModel
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileSetting = path.Substring(0, path.IndexOf("bin")) + "Setting.json";
        private readonly string fileCircle = path.Substring(0, path.IndexOf("bin")) + "Circle.json";
        private JsonFileService jsonFileService;
        //public int CountActiveCircle { get; set; }
        //public int CountCircle { get; set; }
        //public int Speed { get; set; }
        private RelayCommand saveSetting;
        public RelayCommand SaveSetting => saveSetting ?? (saveSetting = new RelayCommand(obj =>
        {
            SetupWindow wnd = obj as SetupWindow;
            jsonFileService = new JsonFileService();
            jsonFileService.SaveSetting(fileSetting, new Setting(Convert.ToInt32(wnd.CountCircle.Text), Convert.ToInt32(wnd.CountActiveCircle.Text), Convert.ToInt32(wnd.Speed.Text)));
            wnd.Close();
        }));
        public SetupWindowViewModel()
        {
            jsonFileService = new JsonFileService();
            //CountActiveCircle = jsonFileService.OpenSetting(fileSetting).CountActiveCircle;
            //CountCircle = jsonFileService.OpenSetting(fileSetting).CountCircle;
            //Speed = jsonFileService.OpenSetting(fileSetting).Speed;
        }
    }
}
