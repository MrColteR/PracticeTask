using PracticeTask.Model;
using PracticeTask.Model.Base;
using PracticeTask.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PracticeTask.Factory
{
    public class Circle2DInteraction : ICircleInteraction
    {
        private readonly Random random;
        private readonly Setting setting;
        private readonly List<double> coordinateX;
        private readonly List<double> coordinateY;
        public IEnumerable<Circle> CreateElipse()
        {
            ObservableCollection<Circle> Circles = new ObservableCollection<Circle>();
            Circles.GetEnumerator();
            var temp = 0;
            for (int i = 0; i < setting.CountCircle; i++)
            {
                if (setting.CountActiveCircle > temp)
                {
                    Circles.Add(new Circle2D(true, true));
                    temp++;
                }
                else
                {
                    Circles.Add(new Circle2D(false, false));
                }
                var coordinate = GetRandomVector(setting.Speed);
                Circles[i].VectorX = coordinate[0];
                Circles[i].VectorY = coordinate[1];
                Circles[i].X = GetRelativeCoordinateX();
                Circles[i].Y = GetRelativeCoordinateY();
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
        public double[] GetRandomVector(int settingSpeed) // a^2 = b^2 + c^2 Скорость шариков
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
        public void Timer_Tick(IEnumerable<Circle> circles, ref int cycleTime, ref bool IsCompleted, double heightScreen, double widthScreen)
        {
            ObservableCollection<Circle> Circles = circles as ObservableCollection<Circle>;
            if (IsCompleted) // Изменения векторов скорости при 2 и последующих запусках
            {
                for (int i = 0; i < Circles.Count; i++)
                {
                    var coordinate = GetRandomVector(setting.Speed);
                    Circles[i].VectorX = coordinate[0];
                    Circles[i].VectorY = coordinate[1];
                }
                IsCompleted = false;
            }

            for (int i = 0; i < Circles.Count; i++)
            {
                if (Circles[i].X <= 0)
                {
                    Circles[i].VectorX = -Circles[i].VectorX;
                    Circles[i].X = 0;
                }
                if (Circles[i].X >= 1 - setting.SizeCircle)
                {
                    Circles[i].VectorX = -Circles[i].VectorX;
                    Circles[i].X = 1 - setting.SizeCircle;
                }
                if (Circles[i].Y <= 0)
                {
                    Circles[i].VectorY = -Circles[i].VectorY;
                    Circles[i].Y = 0;
                }
                if (Circles[i].Y >= 1 - setting.SizeCircle * 2)
                {
                    Circles[i].VectorY = -Circles[i].VectorY;
                    Circles[i].Y = 1 - setting.SizeCircle * 2;
                }

                for (int j = 0; j < Circles.Count; j++)
                {
                    if (j != i)
                    {
                        // Расстояние между шариками
                        double Dx = Circles[j].X * widthScreen - Circles[i].X * widthScreen;   // Если убирать высоту и ширину шарики в некоторых случаях шарики заходят друг в друга
                        double Dy = Circles[j].Y * heightScreen - Circles[i].Y * heightScreen; // Тут какой то баг с Canvas тк если давать шарикам координаты по Y равные 1 они находяться за кнопками,
                                                                                               // Хотя Canvas не заезжает на область в которой находяться кнопки.
                        double d = Math.Sqrt(Dx * Dx + Dy * Dy);
                        double sin = Dx / d;
                        double cos = Dy / d;

                        if (d <= setting.SizeCircle * widthScreen)
                        {
                            // Коэфицент K касательной между шариками
                            double kIncline = (Circles[i].Y - Circles[j].Y) 
                                            / (Circles[i].X - Circles[j].X);

                            // Углы между перпендикулятором к центрам шариков
                            double angleIncline_j = Math.Atan(-1 * kIncline);
                            double angleIncline_i = Math.Atan(-1 * kIncline);

                            // Единичный векторы скорости перпендикуляров
                            double unitVectorX_j = Math.Cos(angleIncline_j);
                            double unitVectorY_j = Math.Sin(angleIncline_j);
                            double unitVectorX_i = Math.Cos(angleIncline_i);
                            double unitVectorY_i = Math.Sin(angleIncline_i);

                            // Проверка на вхождение шариков друг в друга
                            double vectorLenght_j = Circles[j].VectorX * sin + Circles[j].VectorY * cos;
                            double vectorLenght_i = Circles[i].VectorX * sin + Circles[i].VectorY * cos;
                            double dt = (setting.SizeCircle - d) / (vectorLenght_j / vectorLenght_i);
                            if (dt > 1)
                            {
                                dt = 1;
                            }
                            if (dt < -1)
                            {
                                dt = 1;
                            }
                            Circles[i].X -= Circles[i].VectorX * dt;
                            Circles[j].X -= Circles[j].VectorX * dt;

                            // Новые координаты векторов (Отражаем по Y)
                            double vX_j = Circles[j].X - (Circles[j].X + Circles[i].X) / 2;
                            double vY_j = -1 * (Circles[j].Y - (Circles[j].Y + Circles[i].Y) / 2);

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

                            double vLj = Math.Sqrt(Circles[j].VectorX * Circles[j].VectorX + Circles[j].VectorY * Circles[j].VectorY);
                            double vLi = Math.Sqrt(Circles[i].VectorX * Circles[i].VectorX + Circles[i].VectorY * Circles[i].VectorY);

                            // Новые координаты столкнувшихся шариков
                            Circles[j].VectorX = dNew * -vLj * unitVectorY_j / (unitVectorX_j * Math.Sqrt((Math.Pow(unitVectorY_j, 2) / Math.Pow(unitVectorX_j, 2)) + 1));
                            Circles[j].VectorY = dNew * vLj / Math.Sqrt((Math.Pow(unitVectorY_j, 2) / Math.Pow(unitVectorX_j, 2)) + 1);
                            Circles[i].VectorX = -dNew * -vLi * unitVectorY_i / (unitVectorX_i * Math.Sqrt((Math.Pow(unitVectorY_i, 2) / Math.Pow(unitVectorX_i, 2)) + 1)); ;
                            Circles[i].VectorY = -dNew * vLi / Math.Sqrt((Math.Pow(unitVectorY_i, 2) / Math.Pow(unitVectorX_i, 2)) + 1);
                        }
                    }
                }
                Circles[i].X += Circles[i].VectorX;
                Circles[i].Y += Circles[i].VectorY;
                cycleTime++;
            }
        }
        public void Timer_Restart(IEnumerable<Circle> circles, bool IsRight)
        {
            ObservableCollection<Circle> Circles = circles as ObservableCollection<Circle>;
            if (IsRight)
            {
                Circles.Add(new Circle2D(false, false));
                var coordinate = GetRandomVector(setting.Speed);
                Circles[Circles.Count - 1].VectorX = coordinate[0];
                Circles[Circles.Count - 1].VectorY = coordinate[1];
                Circles[Circles.Count - 1].X = GetRelativeCoordinateX();
                Circles[Circles.Count - 1].Y = GetRelativeCoordinateY();
                Circles[Circles.Count - 1].SizeCircle = setting.SizeCircle;
            }
        }
        public double GetRelativeCoordinateX(List<double> coordinateVectorX)
        {
            double coordinate = coordinateVectorX[random.Next(coordinateVectorX.Count - 1)];
            coordinateVectorX.Remove(coordinate);
            return coordinate;
        }
        public double GetRelativeCoordinateY(List<double> coordinateVectorY)
        {
            double coordinate = coordinateVectorY[random.Next(coordinateVectorY.Count - 1)];
            coordinateVectorY.Remove(coordinate);
            return coordinate;
        }
        public Circle2DInteraction(Setting setting)
        {
            random = new Random();
            this.setting = setting;
            coordinateX = new List<double>();
            coordinateY = new List<double>();
            for (int i = 10; i < 80; i++)
            {
                coordinateX.Add(Math.Round(i * 0.01d, 2));
                coordinateY.Add(Math.Round(i * 0.01d, 2));
            }
        }
    }
}
