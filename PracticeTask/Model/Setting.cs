using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.Model
{
    [DataContract]
    public class Setting : INotifyPropertyChanged
    {
        private int countCircle;
        [DataMember]
        public int CountCircle { get { return countCircle; }  
            set 
            {
                countCircle = value;
                OnPropertyChanged(nameof(CountCircle));
            }
        }
        private int countActiveCircle;
        [DataMember]
        public int CountActiveCircle { get { return countActiveCircle; } 
            set 
            {
                countActiveCircle = value;
                OnPropertyChanged(nameof(CountActiveCircle));
            }
        }
        private int speed;
        [DataMember]
        public int Speed { get { return speed; } 
            set 
            {
                speed = value;
                OnPropertyChanged(nameof(Speed));
            }
        }
        private double sizeCircle;
        [DataMember]
        public double SizeCircle
        {
            get { return sizeCircle; }
            set
            {
                sizeCircle = value;
                OnPropertyChanged(nameof(SizeCircle));
            }
        }
        private int windowView;
        [DataMember]
        public int WindowView
        {
            get { return windowView; }
            set
            {
                windowView = value;
                OnPropertyChanged(nameof(WindowView));
            }
        }
        public Setting(int countCircle, int countActiveCircle, int speed, double sizeCircle, int windowView)
        {
            CountCircle = countCircle;
            CountActiveCircle = countActiveCircle;
            Speed = speed;
            SizeCircle = sizeCircle;
            WindowView = windowView;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
