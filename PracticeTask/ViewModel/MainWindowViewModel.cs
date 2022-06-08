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
    public class MainWindowViewModel
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileSetting = path.Substring(0, path.IndexOf("bin")) + "Setting.json";
        private JsonFileService jsonFileService = new JsonFileService();
        private RelayCommand openSetup;
        public RelayCommand OpenSetup => openSetup ?? (openSetup = new RelayCommand(obj =>
        {
            Model.Setting setting = new Setting(jsonFileService.OpenSetting(fileSetting).CountCircle,
                                                jsonFileService.OpenSetting(fileSetting).CountActiveCircle,
                                                jsonFileService.OpenSetting(fileSetting).Speed);

            SetupWindow window = new SetupWindow(setting);
            window.ShowDialog();
        }));
        private RelayCommand startTest; 
        public RelayCommand StartTest => startTest ?? (startTest = new RelayCommand(obj => 
        {
            TestWindow window = new TestWindow();
            window.Show();
        }));
    }
}
