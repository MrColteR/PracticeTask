using PracticeTask.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.Model
{
    public class Circle2D : Circle
    {
        //public override int Speed { get; set; }
        public override int Radius { get; set; }
        public override bool IsActive { get; set; }
        public Circle2D(/*int speed,*/ int radius, bool isActive) : base(/*speed,*/ radius, isActive)
        {
            //Speed = speed;
            Radius = radius;
            IsActive = isActive;
        }
    }
}
