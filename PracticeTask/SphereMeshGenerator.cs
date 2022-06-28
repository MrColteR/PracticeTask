using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace PracticeTask
{
    public class SphereMeshGenerator : INotifyPropertyChanged
    {
        private int slices = 32;
        private int stacks = 16;
        private Point3D center = new Point3D();
        private double radius = 1;

        public int Slices
        {
            get { return slices; }
            set { slices = value; }
        }
        public int Stacks
        {
            get { return stacks; }
            set { stacks = value; }
        }
        public Point3D Center
        {
            get { return center; }
            set 
            { 
                center = value; 
                OnPropertyChanged(nameof(Center)); 
            }
        }
        public double Radius
        {
            get { return radius; }
            set
            {
                radius = value; 
                OnPropertyChanged(nameof(Radius)); 
            }
        }
        public MeshGeometry3D Geometry
        {
            get
            {
                return CalculateMesh();
            }
        }

        private MeshGeometry3D CalculateMesh()
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

            for (int stack = 0; stack <= Stacks; stack++)
            {
                double phi = Math.PI / 2 - stack * Math.PI / Stacks; // угол, под которым мнимое направление, проведенное из центра системы координат, замыкается с плоскостью XZ.
                double y = radius * Math.Sin(phi); // Определите положение координаты Y. 
                double scale = -radius * Math.Cos(phi);

                for (int slice = 0; slice <= Slices; slice++)
                {
                    double theta = slice * 2 * Math.PI / Slices; // Когда мы смотрим на 2D систему координат осей X и Z... это угол, который замыкает воображаемое направление, проведенное из центра системы координат с осью Z (Z = Y).
                    double x = scale * Math.Sin(theta); // Определите положение координаты X. Обратите внимание, что масштаб = -_радиус * Math.Cos (фи)
                    double z = scale * Math.Cos(theta); // Определите положение координаты Z. Обратите внимание, что масштаб = -_радиус * Math.Cos (фи)

                    Vector3D normal = new Vector3D(x, y, z); // Нормаль — это вектор, перпендикулярный поверхности. В этом случае вектор нормали перпендикулярен треугольной поверхности треугольника.
                    mesh.Normals.Add(normal);
                    mesh.Positions.Add(normal + Center);     // Positions получает вершины треугольников. 
                    mesh.TextureCoordinates.Add(new Point((double)slice / Slices, (double)stack / Stacks));
                    // TextureCoordinates сообщает, где точка из 2D будет отображаться в 3D-мире. 
                }
            }

            for (int stack = 0; stack <= Stacks; stack++)
            {
                int top = (stack + 0) * (Slices + 1);
                int bot = (stack + 1) * (Slices + 1);

                for (int slice = 0; slice < Slices; slice++)
                {
                    if (stack != 0)
                    {
                        mesh.TriangleIndices.Add(top + slice);
                        mesh.TriangleIndices.Add(bot + slice);
                        mesh.TriangleIndices.Add(top + slice + 1);
                    }

                    if (stack != Stacks - 1)
                    {
                        mesh.TriangleIndices.Add(top + slice + 1);
                        mesh.TriangleIndices.Add(bot + slice);
                        mesh.TriangleIndices.Add(bot + slice + 1);
                    }
                }
            }

            return mesh;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
