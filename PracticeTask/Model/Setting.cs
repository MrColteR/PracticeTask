using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.Model
{
    [DataContract]
    public class Setting
    {
        [DataMember]
        public int CountCircle { get; set; }
        [DataMember]
        public int CountActiveCircle { get; set; }
        [DataMember]
        public int Speed { get; set; }
        public Setting(int countCircle, int countActiveCircle, int speed)
        {
            CountCircle = countCircle;
            CountActiveCircle = countActiveCircle;
            Speed = speed;
        }
    }
}
