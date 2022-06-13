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
            get => height = 50;
            set
            {
                height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        private int width;
        public int Width
        {
            get => width = 50;
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
        private double heightWindow;
        public double HeightWindow
        {
            get => heightWindow;
            set
            {
                heightWindow = value;
                OnPropertyChanged(nameof(HeightWindow));
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
        public Circle2D(int x, int y, double heightWindow, bool isActive, bool isActiveColor) : base(isActive, isActiveColor)
        {
            X = x;
            Y = y;
            HeightWindow = heightWindow;
            IsActive = isActive;
            IsActiveColor = isActiveColor;
        }
    }
}
