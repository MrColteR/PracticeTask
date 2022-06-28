using PracticeTask.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace PracticeTask.View
{
    /// <summary>
    /// Логика взаимодействия для TestWindow3D.xaml
    /// </summary>
    public partial class TestWindow3D : Window
    {
        private readonly TestWindowViewModel viewModel;
        public TestWindow3D(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            Loaded += TestWindow_Loaded;
            DataContext = viewModel = new TestWindowViewModel(mainWindowViewModel);
            viewModel.Closing += () =>
            {
                Close();
            };
        }
        private void TestWindow_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.HeightItemsControl = ActualHeight;
            viewModel.WidthItemsControl = ActualWidth;
        }
    }
}
