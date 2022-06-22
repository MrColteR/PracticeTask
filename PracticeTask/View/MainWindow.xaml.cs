using PracticeTask.Model;
using PracticeTask.Service;
using PracticeTask.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticeTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileSetting = path.Substring(0, path.IndexOf("bin")) + "Setting.json";
        private MainWindowViewModel viewModel;
        private JsonFileService jsonFileService;

        public MainWindow()
        {
            InitializeComponent();
            jsonFileService = new JsonFileService();
            DataContext = viewModel = new MainWindowViewModel(new SettingWindowService(), new TestWindowService());
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            jsonFileService.SaveSetting(fileSetting, viewModel.Setting);
        }
    }

}
