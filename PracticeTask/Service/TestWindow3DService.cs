using PracticeTask.View;
using PracticeTask.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.Service
{
    public class TestWindow3DService : IWindowOpenService
    {
        public void Show(MainWindowViewModel viewModel)
        {
            TestWindow3D window = new TestWindow3D(viewModel);
            window.Show();
        }
    }
}
