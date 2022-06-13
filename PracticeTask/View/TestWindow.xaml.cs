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
    public partial class TestWindow : Window
    {
        private TestWindowViewModel viewModel;
        public TestWindow(MainWindowViewModel mainWindowViewModel)
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
            viewModel.SizeCircle = ItemsControl.ActualHeight / 15;
            viewModel.CreateElipse(viewModel.Setting.CountCircle);
        }
    }
}
