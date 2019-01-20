using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1.Figures
{
    class Ellipse : Figure
    {
        public Ellipse(Point point, Point point1) : base(point)
        {
            this.Fill = Painter.SelectedFill.Clone();
            this.Line = Painter.SelectedLine.Clone();
        }

        public override void Draw(DrawingContext drawingContext)
        {
            Ellipse ellipse = new Ellipse(points[0], points[1]);
            Pen pen = new Pen(Brushes.Aqua, 5);
            Vector point0 = Point.Subtract(points[1], points[0])/2;
            drawingContext.DrawEllipse(this.Fill, this.Line, Point.Add(points[0], point0), point0.X, point0.Y);

        }

        public override void AddPoint(Point point)
        {
            points[1] = point;
        }

    }
}
