using PracticeTask.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.ViewModel
{
    public class MainWindowViewModel
    {
        private RelayCommand openSetup;
        public RelayCommand OpenSetup => openSetup ?? (openSetup = new RelayCommand(obj =>
        {
            SetupWindow window = new SetupWindow();
            window.ShowDialog();
        }));
        private RelayCommand startTest;
        public RelayCommand StartTest => startTest ?? (startTest = new RelayCommand(obj => 
        {
            TestWindow window = new TestWindow();
            window.Show();
        }));
    }
}
