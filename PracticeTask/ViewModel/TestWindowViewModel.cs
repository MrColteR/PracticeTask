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

namespace PracticeTask.ViewModel
{
    public class TestWindowViewModel : ViewModelBase
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileCircles = path.Substring(0, path.IndexOf("bin")) + "Circles.json";
        private MainWindowViewModel viewModel;
        private JsonFileService jsonFileService;
        private Random random;
        public event Action Closing;

        private ObservableCollection<Circle2D> circles;
        public ObservableCollection<Circle2D> Circles
        {
            get { return circles; }
            set { circles = value; }
        }

        public List<int> X { get; set; }
        public List<int> Y { get; set; }
        public int W { get; set; } = 100;
        public List<int> Width1 { get; set; }
        public List<int> Height1 { get; set; }

        private DelegateCommand closeTest;
        public DelegateCommand CloseTest => closeTest ?? (closeTest = new DelegateCommand(Closing));


        public TestWindowViewModel(MainWindowViewModel viewModel)
        {
            X = new List<int>();
            //XX = new List<int>();
            Y = new List<int>();
            Height1 = new List<int>();
            Width1 = new List<int>();
            random = new Random();
            jsonFileService = new JsonFileService();

            this.viewModel = viewModel;
            Circles = jsonFileService.Open2D(fileCircles);
            //Circles.Add(new Circle2D(1, 1, false));

            CreateElipse(viewModel.Setting.CountCircle);
            for (int i = 0; i < Circles.Count; i++)
            {
                X.Add(Circles[i].X);
                Y.Add(Circles[i].Y);
                //H.Add(Circles[i].Y);
                //XX.Add(Circles[i].X);

                Height1.Add(100);
                Width1.Add(100);
            }
            W = 100;
            //Height1 = X;
            //Width1 = Y;
        }   
        private void CreateElipse(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Circles.Add(new Circle2D(random.Next(100), random.Next(100), false));
            }
        }
    }
}
