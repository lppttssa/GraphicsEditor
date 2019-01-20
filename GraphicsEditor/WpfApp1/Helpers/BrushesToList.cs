using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1.Helpers
{
    public static class Palette
    {
        public static IEnumerable<SolidColorBrush> Brushes { get; private set; }
        public static IEnumerable<DashStyle> DashStyles { get; private set; }
        
        static Palette()
        {
            List<SolidColorBrush> brushes = new List<SolidColorBrush>();

            foreach (PropertyInfo propInfo in typeof(System.Windows.Media.Brushes).GetProperties(BindingFlags.Public | BindingFlags.Static))
                if (propInfo.PropertyType == typeof(SolidColorBrush))
                    brushes.Add((SolidColorBrush)propInfo.GetValue(null, null));
            Brushes = brushes;
        }
    }
}
