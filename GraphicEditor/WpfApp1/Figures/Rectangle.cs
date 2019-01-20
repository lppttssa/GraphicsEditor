using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1.Figures
{
    class Rectangle : Figure
    {
        public Rectangle(Point point) : base(point)
        {

        }

        public override void Draw(DrawingContext drawingContext)
        {
            Rect rectangle = new Rect(points[0], points[1]);
            Pen pen = new Pen(Brushes.Aqua, 5);
            drawingContext.DrawRectangle(null,pen, rectangle);

        }

        public override void AddPoint(Point point)
        {
            points[1] = point;
        }
    }
}
