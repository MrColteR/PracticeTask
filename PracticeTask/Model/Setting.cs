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
                OnPropertyChanged(nameof(countCircle));
            }
        }
        private int countActiveCircle;
        [DataMember]
        public int CountActiveCircle { get { return countActiveCircle; } 
            set 
            {
                countActiveCircle = value;
                OnPropertyChanged(nameof(countActiveCircle));
            }
        }
        private int speed;
        [DataMember]
        public int Speed { get { return speed; } 
            set 
            {
                speed = value;
                OnPropertyChanged(nameof(speed));
            }
        }
        public Setting(int countCircle, int countActiveCircle, int speed)
        {
            CountCircle = countCircle;
            CountActiveCircle = countActiveCircle;
            Speed = speed;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
