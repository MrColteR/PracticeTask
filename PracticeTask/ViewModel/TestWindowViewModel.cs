using PracticeTask.Model;
using PracticeTask.ViewModel.Base;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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
        private List<double> vectorX;
        private List<double> vectorY;

        public event Action Closing;

        private ObservableCollection<Circle2D> circles;
        public ObservableCollection<Circle2D> Circles
        {
            get { return circles; }
            set { circles = value; }
        }
        private Setting setting;
        public Setting Setting
        {
            get { return setting; }
            set { setting = value; }
        }
        private double heightItemsControl;
        public double HeightItemsControl
        {
            get { return heightItemsControl; }
            set { heightItemsControl = value; OnPropertyChanged(nameof(HeightItemsControl)); }
        }
        private double widthItemsControl;
        public double WidthItemsControl
        {
            get { return widthItemsControl; }
            set { widthItemsControl = value; OnPropertyChanged(nameof(WidthItemsControl)); }
        }
        private static double sizeCircle;
        public static double SizeCircle
        {
            get { return sizeCircle; }
            set { sizeCircle = value; }
        }

        private DelegateCommand closeTest;
        public DelegateCommand CloseTest => closeTest ?? (closeTest = new DelegateCommand(Closing));

        private RelayCommand start;
        public RelayCommand Start => start ?? (start = new RelayCommand(obj =>
        {
            int a = 0;

            for (int i = 0; i < Circles.Count; i++)
            {
                vectorX.Add(GetRandomDirection());
                vectorY.Add(GetRandomDirection());
                Circles[i].IsActiveColor = false;
            }

            timer.Tick += new EventHandler(async (object obje, EventArgs e) =>
            {
                while (a < 2000)
                {
                    for (int i = 0; i < Circles.Count; i++)
                    {
                        Circles[i].X += vectorX[i];
                        Circles[i].Y += vectorY[i];
                        if (Circles[i].X >= 1 - (1 * SizeCircle / WidthItemsControl) || Circles[i].X <= 0)
                        {
                            vectorX[i] = -vectorX[i];
                        }
                        if (Circles[i].Y >= 1 - (1 * SizeCircle / HeightItemsControl) || Circles[i].Y <= 0)
                        {
                            vectorY[i] = -vectorY[i];
                        }
                        for (int j = i + 1; j < Circles.Count - 1; j++)
                        {
                            if (Math.Sqrt(Math.Pow(Circles[i].X * WidthItemsControl - Circles[j].X * WidthItemsControl, 2) +
                                Math.Pow(Circles[i].Y * HeightItemsControl - Circles[j].Y * HeightItemsControl, 2)) <= SizeCircle - 1)
                            {
                                vectorX[i] = -vectorX[i];
                                vectorY[i] = -vectorY[i];
                                vectorX[j] = -vectorX[j];
                                vectorY[j] = -vectorY[j];
                            }
                        }
                    }
                    
                    await Task.Delay(30);
                    a++;
                }
            });
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }));

        public TestWindowViewModel(MainWindowViewModel viewModel)
        {
            random = new Random();
            jsonFileService = new JsonFileService();
            timer = new DispatcherTimer();
            vectorX = new List<double>();
            vectorY = new List<double>();

            this.viewModel = viewModel;
            Circles = jsonFileService.Open2D(fileCircles);
            Setting = viewModel.Setting;
        }   
        public TestWindowViewModel() { }
        public void CreateElipse(int count)
        {
            SizeCircle = Math.Round(HeightItemsControl / 15);
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
        private double GetRandomDirection()
        {
            int direction = random.Next(1, 3);
            switch (direction)
                {
                    case 1:
                    return Setting.Speed;
                    default:
                    return -Setting.Speed;
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
    }
}
