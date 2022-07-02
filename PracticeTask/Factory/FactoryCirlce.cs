using PracticeTask.Factory;
using PracticeTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.Factory
{
    public static class FactoryCirlce
    {
        public static ICircleInteraction Circle2D(Setting setting) => new Circle2DInteraction(setting);
        public static ICircleInteraction Circle3D(Setting setting) => new Circle3DInteraction(setting);
    }
}
