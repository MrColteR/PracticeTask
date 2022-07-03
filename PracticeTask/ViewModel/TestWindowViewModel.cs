using PracticeTask.Factory;
using PracticeTask.Model;
using PracticeTask.Model.Base;
using PracticeTask.ViewModel.Base;
using Prism.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace PracticeTask.ViewModel
{
    public class TestWindowViewModel : ViewModelBase
    {
        private readonly ICircleInteraction interaction;
        public event Action Closing;
        private bool IsRight { get; set; }
        private bool IsStarted { get; set; }
        private bool IsChecked { get; set; }
        private int IsStop;
        private bool IsCompleted;
        private DispatcherTimer Timer { get; set; }
        private DispatcherTimer TimerRestart { get; set; }
        private int LevelOfDifficultyUp { get; set; }
        private int LevelOfDifficultyDown { get; set; }
        public Setting Setting { get; set; }

        private ObservableCollection<Circle> circles;
        public ObservableCollection<Circle> Circles
        {
            get => circles;
            set
            {
                if (Circles != value)
                {
                    circles = value;
                    OnPropertyChanged(nameof(Circles));
                }
            }
        }
        private string buttonStartVisibility;
        public string ButtonStartVisibility
        {
            get => buttonStartVisibility; 
            set 
            { 
                buttonStartVisibility = value; 
                OnPropertyChanged(nameof(ButtonStartVisibility)); 
            }
        }
        private string buttonCheckVisibility;
        public string ButtonCheckVisibility
        {
            get => buttonCheckVisibility; 
            set 
            {
                buttonCheckVisibility = value; 
                OnPropertyChanged(nameof(ButtonCheckVisibility)); 
            }
        }
        private string textBlockVisible;
        public string TextBlockVisible 
        {
            get => textBlockVisible;
            set 
            { 
                textBlockVisible = value;
                OnPropertyChanged(nameof(TextBlockVisible));
            }
        }
        private string textBlockText;
        public string TextBlockText
        {
            get => textBlockText; 
            set 
            {
                textBlockText = value; 
                OnPropertyChanged(nameof(TextBlockText));
            }
        }
        private string itemsControlVisibility;
        public string ItemsControlVisibility
        {
            get => itemsControlVisibility; 
            set 
            { 
                itemsControlVisibility = value; 
                OnPropertyChanged(nameof(ItemsControlVisibility));
            }
        }
        private double heightItemsControl;
        public double HeightItemsControl
        {
            get => heightItemsControl; 
            set 
            { 
                heightItemsControl = value;
                OnPropertyChanged(nameof(HeightItemsControl));
            }
        }
        private double widthItemsControl;
        public double WidthItemsControl
        {
            get => widthItemsControl;
            set 
            { 
                widthItemsControl = value;
                OnPropertyChanged(nameof(WidthItemsControl));
            }
        }

        private DelegateCommand closeTest;
        public DelegateCommand CloseTest => closeTest ?? (closeTest = new DelegateCommand(Closing));

        private RelayCommand start;
        public RelayCommand Start => start ?? (start = new RelayCommand(obj =>
        {
            for (int i = 0; i < Circles.Count; i++)
            {
                Circles[i].IsActiveColor = false;
            }
            Timer = new DispatcherTimer();
            Timer.Tick += Timer_Tick;
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            Timer.Start();
            IsStarted = true;
            IsChecked = false;
        }, (obj) => IsStarted == false));

        public void Timer_Tick(object sender, EventArgs e)
        {
            interaction.Timer_Tick(Circles, ref IsStop, ref IsCompleted, HeightItemsControl, WidthItemsControl);
            if (IsStop >= 500)
            {
                IsStarted = false;
                for (int i = 0; i < Circles.Count; i++)
                {
                    Circles[i].VectorX = 0;
                    Circles[i].VectorY = 0;
                }
                ButtonStartVisibility = "Collapsed";
                ButtonCheckVisibility = "Visible";
                ItemsControlVisibility = "Visible";
                IsStop = 0;
                Timer.Stop();
            }
        }

        private RelayCommand check;
        public RelayCommand Check => check ?? (check = new RelayCommand(obj =>
        {
            var temp = 0;
            IsChecked = true;
            for (int i = 0; i < Circles.Count; i++)
            {
                if (Circles[i].IsActiveColor == Circles[i].IsActive && Circles[i].IsActive == true)
                {
                    temp++;
                }
            }
            if (temp == Setting.CountActiveCircle)
            {
                ItemsControlVisibility = "Collapsed";
                TextBlockVisible = "Visible";
                TextBlockText = "Повезло";
                LevelOfDifficultyUp++;
                IncreasingLevelOfDifficulty();
            }
            else
            {
                ItemsControlVisibility = "Collapsed";
                TextBlockVisible = "Visible";
                TextBlockText = "Не повезло";
                LevelOfDifficultyDown++;
                ReducingLevelOfDifficulty();
            }
            TimerRestart = new DispatcherTimer();
            TimerRestart.Tick += Timer_Restart;
            TimerRestart.Interval = new TimeSpan(0, 0, 2);
            TimerRestart.Start();
        }, (obj) => IsChecked == false));

        public void Timer_Restart(object sender, EventArgs e)
        {
            for (int i = 0; i < Circles.Count; i++)
            {
                if (Circles[i].IsActive == true)
                {
                    Circles[i].IsActiveColor = true;
                }
                else
                {
                    Circles[i].IsActiveColor = false;
                }
            }
            ButtonStartVisibility = "Visible";
            ItemsControlVisibility = "Visible";
            ButtonCheckVisibility = "Collapsed";
            TextBlockVisible = "Collapsed";
            IsRight = false;
            IsCompleted = true;

            interaction.Timer_Restart(Circles, IsRight);
            TimerRestart.Stop();
        }
        public void CreateElipse()
        {
            if (Setting.WindowView == 0)
            {
                Setting.SizeCircle = 0.03;
            }
            else
            {
                Setting.SizeCircle = 0.1;
            }
            Circles = (ObservableCollection<Circle>)interaction.CreateElipse();
        }
        private void IncreasingLevelOfDifficulty()
        {
            LevelOfDifficultyDown = 0;
            if (LevelOfDifficultyUp <= 2)
            {
                Setting.Speed += 1;
            }
            else
            {
                Setting.CountCircle += 1;
                IsRight = true;
                LevelOfDifficultyUp = 0;
            }
        }
        private void ReducingLevelOfDifficulty()
        {
            LevelOfDifficultyUp = 0;
            if (LevelOfDifficultyDown <= 2)
            {
                if (Setting.Speed > 1)
                {
                    Setting.Speed -= 1;
                }
            }
            else
            {
                if (Setting.CountCircle > 1)
                {
                    Circles.RemoveAt(Circles.Count - 1);
                    LevelOfDifficultyDown = 0;
                }
                LevelOfDifficultyUp = 0;
            }
        }

        public TestWindowViewModel(MainWindowViewModel viewModel)
        {
            Circles = new ObservableCollection<Circle>();
            Setting = viewModel.Setting;
            IsStarted = false;
            IsChecked = false;
            ButtonStartVisibility = "Visible";
            ButtonCheckVisibility = "Collapsed";
            TextBlockVisible = "Collapsed";
            IsStop = 0;
            IsCompleted = false;
            LevelOfDifficultyUp = 0;
            LevelOfDifficultyDown = 0;

            if (Setting.WindowView == 0) // Выбор между 2D и 3D шариками
            {
                interaction = FactoryCirlce.Circle2D(Setting);
            }
            else
            {
                interaction = FactoryCirlce.Circle3D(Setting);
            }
            CreateElipse();
        }
    }
}
