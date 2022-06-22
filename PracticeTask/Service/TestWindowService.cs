using PracticeTask.View;
using PracticeTask.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.Service
{
    public class TestWindowService : IWindowOpenService
    {
        public void Show(MainWindowViewModel viewModel)
        {
            TestWindow window = new TestWindow(viewModel);
            window.Show();
        }
    }
}
