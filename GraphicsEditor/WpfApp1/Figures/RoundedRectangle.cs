using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1.Figures
{
    class RoundedRectangle : Figure
    {
        public RoundedRectangle(Point point, Point point1) : base(point)
        {
            this.Fill = Painter.SelectedFill.Clone();
            this.Line = Painter.SelectedLine.Clone();
            
        } 

        public override void Draw(DrawingContext drawingContext)
        {
            
            Pen pen = new Pen(Brushes.Aqua, 5);
            Vector point0 = Point.Subtract(points[0], points[1]);
            drawingContext.DrawRoundedRectangle(this.Fill, this.Line, new Rect(points[1], point0), 20.0, 20.0);

        }

        public override void AddPoint(Point point)
        {
            points[1] = point;
        }
    }
}
