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
        private List<double> coordinateVectorX;
        private List<double> coordinateVectorY;
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
                    if (j != i) // j = 1, i = 2
                    {
                        double Dx = Circles[j].X - Circles[i].X;
                        double Dy = Circles[j].Y - Circles[i].Y;
                        double d = Math.Sqrt((Dx * Dx) + (Dy * Dy));
                        if (d == 0)
                        {
                            d = 0.001;
                        }
                        double sin = Dx / d; 
                        double cos = Dy / d;

                        if (d < SizeCircle / 1000d)
                        {
                            double Vn1 = (vectorX[i] * sin) + (vectorY[i] * cos);
                            double Vn2 = (vectorX[j] * sin) + (vectorY[j] * cos);
                            double dt = (SizeCircle / 1000d - d) / (Vn1 - Vn2);

                            if (dt > 0)
                            {
                                dt = 1;
                            }

                            if (dt < -0)
                            {
                                dt = -1;
                            }

                            Circles[j].X -= vectorX[j] * dt;
                            Circles[j].Y -= vectorY[j] * dt;
                            Circles[i].X -= vectorX[i] * dt;
                            Circles[i].Y -= vectorY[i] * dt;

                            Dx = Circles[j].X - Circles[i].X;
                            Dy = Circles[j].Y - Circles[i].Y;
                            d = Math.Sqrt(Dx * Dx + Dy * Dy);

                            if (d == 0)
                            {
                                d = 0.001;
                            }

                            sin = Dx / d;
                            cos = Dy / d;
                            Vn1 = (vectorX[i] * sin) + (vectorY[i] * cos);
                            Vn2 = (vectorX[j] * sin) + (vectorY[j] * cos);
                            double Vt1 = (-vectorX[i] * cos) + (vectorY[i] * sin);
                            double Vt2 = (-vectorX[j] * cos) + (vectorY[j] * sin);

                            double o = Vn2;
                            Vn2 = Vn1;
                            Vn1 = o;

                            vectorX[j] = (Vn2 * sin) - (Vt2 * cos);
                            vectorY[j] = (Vn2 * cos) + (Vt2 * sin);
                            vectorX[i] = (Vn1 * sin) - (Vt1 * cos);
                            vectorY[i] = (Vn1 * cos) + (Vt1 * sin);

                            Circles[j].X += vectorX[j] * dt;
                            Circles[j].Y += vectorY[j] * dt;
                            Circles[i].X += vectorX[i] * dt;
                            Circles[i].Y += vectorY[i] * dt;
                        }
                    }
                }
                Circles[i].X += vectorX[i];
                Circles[i].Y += vectorY[i];
                isStop++;
            }
            if (isStop >= 4000)
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
                Circles.Add(new Circle2D(GetRelativeCoordinateX(), GetRelativeCoordinateY(), SizeCircle, false, false));
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
            coordinateVectorX = new List<double>();
            coordinateVectorY = new List<double>();
            AddCoordinateForVector();

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

        private void AddCoordinateForVector()
        {
            for (int i = 10; i < 80; i++)
            {
                coordinateVectorX.Add(Math.Round(i * 0.01d, 1));
                coordinateVectorY.Add(Math.Round(i * 0.01d, 1));
            }
        }

        public void CreateElipse(int count)
        {
            SizeCircle = Math.Round(HeightItemsControl * 0.07);
            var temp = 0;
            for (int i = 0; i < count; i++)
            {
                if (Setting.CountActiveCircle > temp)
                {
                    Circles.Add(new Circle2D(GetRelativeCoordinateX(), GetRelativeCoordinateY(), SizeCircle, true, true));
                    temp++;
                }
                else
                {
                    Circles.Add(new Circle2D(GetRelativeCoordinateX(), GetRelativeCoordinateY(), SizeCircle, false, false));
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

        private double GetRelativeCoordinateX()
        {
            var a = coordinateVectorX.Count;
            double coordinate = coordinateVectorX[random.Next(coordinateVectorX.Count - 1)];
            coordinateVectorX.Remove(coordinate);
            return coordinate;
        }
        private double GetRelativeCoordinateY()
        {
            double coordinate = coordinateVectorY[random.Next(coordinateVectorY.Count - 1)];
            coordinateVectorY.Remove(coordinate);
            return coordinate;
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
