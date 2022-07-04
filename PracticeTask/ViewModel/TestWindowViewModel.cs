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
        private readonly ICircleInteraction interaction; // Интерфейс для фабрики Circle2D или Circle3D
        public event Action Closing; // Делегат для закрытия окна
        public event Action AddNewCircle3DAction; // Делегат для добавления Circle3D
        public event Action DeleteCircle3DAction; // Делегат для удаления Circle3D
        private bool IsRight { get; set; } // Булеан для проверки правильности ответа пользователя
        private bool IsStarted { get; set; } // Булеан для отслеживания начатия движения
        private bool IsChecked { get; set; } // Булеан для отслеживания начатия проверки
        private int IsStop; // Счетчик для остановки таймера
        private bool IsCompleted; // Булеан для изменения векторов скорости при 2 и последующих старотов
        private bool IsIncreasingLevelOfDifficulty { get; set; } // Булеан для добавления Circle3D
        private bool IsReducingLevelOfDifficulty { get; set; } // Булеан для удаления Circle3D
        private DispatcherTimer Timer { get; set; } // Таймер для движения
        private DispatcherTimer TimerRestart { get; set; } // Таймер для проверки
        private int LevelOfDifficultyUp { get; set; } // Счет правильных ответов
        private int LevelOfDifficultyDown { get; set; } // Счетчик неправильных ответов
        public Setting Setting { get; set; } // Модель настроек

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
        private bool isShowButtonStart;
        public bool IsShowButtonStart
        {
            get => isShowButtonStart; 
            set 
            {
                isShowButtonStart = value; 
                OnPropertyChanged(nameof(IsShowButtonStart)); 
            }
        }
        private bool isShowButtonCheck;
        public bool IsShowButtonCheck
        {
            get => isShowButtonCheck; 
            set 
            {
                isShowButtonCheck = value; 
                OnPropertyChanged(nameof(IsShowButtonCheck)); 
            }
        }
        private bool isShowTextBlock;
        public bool IsShowTextBlock
        {
            get => isShowTextBlock;
            set 
            {
                isShowTextBlock = value;
                OnPropertyChanged(nameof(IsShowTextBlock));
            }
        }
        private bool isShowItemsControl;
        public bool IsShowItemsControl
        {
            get => isShowItemsControl;
            set
            {
                isShowItemsControl = value;
                OnPropertyChanged(nameof(IsShowItemsControl));
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
            if (IsStop >= Setting.TimeTest)
            {
                IsStarted = false;
                for (int i = 0; i < Circles.Count; i++)
                {
                    Circles[i].VectorX = 0;
                    Circles[i].VectorY = 0;
                }
                IsShowButtonStart = false;
                IsShowButtonCheck = true;
                IsShowItemsControl = true;
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
                IsShowItemsControl = false;
                IsShowTextBlock = true;
                TextBlockText = "Повезло";
                LevelOfDifficultyUp++;
                IncreasingLevelOfDifficulty();
            }
            else
            {
                IsShowItemsControl = false;
                IsShowTextBlock = true;
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
            IsShowButtonStart = true;
            IsShowItemsControl = true;
            IsShowButtonCheck = false;
            IsShowTextBlock = false;
            IsCompleted = true;

            interaction.Timer_Restart(Circles, IsRight);
            if (IsRight && Setting.WindowView == 1 && IsIncreasingLevelOfDifficulty)
            {
                IsIncreasingLevelOfDifficulty = !IsIncreasingLevelOfDifficulty;
                DelegateCommand command = new DelegateCommand(AddNewCircle3DAction);
                command.Execute();
            }
            if (!IsRight && Setting.WindowView == 1 && IsReducingLevelOfDifficulty)
            {
                IsReducingLevelOfDifficulty = !IsReducingLevelOfDifficulty;
                DelegateCommand command = new DelegateCommand(DeleteCircle3DAction);
                command.Execute();
            }

            IsRight = false;
            TimerRestart.Stop();
        }

        private DelegateCommand addNewCircle3D; // Комманды для добавления и удаления Circle3D
        private DelegateCommand deleteCircle3D;
        public DelegateCommand AddNewCircle3D => addNewCircle3D ?? (addNewCircle3D = new DelegateCommand(AddNewCircle3DAction));
        public DelegateCommand DeleteCircle3D => deleteCircle3D ?? (deleteCircle3D = new DelegateCommand(DeleteCircle3DAction));

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
                IsIncreasingLevelOfDifficulty = true;
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
                    IsReducingLevelOfDifficulty = true;
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
            IsShowItemsControl = true;
            IsShowButtonStart = true;
            IsShowButtonCheck = false;
            IsShowTextBlock = false;
            IsStop = 0;
            IsCompleted = false;
            LevelOfDifficultyUp = 0;
            LevelOfDifficultyDown = 0;
            IsIncreasingLevelOfDifficulty = false;
            IsReducingLevelOfDifficulty = false;

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
