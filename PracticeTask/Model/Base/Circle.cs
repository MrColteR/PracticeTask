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
        private double x;
        public double X 
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged(nameof(X));
            }
        }
        private double y;
        public double Y
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
        private double z;
        public double Z
        {
            get => z;
            set
            {
                z = value;
                OnPropertyChanged(nameof(Z));
            }
        }
        private double vectorX;
        public double VectorX
        {
            get => vectorX;
            set
            {
                vectorX = value;
                OnPropertyChanged(nameof(VectorX));
            }
        }
        private double vectorY;
        public double VectorY
        {
            get => vectorY;
            set
            {
                vectorY = value;
                OnPropertyChanged(nameof(VectorY));
            }
        }
        private double vectorZ;
        public double VectorZ
        {
            get => vectorZ;
            set
            {
                vectorZ = value;
                OnPropertyChanged(nameof(VectorZ));
            }
        }
        private double sizeCircle;
        public double SizeCircle
        {
            get => sizeCircle;
            set
            {
                sizeCircle = value;
            }
        }
        public virtual bool IsActive { get; set; }
        public virtual bool IsActiveColor { get; set; }
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
