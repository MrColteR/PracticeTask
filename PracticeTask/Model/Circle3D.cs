using PracticeTask.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PracticeTask.Model
{
    internal class Circle3D : Circle
    {
        private double x;
        public override double X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged(nameof(X));
            }
        }
        private double y;
        public override double Y
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
        private double z;
        public override double Z
        {
            get => z;
            set
            {
                z = value;
                OnPropertyChanged(nameof(Z));
            }
        }
        private int height;
        public int Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        private int width;
        public int Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
        private bool isActive;
        public override bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                OnPropertyChanged(nameof(IsActive));
            }
        }
        private double sizeCircle;
        public override double SizeCircle
        {
            get => sizeCircle;
            set
            {
                sizeCircle = value;
            }
        }
        private bool isActiveColor;
        public override bool IsActiveColor
        {
            get => isActiveColor;
            set
            {
                isActiveColor = value;
                if (IsActiveColor == true)
                {
                    Material3DCircle = Brushes.Red;
                }
                else
                {
                    Material3DCircle = Brushes.Yellow;
                }
                OnPropertyChanged(nameof(IsActiveColor));
            }
        }
        private Brush material3DCircle;
        public override Brush Material3DCircle
        {
            get => material3DCircle;
            set
            {
                material3DCircle = value;
                OnPropertyChanged(nameof(Material3DCircle));
            }
        }
        public Circle3D(bool isActive, bool isActiveColor) : base( isActive, isActiveColor)
        {
            IsActive = isActive;
            IsActiveColor = isActiveColor;
            if (IsActiveColor == true)
            {
                Material3DCircle = Brushes.Red;
            }
            else
            {
                Material3DCircle = Brushes.Yellow;
            }
        }
    }
}
