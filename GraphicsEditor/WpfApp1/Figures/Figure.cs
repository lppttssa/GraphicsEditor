using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1.Figures
{
    public class Figure
    {
        public List<Point> points;
        public Brush Fill;
        public Pen Line;

        public Figure(Point point)
        {
            points = new List<Point> { point, point };
        }

        public virtual void Draw(DrawingContext drawingContext)
        {

        }

        public virtual void AddPoint(Point point)
        {

        }
    }

}
