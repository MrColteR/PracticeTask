using PracticeTask.Model;
using PracticeTask.ViewModel.Base;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PracticeTask.ViewModel
{
    public class TestWindowViewModel : ViewModelBase
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileCircles = path.Substring(0, path.IndexOf("bin")) + "Circles.json";
        private MainWindowViewModel viewModel;
        private JsonFileService jsonFileService;
        private Random random;
        private DispatcherTimer timer;
        private DispatcherTimer timerRestart;
        private List<double> vectorX;
        private List<double> vectorY;
        private bool isStarted;
        private bool isChecked;
        private bool isRight;
        private int isStop;
        private int levelOfDifficultyUp;
        private int levelOfDifficultyDown;

        public event Action Closing;

        private ObservableCollection<Circle2D> circles;
        public ObservableCollection<Circle2D> Circles
        {
            get => circles;
            set => circles = value;
        }
        private Setting setting;
        public Setting Setting
        {
            get => setting;
            set
            {
                setting = value;
                OnPropertyChanged(nameof(Setting));
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
        private static double sizeCircle;
        public static double SizeCircle
        {
            get => sizeCircle; 
            set 
            { 
                sizeCircle = value;
            }
        }
        private DelegateCommand closeTest;
        public DelegateCommand CloseTest => closeTest ?? (closeTest = new DelegateCommand(Closing));

        private RelayCommand start;
        public RelayCommand Start => start ?? (start = new RelayCommand(obj =>
        {
            for (int i = 0; i < Circles.Count; i++)
            {
                vectorX.Add(GetRandomVector(Circles[i].X));
                vectorY.Add(GetRandomVector(Circles[i].Y));
                Circles[i].IsActiveColor = false;
            }
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0,0,0,0,20);
            timer.Start();
            isStarted = true;
            isChecked = false;
        }, (obj) => isStarted == false));

        private void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < Circles.Count; i++)
            {
                if (Circles[i].X <= 0)
                {
                    vectorX[i] = -vectorX[i];
                    Circles[i].X = 0;
                }
                if (Circles[i].X >= 1 - (1 * SizeCircle / WidthItemsControl))
                {
                    vectorX[i] = -vectorX[i];
                    Circles[i].X = 1 - (1 * SizeCircle / WidthItemsControl);
                }
                if (Circles[i].Y <= 0)
                {
                    vectorY[i] = -vectorY[i];
                    Circles[i].Y = 0;
                }
                if (Circles[i].Y >= 1 - (1 * SizeCircle / HeightItemsControl))
                {
                    vectorY[i] = -vectorY[i];
                    Circles[i].Y = 1 - (1 * SizeCircle / HeightItemsControl);
                }

                for (int j = 0; j < Circles.Count; j++)
                {
                    if (j != i)
                    {
                        double distanse = Math.Sqrt(Math.Pow(Circles[j].X * WidthItemsControl - Circles[i].X * WidthItemsControl, 2)
                                        + Math.Pow(Circles[j].Y * HeightItemsControl - Circles[i].Y * HeightItemsControl, 2));
                        if (distanse < SizeCircle)
                        {
                            //double kIncline = (Circles[j].Y - Circles[i].Y)/(Circles[j].X - Circles[i].X);
                            //double bIncline = Circles[j].Y - (Circles[i].Y - Circles[i].Y) * Circles[j].X / (Circles[i].X - Circles[j].X);

                            //double angleIncline = Math.Atan(-1 * kIncline);

                            //double unitVectorX = Math.Cos(angleIncline);
                            //double unitVectorY = Math.Sin(angleIncline);

                            //double cosAlpha = (unitVectorX * vectorX[i] + unitVectorY * vectorY[i]) 
                            //                / Math.Sqrt(vectorX[i] * vectorX[i] + vectorY[i] * vectorY[i]);
                            //double alpha = Math.Acos(cosAlpha);

                            //double vX = Circles[j].X - (Circles[j].X + Circles[i].X) / 2;
                            //double vY = Circles[j].Y - (Circles[j].Y + Circles[i].Y) / 2;

                            //double d = unitVectorX * vY - unitVectorY * vX > 0 ? 1 : -1;

                            //double vectorLenght = Math.Sqrt(Math.Pow(vectorX[i], 2) + Math.Pow(vectorY[i], 2));
                            //vectorX[i] = d * -vectorLenght * unitVectorY / (unitVectorX * Math.Sqrt(Math.Pow(unitVectorY, 2) / Math.Pow(unitVectorX, 2) + 1));
                            //vectorY[i] = d * vectorLenght / Math.Sqrt((Math.Pow(unitVectorY, 2) / Math.Pow(unitVectorX, 2)) + 1);



                            double kIncline1 = (Circles[i].Y - Circles[j].Y) / (Circles[i].X - Circles[j].X);
                            double bIncline1 = Circles[i].Y - (Circles[j].Y - Circles[j].Y) * Circles[i].X / (Circles[j].X - Circles[i].X);

                            double angleIncline1 = Math.Atan(-1 * kIncline1);

                            double unitVectorX1 = Math.Cos(angleIncline1);
                            double unitVectorY1 = Math.Sin(angleIncline1);

                            double cosAlpha1 = (unitVectorX1 * vectorX[j] + unitVectorY1 * vectorY[j])
                                            / Math.Sqrt(vectorX[j] * vectorX[j] + vectorY[j] * vectorY[j]);
                            double alpha1 = Math.Acos(cosAlpha1);

                            double vX1 = Circles[i].X - (Circles[i].X + Circles[j].X) / 2;
                            double vY1 = Circles[i].Y - (Circles[i].Y + Circles[j].Y) / 2;

                            double d1 = unitVectorX1 * vY1 - unitVectorY1 * vX1 > 0 ? 1 : -1;

                            double vectorLenght1 = Math.Sqrt(Math.Pow(vectorX[j], 2) + Math.Pow(vectorY[j], 2));
                            vectorX[j] = d1 * -vectorLenght1 * unitVectorY1 / (unitVectorX1 * Math.Sqrt(Math.Pow(unitVectorY1, 2) / Math.Pow(unitVectorX1, 2) + 1));
                            vectorY[j] = d1 * vectorLenght1 / Math.Sqrt((Math.Pow(unitVectorY1, 2) / Math.Pow(unitVectorX1, 2)) + 1);
                        }

                    }
                }
                Circles[i].X += vectorX[i];
                Circles[i].Y += vectorY[i];
                isStop++;
            }
            if (isStop >= 2000)
            {
                isStarted = false;
                isStop = 0;
                vectorX.Clear();
                vectorY.Clear();
                ButtonStartVisibility = "Collapsed";
                ButtonCheckVisibility = "Visible";
                ItemsControlVisibility = "Visible";
                timer.Stop();
            }

        }

        private RelayCommand check;
        public RelayCommand Check => check ?? (check = new RelayCommand(obj =>
        {
            var temp = 0;
            isChecked = true;
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
                levelOfDifficultyUp++;
                IncreasingLevelOfDifficulty();
               
            }
            else
            {
                ItemsControlVisibility = "Collapsed";
                TextBlockVisible = "Visible";
                TextBlockText = "Не повезло";
                levelOfDifficultyDown++;
                ReducingLevelOfDifficulty();
            }
            timerRestart = new DispatcherTimer();
            timerRestart.Tick += Timer_Restart;
            timerRestart.Interval = new TimeSpan(0, 0, 2);
            timerRestart.Start();
        }, (obj) => isChecked == false));

        private void Timer_Restart(object sender, EventArgs e)
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
            if (isRight)
            {
                Circles.Add(new Circle2D(GetRelativeCoordinate(), GetRelativeCoordinate(), SizeCircle, false, false));
                isRight = false;
            }
            ButtonStartVisibility = "Visible";
            ItemsControlVisibility = "Visible";
            ButtonCheckVisibility = "Collapsed";
            TextBlockVisible = "Collapsed";
            timerRestart.Stop();
        }

        public TestWindowViewModel(MainWindowViewModel viewModel)
        {
            random = new Random();
            jsonFileService = new JsonFileService();
            vectorX = new List<double>();
            vectorY = new List<double>();

            this.viewModel = viewModel;
            Circles = jsonFileService.Open2D(fileCircles);
            Setting = viewModel.Setting;
            isStarted = false;
            isChecked = false;
            ButtonStartVisibility = "Visible";
            ButtonCheckVisibility = "Collapsed";
            TextBlockVisible = "Collapsed";
            isStop = 0;
            levelOfDifficultyUp = 0;
            levelOfDifficultyDown = 0;
        }

        public void CreateElipse(int count)
        {
            SizeCircle = Math.Round(HeightItemsControl * 0.07);
            var temp = 0;
            for (int i = 0; i < count; i++)
            {
                if (Setting.CountActiveCircle > temp)
                {
                    Circles.Add(new Circle2D(GetRelativeCoordinate(), GetRelativeCoordinate(), SizeCircle, true, true));
                    temp++;
                }
                else
                {
                    Circles.Add(new Circle2D(GetRelativeCoordinate(), GetRelativeCoordinate(), SizeCircle, false, false));
                }
            }
        }
        private double GetRandomVector(double coordinate)
        {
            int direction = random.Next(1, 3);
            double vectorX = random.Next(Convert.ToInt32(coordinate * 1000) + 1) / 1000d;
            switch (direction)
            {
                case 1:
                    return vectorX * Setting.Speed;
                default:
                    return vectorX * - Setting.Speed;
            }
        }

        private double GetRelativeCoordinate()
        {
            double relativeCoordinate = random.Next(1, 9);
            switch (relativeCoordinate)
            {
                case 1:
                    return 0.1;
                case 2:
                    return 0.2;
                case 3:
                    return 0.3;
                case 4:
                    return 0.4;
                case 5:
                    return 0.5;
                case 6:
                    return 0.6;
                case 7:
                    return 0.7;
                default:
                    return 0.8;
            }
        }

        private void IncreasingLevelOfDifficulty()
        {
            levelOfDifficultyDown = 0;
            if (levelOfDifficultyUp <= 2)
            {
                Setting.Speed += 0.001;
            }
            else
            {
                Setting.CountCircle += 1;
                isRight = true;
                levelOfDifficultyUp = 0;
            }
        }

        private void ReducingLevelOfDifficulty()
        {
            levelOfDifficultyUp = 0;
            if (levelOfDifficultyDown <= 2)
            {
                if (Setting.Speed > 0.001)
                {
                    Setting.Speed -= 0.001;
                }
            }
            else
            {
                if (Setting.CountCircle > 1)
                {
                    Circles.RemoveAt(Circles.Count - 1);
                    levelOfDifficultyDown = 0;
                }
                levelOfDifficultyUp = 0;
            }
        }
    }
}
