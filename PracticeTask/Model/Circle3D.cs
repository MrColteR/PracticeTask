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
                if (IsActiveColor == true)
                {
                    IsActiveMaterial = true;
                }
                else
                {
                    IsActiveMaterial = false;
                }
                OnPropertyChanged(nameof(IsActiveColor));
            }
        }
        private bool isActiveMaterial;
        public bool IsActiveMaterial
        {
            get => isActiveMaterial;
            set
            {
                isActiveMaterial = value;
                OnPropertyChanged(nameof(IsActiveMaterial));
            }
        }
        public Circle3D(bool isActive, bool isActiveColor) : base( isActive, isActiveColor)
        {
            IsActive = isActive;
            IsActiveColor = isActiveColor;
            if (IsActiveColor == true)
            {
                IsActiveMaterial = true;
            }
            else
            {
                IsActiveMaterial = false;
            }
        }
    }
}
