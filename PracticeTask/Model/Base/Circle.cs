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
