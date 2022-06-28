using PracticeTask.Model;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticeTask.View
{
    /// <summary>
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow2D : Window
    {
        private readonly TestWindowViewModel viewModel;
        public TestWindow2D(MainWindowViewModel mainWindowViewModel)
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
            viewModel.HeightItemsControl = ItemsControl.ActualHeight;
            viewModel.WidthItemsControl = ItemsControl.ActualWidth;
        }

        private void Ellipse_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var elp = (sender as Ellipse).DataContext as Circle2D;
            elp.IsActiveColor = !elp.IsActiveColor;
        }
    }
}
