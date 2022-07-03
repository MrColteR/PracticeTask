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

            for (int i = 0; i < viewModel.Circles.Count; i++)
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
        }

        private void Viewport3D_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var elp = (sender as FrameworkElement).DataContext as Circle3D; // Как отследить конкретный шарик
            if (elp != null)
            {
                //elp.IsActiveColor = !elp.IsActiveColor;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.Right)
            {
                if (camera.LookDirection.X < 0.82)
                {
                    camera.LookDirection = new Vector3D(camera.LookDirection.X + 0.02, 0, -2);
                }
            }
            else if (e.Key == Key.Left)
            {
                if (camera.LookDirection.X > -0.82)
                {
                    camera.LookDirection = new Vector3D(camera.LookDirection.X - 0.02, 0, -2);
                }
            }
            else if (e.Key == Key.Up)
            {
                camera.LookDirection = new Vector3D(0, 0, -2);
            }
        }
    }
}
