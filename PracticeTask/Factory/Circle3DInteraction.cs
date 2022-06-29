using PracticeTask.Model;
using PracticeTask.Model.Base;
using PracticeTask.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace PracticeTask.Factory
{
    public class Circle3DInteraction : ICircleInteraction
    {
        private readonly Random random;
        private readonly Setting setting;
        private readonly List<double> coordinateX;
        private readonly List<double> coordinateY;
        private readonly List<double> coordinateZ;
        public ObservableCollection<Circle> CreateElipse( )
        {
            ObservableCollection<Circle> Circles = new ObservableCollection<Circle>();
            var temp = 0;
            for (int i = 0; i < setting.CountCircle; i++)
            {
                if (setting.CountActiveCircle > temp)
                {
                    Circles.Add(new Circle3D(true, true));
                    temp++;
                }
                else
                {
                    Circles.Add(new Circle3D(false, false));
                }
                var coordinate = GetRandomVector(setting.Speed);
                Circles[i].VectorX = coordinate[0];
                Circles[i].VectorY = coordinate[1];
                Circles[i].X = GetRelativeCoordinateX();
                Circles[i].Y = GetRelativeCoordinateY();
                Circles[i].Z = GetRelativeCoordinateY();
                Circles[i].SizeCircle = setting.SizeCircle;
            }
            return Circles;
        }
        private double GetRelativeCoordinateX()
        {
            double coordinate = coordinateX[random.Next(coordinateX.Count - 1)];
            coordinateX.Remove(coordinate);
            return coordinate;
        }
        private double GetRelativeCoordinateY()
        {
            double coordinate = coordinateY[random.Next(coordinateY.Count - 1)];
            coordinateY.Remove(coordinate);
            return coordinate;
        }
        private double GetRelativeCoordinateZ()
        {
            double coordinate = coordinateY[random.Next(coordinateY.Count - 1)];
            coordinateY.Remove(coordinate);
            return coordinate;
        }
        public double[] GetRandomVector(int settingSpeed) // a^2 = b^2 + c^2 + d^2 Скорость шариков
        {
            int randomX = random.Next(1, 3);
            int randomY = random.Next(1, 3);
            int randomPart = random.Next(2, 5);
            double speed = settingSpeed * 0.001;
            double vectorX = 0;

            switch (randomPart)
            {
                case 2:
                    vectorX = speed - speed / 2d;
                    break;
                case 3:
                    vectorX = speed - speed / 3d;
                    break;
                case 4:
                    vectorX = speed - speed / 4d;
                    break;
            }

            double vectorY = Math.Sqrt(Math.Pow(speed, 2) - Math.Pow(vectorX, 2));
            double[] result = new double[2];

            switch (randomX)
            {
                case 1:
                    result[0] = vectorX * 1;
                    break;
                case 2:
                    result[0] = vectorX * -1;
                    break;
            }
            switch (randomY)
            {
                case 1:
                    result[1] = vectorY * 1;
                    break;
                case 2:
                    result[1] = vectorY * -1;
                    break;
            }

            return result;
        }
        public void Timer_Tick(ObservableCollection<Circle> Сircles, ref int IsStop, double heightItemsControl, double widthItemsControl)
        {
            for (int i = 0; i < Сircles.Count; i++)
            {
                if (Сircles[i].X <= -1)
                {
                    Сircles[i].VectorX = -Сircles[i].VectorX;
                    //Сircles[i].X = 0;
                    //Сircles[i].Y = 0;
                    //Сircles[i].Z = 0;
                }
                if (Сircles[i].X >= 1)
                {
                    Сircles[i].VectorX = -Сircles[i].VectorX;
                    //Сircles[i].X = 0;
                    //Сircles[i].Y = 0;
                    //Сircles[i].Z = 0;
                }
                if (Сircles[i].Y <= -1)
                {
                    Сircles[i].VectorY = -Сircles[i].VectorY;
                    //Сircles[i].X = 0;
                    //Сircles[i].Y = 0;
                    //Сircles[i].Z = 0;
                }
                if (Сircles[i].Y >= 1)
                {
                    Сircles[i].VectorY = -Сircles[i].VectorY;
                    //Сircles[i].X = 0;
                    //Сircles[i].Y = 0;
                    //Сircles[i].Z = 0;
                }
                Сircles[i].X += 2 * Сircles[i].VectorX;
                Сircles[i].Y += 2 * Сircles[i].VectorY;
                IsStop++;
            }
        }
        public ObservableCollection<Circle> Timer_Restart(ObservableCollection<Circle> circles, bool IsRight)
        {
            return circles;
        }        
        public Circle3DInteraction(Setting setting)
        {
            random = new Random();
            this.setting = setting;
            coordinateX = new List<double>();
            coordinateY = new List<double>();
            coordinateZ = new List<double>();
            for (int i = 10; i < 80; i++)
            {
                coordinateX.Add(Math.Round(i * 0.01d, 2));
                coordinateY.Add(Math.Round(i * 0.01d, 2));
                coordinateZ.Add(Math.Round(i * 0.01d, 2));
            }
        }
    }
}
