using PracticeTask.Model;
using PracticeTask.View;
using PracticeTask.ViewModel.Base;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PracticeTask.ViewModel
{
    public class SetupWindowViewModel : ViewModelBase, ICloseWindow
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileSetting = path.Substring(0, path.IndexOf("bin")) + "Setting.json";
        private readonly string fileCircle = path.Substring(0, path.IndexOf("bin")) + "Circle.json";
        private JsonFileService jsonFileService;

        private int countActiveCircle;
        public int CountActiveCircle { get { return countActiveCircle; }
            set 
            {
                countActiveCircle = value;
                OnPropertyChanged(nameof(CountActiveCircle));
            }
        }
        private int countCircle;
        public int CountCircle { get { return countCircle; }
            set 
            {
                countCircle = value;
                OnPropertyChanged(nameof(CountCircle));
            }
        }
        private int speed;
        public int Speed { get { return speed; } 
            set 
            {
                speed = value;
                OnPropertyChanged(nameof(Speed));
            } 
        }

        public Action Close { get; set; }
        void CloseAndSave()
        {
            jsonFileService = new JsonFileService();
            jsonFileService.SaveSetting(fileSetting, new Setting(CountCircle, CountActiveCircle, Speed));
            Close?.Invoke();
        }
        void CloseWindow()
        {
            Close?.Invoke();
        }

        private DelegateCommand saveSetting;
        public DelegateCommand SaveSetting => saveSetting ?? (saveSetting = new DelegateCommand(CloseAndSave));
        //{
        //    SetupWindow wnd = obj as SetupWindow;
        //    jsonFileService = new JsonFileService();
        //    jsonFileService.SaveSetting(fileSetting, new Setting(CountCircle, CountActiveCircle, Speed));
        //    wnd.Close();
        //}));
        private DelegateCommand closeSetting;
        public DelegateCommand CloseSetting => closeSetting ?? (closeSetting = new DelegateCommand(CloseWindow));
        //{
        //    SetupWindow wnd = obj as SetupWindow;
        //    wnd.Close();
        //}));
        
        public SetupWindowViewModel(Model.Setting setting)
        {
            jsonFileService = new JsonFileService();
            CountActiveCircle = setting.CountActiveCircle;
            Speed = setting.Speed;
            CountCircle = setting.CountCircle;
        }
    }
}
