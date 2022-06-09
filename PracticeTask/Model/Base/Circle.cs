using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.Model.Base
{
    [DataContract]
    public class Circle : INotifyPropertyChanged
    {
        //public virtual int Speed { get; set; }
        //public virtual int Radius { get; set; }
        public virtual bool IsActive { get; set; }
        public Circle(/*int speed,*/ /*int radius,*/ bool isActive)
        {
            //Speed = speed;
            //Radius = radius;
            IsActive = isActive;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
