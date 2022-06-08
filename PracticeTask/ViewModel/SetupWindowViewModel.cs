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
    public class SetupWindowViewModel : ViewModelBase
    {
        private Setting Setting { get; set; }
        private MainWindowViewModel mainWindowViewModel;
        public event Action Closing;

        private int countActiveCircle;
        public int CountActiveCircle { get { return countActiveCircle; }
            set 
            {
                if (CountActiveCircle != value)
                {
                    countActiveCircle = value;
                    OnPropertyChanged(nameof(CountActiveCircle));
                }
            }
        }
        private int countCircle;
        public int CountCircle { get { return countCircle; }
            set 
            {
                if (CountCircle != value)
                {
                    countCircle = value;
                    OnPropertyChanged(nameof(CountCircle));
                }
            }
        }
        private int speed;
        public int Speed { get { return speed; } 
            set 
            {
                if (Speed != value)
                {
                    speed = value;
                    OnPropertyChanged(nameof(Speed));
                }
            } 
        }

        void CloseAndSave()
        {
            Setting.Speed = Speed;
            Setting.CountCircle = CountCircle;
            Setting.CountActiveCircle = CountActiveCircle;
            mainWindowViewModel.Setting = Setting;
            DelegateCommand command = new DelegateCommand(Closing);
            command.Execute();
        }

        private RelayCommand saveSetting;
        public RelayCommand SaveSetting => saveSetting ?? (saveSetting = new RelayCommand(obj =>
        {
            CloseAndSave();
        }));

        private DelegateCommand closeSetting;
        public DelegateCommand CloseSetting => closeSetting ?? (closeSetting = new DelegateCommand(Closing));
                
        public SetupWindowViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            Setting = mainWindowViewModel.Setting;
            CountActiveCircle = Setting.CountActiveCircle;
            Speed = Setting.Speed;
            CountCircle = Setting.CountCircle;
        }
    }
}
