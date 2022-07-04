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
        public int CountCircle 
        {
            get => countCircle; 
            set 
            {
                countCircle = value;
                OnPropertyChanged(nameof(CountCircle));
            }
        }
        private int countActiveCircle;
        [DataMember]
        public int CountActiveCircle 
        { 
            get => countActiveCircle; 
            set 
            {
                countActiveCircle = value;
                OnPropertyChanged(nameof(CountActiveCircle));
            }
        }
        private int speed;
        [DataMember]
        public int Speed 
        { 
            get => speed; 
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
            get => sizeCircle; 
            set
            {
                sizeCircle = value;
                OnPropertyChanged(nameof(SizeCircle));
            }
        }
        private int timeTest;
        [DataMember]
        public int TimeTest
        {
            get => timeTest;
            set
            {
                timeTest = value;
                OnPropertyChanged(nameof(TimeTest));
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
        public Setting(int countCircle, int countActiveCircle, int speed, double sizeCircle, int timeTest, int windowView)
        {
            CountCircle = countCircle;
            CountActiveCircle = countActiveCircle;
            Speed = speed;
            SizeCircle = sizeCircle;
            TimeTest = timeTest;
            WindowView = windowView;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
