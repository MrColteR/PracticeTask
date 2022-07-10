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
        public Circle2D(bool isActive, bool isActiveColor) : base(isActive, isActiveColor)
        {
            IsActive = isActive;
            IsActiveColor = isActiveColor;
        }
    }
}
