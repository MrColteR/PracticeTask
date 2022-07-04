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
        private readonly MainWindowViewModel mainWindowViewModel;
        public event Action Closing;
        private Setting Setting { get; set; }

        private int countActiveCircle;
        public int CountActiveCircle 
        { 
            get => countActiveCircle; 
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
        public int CountCircle 
        {
            get => countCircle; 
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
        public int Speed 
        {
            get => speed;
            set 
            {
                if (Speed != value)
                {
                    speed = value;
                    OnPropertyChanged(nameof(Speed));
                }
            } 
        }
        private int timeTest;
        public int TimeTest
        {
            get => timeTest;
            set
            {
                if (TimeTest != value)
                {
                    timeTest = value;
                    OnPropertyChanged(nameof(TimeTest));
                }
            }
        }
        private int windowView;
        public int WindowView
        {
            get => windowView;
            set
            {
                if (WindowView != value)
                {
                    windowView = value;
                    OnPropertyChanged(nameof(WindowView));
                }
            }
        }

        void CloseAndSave()
        {
            Setting.CountCircle = CountCircle;
            Setting.CountActiveCircle = CountActiveCircle;
            Setting.Speed = Speed;
            Setting.TimeTest = TimeTest;
            Setting.WindowView = WindowView;
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
            CountCircle = Setting.CountCircle;
            Speed = Setting.Speed;
            TimeTest = Setting.TimeTest;
            WindowView = Setting.WindowView;
        }
    }
}
