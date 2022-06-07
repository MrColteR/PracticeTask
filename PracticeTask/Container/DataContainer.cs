using PracticeTask.Container.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.Container
{
    public class DataContainer : OnPropertyChangedClass
    {
        private int countCircle;
        public int CountCircle { get => countCircle; set => SetProperty(ref countCircle, value); }

        private int countActiveCircle;
        public int CountActiveCircle { get => countActiveCircle; set => SetProperty(ref countActiveCircle, value); }

        private int speed;
        public int Speed { get => speed; set => SetProperty(ref speed, value); }
    }
}
