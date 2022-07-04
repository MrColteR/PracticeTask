using PracticeTask.Model;
using PracticeTask.Model.Base;
using PracticeTask.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            for (int i = 0; i < viewModel.Circles.Count; i++) // Привязка Circle3D к Viewport2DVisual3D
            {
                Binding binding = new Binding("Material3DCircle") { Source = viewModel.Circles[i] };

                MultiBinding multiBinding = new MultiBinding();
                multiBinding.Converter = new Converter3D();
                multiBinding.Bindings.Add(new Binding("SizeCircle") { Source = viewModel.Circles[i] });
                multiBinding.Bindings.Add(new Binding("X") { Source = viewModel.Circles[i] });
                multiBinding.Bindings.Add(new Binding("Y") { Source = viewModel.Circles[i] });
                multiBinding.Bindings.Add(new Binding("Z") { Source = viewModel.Circles[i] });

                var visual2d = new Viewport2DVisual3D();
                var material = new DiffuseMaterial();

                BindingOperations.SetBinding(visual2d, Viewport2DVisual3D.GeometryProperty, multiBinding);
                BindingOperations.SetBinding(material, DiffuseMaterial.BrushProperty, binding);

                visual2d.Material = material;
                viewport3D.Children.Add(visual2d);
            }

            viewModel.AddNewCircle3DAction += () => // Добавление Circle3D
            {
                Binding binding = new Binding("Material3DCircle") { Source = viewModel.Circles[viewModel.Circles.Count - 1] };

                MultiBinding multiBinding = new MultiBinding();
                multiBinding.Converter = new Converter3D();
                multiBinding.Bindings.Add(new Binding("SizeCircle") { Source = viewModel.Circles[viewModel.Circles.Count - 1] });
                multiBinding.Bindings.Add(new Binding("X") { Source = viewModel.Circles[viewModel.Circles.Count - 1] });
                multiBinding.Bindings.Add(new Binding("Y") { Source = viewModel.Circles[viewModel.Circles.Count - 1] });
                multiBinding.Bindings.Add(new Binding("Z") { Source = viewModel.Circles[viewModel.Circles.Count - 1] });

                var visual2d = new Viewport2DVisual3D();
                var material = new DiffuseMaterial();

                BindingOperations.SetBinding(visual2d, Viewport2DVisual3D.GeometryProperty, multiBinding);
                BindingOperations.SetBinding(material, DiffuseMaterial.BrushProperty, binding);

                visual2d.Material = material;
                viewport3D.Children.Add(visual2d);
            };

            viewModel.DeleteCircle3DAction += () => // Удаление Circle3D
            {
                viewport3D.Children.RemoveAt(viewport3D.Children.Count - 1);
            };
        }

        private object selectedModel;
        private void Viewport3D_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedModel = sender;
            Point position = e.GetPosition(this);
            viewportHitTest(position);
        }
        private void viewportHitTest(Point position)
        {
            VisualTreeHelper.HitTest(viewport3D, null, HitTestResult, new PointHitTestParameters(position));
            if (selectedModel != null)
            {
                var coordinate = (selectedModel as GeometryModel3D).Bounds;
                viewModel.PressOnCircle3D(coordinate.X, coordinate.Y, coordinate.Z);
            }
        }
        private HitTestResultBehavior HitTestResult(HitTestResult result)
        {
            RayMeshGeometry3DHitTestResult rayHTResult = result as RayMeshGeometry3DHitTestResult;
            if (rayHTResult != null)
            {
                selectedModel = rayHTResult.ModelHit as GeometryModel3D;
                return HitTestResultBehavior.Stop;
            }
            return HitTestResultBehavior.Continue;
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Right)
            {
                if (camera.LookDirection.X < 1.5)
                {
                    camera.LookDirection = new Vector3D(camera.LookDirection.X + 0.03, camera.LookDirection.Y, -2);
                    camera.Position = new Point3D(camera.Position.X - 0.03, camera.Position.Y, 2);
                }
            }
            else if (e.Key == Key.Left)
            {
                if (camera.LookDirection.X > -1.5)
                {
                    camera.LookDirection = new Vector3D(camera.LookDirection.X - 0.03, camera.LookDirection.Y, -2);
                    camera.Position = new Point3D(camera.Position.X + 0.03, camera.Position.Y, 2);
                }
            }
            else if (e.Key == Key.Up)
            {
                if (camera.LookDirection.Y < 1.5)
                {
                    camera.LookDirection = new Vector3D(camera.LookDirection.X, camera.LookDirection.Y + 0.03, -2);
                    camera.Position = new Point3D(camera.Position.X, camera.Position.Y - 0.03, 2);
                }
            }
            else if (e.Key == Key.Down)
            {
                if (camera.LookDirection.Y > -1.5)
                {
                    camera.LookDirection = new Vector3D(camera.LookDirection.X, camera.LookDirection.Y - 0.03, -2);
                    camera.Position = new Point3D(camera.Position.X, camera.Position.Y + 0.03, 2);
                }
            }
        }
    }
}
