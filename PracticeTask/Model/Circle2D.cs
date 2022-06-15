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
        private static double sizeCircle;
        public static double SizeCircle
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
                OnPropertyChanged(nameof(IsActiveColor));
            }
        }
        public Circle2D(double x, double y, double sizeCircle, bool isActive, bool isActiveColor) : base(isActive, isActiveColor)
        {
            X = x;
            Y = y;
            SizeCircle = sizeCircle;
            IsActive = isActive;
            IsActiveColor = isActiveColor;
        }
    }
}
