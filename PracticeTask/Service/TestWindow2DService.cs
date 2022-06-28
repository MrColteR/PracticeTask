using PracticeTask.View;
using PracticeTask.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.Service
{
    public class TestWindow2DService : IWindowOpenService
    {
        public void Show(MainWindowViewModel viewModel)
        {
            TestWindow2D window = new TestWindow2D(viewModel);
            window.Show();
        }
    }
}
