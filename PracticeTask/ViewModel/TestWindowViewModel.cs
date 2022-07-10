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
        private bool isRight; // Булеан для проверки правильности ответа пользователя
        private bool isStarted;  // Булеан для отслеживания начатия движения
        private int cycleTime; // Счетчик для остановки таймера
        private bool IsCompleted; // Булеан для изменения векторов скорости при 2 и последующих старотов
        private bool isIncreasingLevelOfDifficulty; // Булеан для добавления Circle3D
        private bool isReducingLevelOfDifficulty; // Булеан для удаления Circle3D
        private int levelOfDifficultyUp; // Счет правильных ответов
        private int levelOfDifficultyDown;// Счетчик неправильных ответов

        private readonly ICircleInteraction interaction; // Интерфейс для фабрики Circle2D или Circle3D
        public event Action Closing; // Делегат для закрытия окна
        public event Action AddNewCircle3DAction; // Делегат для добавления Circle3D
        public event Action DeleteCircle3DAction; // Делегат для удаления Circle3D
        private DispatcherTimer timer; // Таймер для движения
        private DispatcherTimer timerRestart; // Таймер для проверки
        public bool IsChecked { get; set; } // Булеан для отслеживания начатия проверки
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
        private double heightScreen;
        public double HeightScreen
        {
            get => heightScreen; 
            set 
            {
                heightScreen = value;
                OnPropertyChanged(nameof(HeightScreen));
            }
        }
        private double widthScreen;
        public double WidthScreen
        {
            get => widthScreen;
            set 
            {
                widthScreen = value;
                OnPropertyChanged(nameof(WidthScreen));
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
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            timer.Start();
            isStarted = true;
            IsChecked = false;
        }, (obj) => isStarted == false));

        public void Timer_Tick(object sender, EventArgs e)
        {
            interaction.Timer_Tick(Circles, ref cycleTime, ref IsCompleted, HeightScreen, WidthScreen);
            if (cycleTime >= Setting.TimeTest)
            {
                isStarted = false;
                for (int i = 0; i < Circles.Count; i++)
                {
                    Circles[i].VectorX = 0;
                    Circles[i].VectorY = 0;
                }
                IsShowButtonStart = false;
                IsShowButtonCheck = true;
                IsShowItemsControl = true;
                cycleTime = 0;
                timer.Stop();
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
                levelOfDifficultyUp++;
                IncreasingLevelOfDifficulty();
            }
            else
            {
                IsShowItemsControl = false;
                IsShowTextBlock = true;
                TextBlockText = "Не повезло";
                levelOfDifficultyDown++;
                ReducingLevelOfDifficulty();
            }
            timerRestart = new DispatcherTimer();
            timerRestart.Tick += Timer_Restart;
            timerRestart.Interval = new TimeSpan(0, 0, 2);
            timerRestart.Start();
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

            interaction.Timer_Restart(Circles, isRight);
            if (isRight && Setting.WindowView == 1 && isIncreasingLevelOfDifficulty)
            {
                isIncreasingLevelOfDifficulty = !isIncreasingLevelOfDifficulty;
                DelegateCommand command = new DelegateCommand(AddNewCircle3DAction);
                command.Execute();
            }
            if (!isRight && Setting.WindowView == 1 && isReducingLevelOfDifficulty)
            {
                isReducingLevelOfDifficulty = !isReducingLevelOfDifficulty;
                DelegateCommand command = new DelegateCommand(DeleteCircle3DAction);
                command.Execute();
            }

            isRight = false;
            timerRestart.Stop();
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
        public void PressOnCircle3D(double x, double y, double z)
        {
            if (!IsChecked)
            {
                for (int i = 0; i < Circles.Count; i++)
                {
                    if (Math.Round(Circles[i].X, 2) == Math.Round(x + Circles[i].SizeCircle, 2) &&
                        Math.Round(Circles[i].Y, 2) == Math.Round(y + Circles[i].SizeCircle, 2) &&
                        Math.Round(Circles[i].Z, 2) == Math.Round(z + Circles[i].SizeCircle, 2))
                    {
                        Circles[i].IsActiveColor = !Circles[i].IsActiveColor;
                    }
                }
            }
        }
        private void IncreasingLevelOfDifficulty()
        {
            levelOfDifficultyDown = 0;
            if (levelOfDifficultyUp <= 2)
            {
                Setting.Speed += 1;
            }
            else
            {
                Setting.CountCircle += 1;
                isRight = true;
                levelOfDifficultyUp = 0;
                isIncreasingLevelOfDifficulty = true;
            }
        }
        private void ReducingLevelOfDifficulty()
        {
            levelOfDifficultyUp = 0;
            if (levelOfDifficultyDown <= 2)
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
                    levelOfDifficultyDown = 0;
                    isReducingLevelOfDifficulty = true;
                }
                levelOfDifficultyUp = 0;
            }
        }

        public TestWindowViewModel(MainWindowViewModel viewModel)
        {
            Circles = new ObservableCollection<Circle>();
            Setting = viewModel.Setting;
            cycleTime = 0;
            isStarted = false;
            IsChecked = true;
            IsShowItemsControl = true;
            IsShowButtonStart = true;
            IsShowButtonCheck = false;
            IsShowTextBlock = false;
            IsCompleted = false;
            levelOfDifficultyUp = 0;
            levelOfDifficultyDown = 0;
            isIncreasingLevelOfDifficulty = false;
            isReducingLevelOfDifficulty = false;

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
