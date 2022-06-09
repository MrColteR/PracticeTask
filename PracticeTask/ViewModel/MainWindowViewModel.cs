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
        private readonly JsonFileService jsonFileService = new JsonFileService();

        public Setting Setting { get; set; }
        public MainWindowViewModel ViewModel { get; set; }

        private RelayCommand openSetup;
        public RelayCommand OpenSetup => openSetup ?? (openSetup = new RelayCommand(obj =>
        {
            SetupWindow window = new SetupWindow(ViewModel);
            window.ShowDialog();
        }));
        private RelayCommand startTest; 
        public RelayCommand StartTest => startTest ?? (startTest = new RelayCommand(obj => 
        {
            TestWindow window = new TestWindow(ViewModel);
            window.Show();
        }));

        public MainWindowViewModel()
        {
            ViewModel = this;
            jsonFileService = new JsonFileService();
            Setting = new Setting(jsonFileService.OpenSetting(fileSetting).CountCircle,
                                  jsonFileService.OpenSetting(fileSetting).CountActiveCircle,
                                  jsonFileService.OpenSetting(fileSetting).Speed);
        }
    }
}
