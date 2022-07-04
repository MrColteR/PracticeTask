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
        public IEnumerable<Circle> CreateElipse()
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
                Circles[i].VectorZ = coordinate[2];
                Circles[i].X = GetRelativeCoordinateX();
                Circles[i].Y = GetRelativeCoordinateY();
                Circles[i].Z = GetRelativeCoordinateZ();
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
            int randomZ = 0;
            int randomPartX = random.Next(2, 4);
            double speed = settingSpeed * 0.001;
            double vectorX = 0;

            switch (randomPartX)
            {
                case 2:
                    vectorX = speed - speed / 2d;
                    break;
                case 3:
                    vectorX = speed - speed / 3d;
                    break;
            }
            double vectorY = (speed - vectorX) / 2d;
            double vectorZ = Math.Sqrt(Math.Pow(speed, 2) - Math.Pow(vectorX, 2) + Math.Pow(vectorY, 2));
            double[] result = new double[3];

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
            switch (randomZ)
            {
                case 1:
                    result[2] = vectorZ * 1;
                    break;
                case 2:
                    result[2] = vectorZ * -1;
                    break;
            }

            return result;
        }
        public void Timer_Tick(IEnumerable<Circle> circles, ref int IsStop, ref bool IsCompleted, double heightItemsControl, double widthItemsControl)
        {
            ObservableCollection<Circle> Сircles = circles as ObservableCollection<Circle>;
            if (IsCompleted) // Изменения векторов скорости при 2 и последующих запусках
            {
                for (int i = 0; i < Сircles.Count; i++)
                {
                    var coordinate = GetRandomVector(setting.Speed);
                    Сircles[i].VectorX = coordinate[0];
                    Сircles[i].VectorY = coordinate[1];
                    Сircles[i].VectorZ = coordinate[2];
                }
                IsCompleted = false;
            }
            for (int i = 0; i < Сircles.Count; i++)
            {
                if (Сircles[i].X <= -2 + Сircles[i].SizeCircle)
                {
                    Сircles[i].VectorX = -Сircles[i].VectorX;
                }
                if (Сircles[i].X >= 2 - Сircles[i].SizeCircle)
                {
                    Сircles[i].VectorX = -Сircles[i].VectorX;
                }
                if (Сircles[i].Y <= -1 + Сircles[i].SizeCircle)
                {
                    Сircles[i].VectorY = -Сircles[i].VectorY;
                }
                if (Сircles[i].Y >= 1 - Сircles[i].SizeCircle)
                {
                    Сircles[i].VectorY = -Сircles[i].VectorY;
                }
                if (Сircles[i].Z <= -1 + Сircles[i].SizeCircle)
                {
                    Сircles[i].VectorZ = -Сircles[i].VectorZ;
                }
                if (Сircles[i].Z >= 1 - Сircles[i].SizeCircle)
                {
                    Сircles[i].VectorZ = -Сircles[i].VectorZ;
                }

                // Реализация отталкивания
                for (int j = 0; j < Сircles.Count; j++)
                {
                    if (j != i)
                    {
                        // Расстояние между шариками
                        double Dx = Сircles[j].X - Сircles[i].X;
                        double Dy = Сircles[j].Y - Сircles[i].Y;
                        double Dz = Сircles[j].Z - Сircles[i].Z;
                        double d = Math.Sqrt(Dx * Dx + Dy * Dy + Dz * Dz);
                        double sin = Dx / d;
                        double cos = Dy / d;
                         
                        if (d <= setting.SizeCircle * 2)
                        {
                            // Коэфицент K касательной между шариками
                            double kIncline = (Сircles[i].Y - Сircles[j].Y)
                                            / (Сircles[i].X - Сircles[j].X);

                            // Углы между перпендикулятором к центрам шариков
                            double angleIncline_j = Math.Atan(-1 * kIncline);
                            double angleIncline_i = Math.Atan(-1 * kIncline);

                            // Единичный векторы скорости перпендикуляров
                            double unitVectorX_j = Math.Cos(angleIncline_j);
                            double unitVectorY_j = Math.Sin(angleIncline_j);
                            double unitVectorX_i = Math.Cos(angleIncline_i);
                            double unitVectorY_i = Math.Sin(angleIncline_i);

                            // Проверка на вхождение шариков друг в друга
                            double vectorLenght_j = Сircles[j].VectorX * sin
                                                  + Сircles[j].VectorY * cos;
                            double vectorLenght_i = Сircles[i].VectorX * sin
                                                  + Сircles[i].VectorY * cos;
                            double dt = (setting.SizeCircle - d) / (vectorLenght_j / vectorLenght_i);
                            if (dt > 1)
                            {
                                dt = 1;
                            }
                            if (dt < -1)
                            {
                                dt = 1;
                            }
                            Сircles[i].X -= 2 * Сircles[i].VectorX * dt;
                            Сircles[j].X -= 2 * Сircles[j].VectorX * dt;
                            Сircles[i].X -= 2 * Сircles[i].VectorX * dt;
                            Сircles[j].X -= 2 * Сircles[j].VectorX * dt;

                            // Новые координаты векторов (Отражаем по Y)
                            double vX_j = Сircles[j].X - (Сircles[j].X + Сircles[i].X) / 2;
                            double vY_j = -1 * (Сircles[j].Y - (Сircles[j].Y + Сircles[i].Y) / 2);

                            // Новое расстояние между центрами
                            double dNew;
                            if (unitVectorX_j * vY_j - vX_j * unitVectorY_j > 0)
                            {
                                dNew = 1;
                            }
                            else
                            {
                                dNew = -1;
                            }

                            double vLj = Math.Sqrt(Сircles[j].VectorX * Сircles[j].VectorX + Сircles[j].VectorY * Сircles[j].VectorY);
                            double vLi = Math.Sqrt(Сircles[i].VectorX * Сircles[i].VectorX + Сircles[i].VectorY * Сircles[i].VectorY);

                            // Новые координаты столкнувшихся шариков
                            Сircles[j].VectorX = dNew * -vLj * unitVectorY_j / (unitVectorX_j * Math.Sqrt((Math.Pow(unitVectorY_j, 2) / Math.Pow(unitVectorX_j, 2)) + 1));
                            Сircles[j].VectorY = dNew * vLj / Math.Sqrt((Math.Pow(unitVectorY_j, 2) / Math.Pow(unitVectorX_j, 2)) + 1);
                            Сircles[i].VectorX = -dNew * -vLi * unitVectorY_i / (unitVectorX_i * Math.Sqrt((Math.Pow(unitVectorY_i, 2) / Math.Pow(unitVectorX_i, 2)) + 1)); ;
                            Сircles[j].VectorY = -dNew * vLi / Math.Sqrt((Math.Pow(unitVectorY_i, 2) / Math.Pow(unitVectorX_i, 2)) + 1);
                            Сircles[i].VectorZ = -Сircles[i].VectorZ;
                            Сircles[j].VectorZ = -Сircles[i].VectorZ;
                        }
                    }
                    Сircles[i].X += Сircles[i].VectorX;
                    Сircles[i].Y += Сircles[i].VectorY;
                    Сircles[i].Z += Сircles[i].VectorZ;
                    IsStop++;
                }
            }
        }
        public void Timer_Restart(IEnumerable<Circle> circles, bool IsRight)
        {
            ObservableCollection<Circle> Circles = circles as ObservableCollection<Circle>;
            if (IsRight)
            {
                Circles.Add(new Circle3D(false, false));
                var coordinate = GetRandomVector(setting.Speed);
                Circles[Circles.Count - 1].VectorX = coordinate[0];
                Circles[Circles.Count - 1].VectorY = coordinate[1];
                Circles[Circles.Count - 1].VectorZ = coordinate[2];
                Circles[Circles.Count - 1].X = GetRelativeCoordinateX();
                Circles[Circles.Count - 1].Y = GetRelativeCoordinateY();
                Circles[Circles.Count - 1].Z = GetRelativeCoordinateZ();
                Circles[Circles.Count - 1].SizeCircle = setting.SizeCircle;
            }
        }        
        public Circle3DInteraction(Setting setting)
        {
            random = new Random();
            this.setting = setting;
            coordinateX = new List<double>();
            coordinateY = new List<double>();
            coordinateZ = new List<double>();
            coordinateX.Add(0);
            coordinateY.Add(0);
            coordinateZ.Add(0);
            for (int i = 10; i < 80; i++)
            {
                coordinateX.Add(Math.Round(i * 0.01d, 2));
                coordinateY.Add(Math.Round(i * 0.01d, 2));
                coordinateZ.Add(Math.Round(i * 0.01d, 2));
                coordinateX.Add(Math.Round(-i * 0.01d, 2));
                coordinateY.Add(Math.Round(-i * 0.01d, 2));
                coordinateZ.Add(Math.Round(-i * 0.01d, 2));
            }
        }
    }
}
