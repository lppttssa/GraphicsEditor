using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1.Figures
{
    class Line : Figure
    {
        public Line(Point point) : base(point)
        {
            this.Fill = Painter.SelectedFill.Clone();
            this.Line = Painter.SelectedLine.Clone();
        }

        public override void Draw(DrawingContext drawingContext)
        {
            drawingContext.DrawLine(this.Line, points[0], points[1]);
        }

        public override void AddPoint(Point point)
        {
            points[1] = point;
        }
    }
}
