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
        private double sizeCircle;
        public double SizeCircle
        {
            get { return sizeCircle; }
            set { sizeCircle = value; OnPropertyChanged(nameof(SizeCircle)); }
        }

        private DelegateCommand closeTest;
        public DelegateCommand CloseTest => closeTest ?? (closeTest = new DelegateCommand(Closing));

        private RelayCommand start;
        public RelayCommand Start => start ?? (start = new RelayCommand(obj =>
        {
            int a = 0;
            List<double> vectorX = new List<double>();
            List<double> vectorY = new List<double>();

            for (int i = 0; i < Circles.Count; i++)
            {
                vectorX.Add(GetRandomDirection());
                vectorY.Add(GetRandomDirection());
            }

            timer.Tick += new EventHandler(async (object obje, EventArgs e) =>
            {
                while (a < 2000)
                {
                    for (int i = 0; i < Circles.Count; i++)
                    {
                        Circles[i].X += vectorX[i];
                        Circles[i].Y += vectorY[i];

                        if (Circles[i].X >= WidthItemsControl - SizeCircle || Circles[i].X <= 0)
                        {
                            vectorX[i] = -vectorX[i];
                        }
                        if (Circles[i].Y >= HeightItemsControl - SizeCircle || Circles[i].Y <= 0)
                        {
                            vectorY[i] = -vectorY[i];
                        }
                    }
                    
                    await Task.Delay(40);
                    a++;
                }
            });
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
        }));

        public TestWindowViewModel(MainWindowViewModel viewModel)
        {
            random = new Random();
            jsonFileService = new JsonFileService();
            timer = new DispatcherTimer();
            
            this.viewModel = viewModel;
            Circles = jsonFileService.Open2D(fileCircles);
            Setting = viewModel.Setting;
            //CreateElipse(viewModel.Setting.CountCircle);
        }   
        public void CreateElipse(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Circles.Add(new Circle2D(random.Next((int)SizeCircle, Convert.ToInt32(WidthItemsControl - SizeCircle)),
                                         random.Next((int)SizeCircle, Convert.ToInt32(HeightItemsControl - SizeCircle)),
                                         HeightItemsControl, false, false));
            }
        }
        private int GetRandomDirection()
        {
            int direction = random.Next(1, 3);
            switch (direction)
                {
                    case 1:
                    return 1;
                    default:
                    return -1;
                }
        }
    }
}
