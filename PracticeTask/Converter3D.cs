﻿using PracticeTask.Model;
using PracticeTask.Model.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Media3D;

namespace PracticeTask
{
    public class Converter3D : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double size = (double)values[0];
            double x = (double)values[1];
            double y = (double)values[2];
            double z = (double)values[3];
            double absoluteСoordinateWidth = (double)values[4];
            double absoluteСoordinateHeight = (double)values[5];

            SphereMeshGenerator sphere = new SphereMeshGenerator();
            {
                sphere.Center = new System.Windows.Media.Media3D.Point3D(x, y, z);
                sphere.Radius = size;
                sphere.Stacks = 16;
                sphere.Slices = 32;
            };
            return sphere.Geometry;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
