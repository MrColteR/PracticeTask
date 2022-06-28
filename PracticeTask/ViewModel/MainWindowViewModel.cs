using PracticeTask.Model;
using PracticeTask.Service;
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
        private readonly IWindowOpenService settingService;
        private readonly IWindowOpenService testService2D;
        private readonly IWindowOpenService testService3D;

        public Setting Setting { get; set; }
        public MainWindowViewModel ViewModel { get; set; }

        private RelayCommand openSetup;
        public RelayCommand OpenSetup => openSetup ?? (openSetup = new RelayCommand(obj =>
        {
            settingService.Show(ViewModel);
        }));
        private RelayCommand startTest; 
        public RelayCommand StartTest => startTest ?? (startTest = new RelayCommand(obj => 
        {
            if (Setting.WindowView == 0)
            {
                testService2D.Show(ViewModel);
            }
            else
            {
                testService3D.Show(ViewModel);
            }
        }));

        public MainWindowViewModel(IWindowOpenService settingService, IWindowOpenService testService2D, IWindowOpenService testService3D)
        {
            this.settingService = settingService;
            this.testService2D = testService2D;
            this.testService3D = testService3D;
            ViewModel = this;
            jsonFileService = new JsonFileService();
            Setting = new Setting(jsonFileService.OpenSetting(fileSetting).CountCircle,
                                  jsonFileService.OpenSetting(fileSetting).CountActiveCircle,
                                  jsonFileService.OpenSetting(fileSetting).Speed,
                                  jsonFileService.OpenSetting(fileSetting).SizeCircle,
                                  jsonFileService.OpenSetting(fileSetting).WindowView);
        }
    }
}
