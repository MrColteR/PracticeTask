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
using System.Windows.Shapes;

namespace PracticeTask.View
{
    /// <summary>
    /// Логика взаимодействия для SetupWindow.xaml
    /// </summary>
    public partial class SetupWindow : Window
    {
        private SetupWindowViewModel viewModel;
        public SetupWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = viewModel = new SetupWindowViewModel(mainWindowViewModel);
            viewModel.Closing += () =>
            {
                Close();
            };
        }
    }
}
