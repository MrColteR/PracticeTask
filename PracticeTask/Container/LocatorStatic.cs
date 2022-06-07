using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTask.Container
{
    public static class LocatorStatic
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private static readonly string fileSetting = path.Substring(0, path.IndexOf("bin")) + "Setting.json";
        private static readonly JsonFileService json = new JsonFileService();
        public static DataContainer Data { get; } = new DataContainer()
            {
                CountActiveCircle = json.OpenSetting(fileSetting).CountActiveCircle,
                CountCircle = json.OpenSetting(fileSetting).CountCircle,
                Speed = json.OpenSetting(fileSetting).Speed,
            };
    }
}
