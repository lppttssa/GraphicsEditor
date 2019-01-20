using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1.Figures
{
    [DataContract]
    public class Line : Figure
    {
        public Line()
        {

        }

        public override bool HasIntersection(Rect rect)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (rect.Contains(points[i]))
                    return true;
            }
            return false;
        }

        public Line(Point point) : base(point)
        {
            this.Fill = Painter.SelectedFill.Clone();
            this.Line = Painter.SelectedLine.Clone();
            Name = "line_#"+Name;
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
