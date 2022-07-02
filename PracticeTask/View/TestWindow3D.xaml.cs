using PracticeTask.Model;
using PracticeTask.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace PracticeTask.View
{
    /// <summary>
    /// Логика взаимодействия для TestWindow3D.xaml
    /// </summary>
    public partial class TestWindow3D : Window
    {
        private readonly TestWindowViewModel viewModel;
        private Viewport3D viewport;
        public TestWindow3D(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            Loaded += TestWindow_Loaded;
            DataContext = viewModel = new TestWindowViewModel(mainWindowViewModel);
            viewModel.Closing += () =>
            {
                Close();
            };
            
        }
        private void TestWindow_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.HeightItemsControl = ActualHeight;
            viewModel.WidthItemsControl = ActualWidth;
        }

        private void Viewport3D_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var elp = (sender as FrameworkElement).DataContext as Circle3D;
            elp.IsActiveColor = !elp.IsActiveColor;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            viewport = this.ItemsControl.DataContext as Viewport3D;
            var a = viewport;
            //if (e.Key == Key.Right)
            //{
            //    Rotate(10);
            //}
            //else if (e.Key == Key.Left)
            //{
            //    Rotate(-10);
            //}
            //else if (e.Key == Key.Up)
            //{
            //    Move(-10);
            //}
            //else if (e.Key == Key.Down)
            //{
            //    Move(10);
            //}
            //else if (e.Key == Key.PageUp)
            //{
            //    RotateVertical(10);
            //}
            //else if (e.Key == Key.PageDown)
            //{
            //    RotateVertical(-10);
            //}
        }
        //public void Move(double d)
        //{
        //    double u = 0.05;
        //    PerspectiveCamera camera = (PerspectiveCamera)Viewport3D.Camera;
        //    Vector3D lookDirection = camera.LookDirection;
        //    Point3D position = camera.Position;

        //    lookDirection.Normalize();
        //    position = position + u * lookDirection * d;

        //    camera.Position = position;
        //}
        //public void Rotate(double d)
        //{
        //    double u = 0.05;
        //    double angleD = u * d;
        //    PerspectiveCamera camera = (PerspectiveCamera)Viewport3D.Camera;
        //    Vector3D lookDirection = camera.LookDirection;

        //    var m = new Matrix3D();
        //    m.Rotate(new Quaternion(camera.UpDirection, -angleD)); // Rotate about the camera's up direction to look left/right
        //    camera.LookDirection = m.Transform(camera.LookDirection);
        //}
        //public void RotateVertical(double d)
        //{
        //    double u = 0.05;
        //    double angleD = u * d;
        //    PerspectiveCamera camera = (PerspectiveCamera)Viewport3D.Camera;
        //    Vector3D lookDirection = camera.LookDirection;

        //    // Cross Product gets a vector that is perpendicular to the passed in vectors (order does matter, reverse the order and the vector will point in the reverse direction)
        //    var cp = Vector3D.CrossProduct(camera.UpDirection, lookDirection);
        //    cp.Normalize();

        //    var m = new Matrix3D();
        //    m.Rotate(new Quaternion(cp, -angleD)); // Rotate about the vector from the cross product
        //    camera.LookDirection = m.Transform(camera.LookDirection);
        //}
    }
}
