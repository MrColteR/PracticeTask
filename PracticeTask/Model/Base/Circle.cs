using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PracticeTask.Model.Base
{
    [DataContract]
    public abstract class Circle : INotifyPropertyChanged
    {
        public virtual double X { get; set; }
        public virtual double Y { get; set; }
        public virtual double Z { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsActiveColor { get; set; }
        public virtual double SizeCircle { get; set; }
        public virtual double VectorX { get; set; }
        public virtual double VectorY { get; set; }
        public virtual double VectorZ { get; set; }
        public virtual Brush Material3DCircle { get; set; }
        public Circle(bool isActive, bool isActiveColor)
        {
            IsActive = isActive;
            IsActiveColor = isActiveColor;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
