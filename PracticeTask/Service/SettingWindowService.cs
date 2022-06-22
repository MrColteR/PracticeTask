using PracticeTask.View;
using PracticeTask.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.Service
{
    public class SettingWindowService : IWindowOpenService
    {
        public void Show(MainWindowViewModel viewModel)
        {
            SetupWindow window = new SetupWindow(viewModel);
            window.Show();
        }
    }
}
