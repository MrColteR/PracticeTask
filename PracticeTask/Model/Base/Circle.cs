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
    public abstract class Circle : INotifyPropertyChanged
    {
        public virtual double X { get; set; }
        public virtual double Y { get; set; }
        //public virtual double Height { get; set; }
        //public virtual double Width { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsActiveColor { get; set; }
        public static double SizeCircle { get; set; }
        public Circle(double x, double y, double sizeCircle, /*double height, double width,*/ bool isActive, bool isActiveColor)
        {
            X = x;
            Y = y;
            //Height = height;
            //Width = width;
            IsActive = isActive;
            IsActiveColor = isActiveColor;
            SizeCircle = sizeCircle;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
