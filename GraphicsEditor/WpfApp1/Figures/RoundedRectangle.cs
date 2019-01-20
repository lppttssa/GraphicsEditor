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
    public class RoundedRectangle : Figure
    {
        public RoundedRectangle()
        {

        }

        public double RadiusX { get; set; }

        public double RadiusY { get; set; }

        public override bool HasIntersection(Rect rect)
        {
            Rect rSource = new Rect(points[0], points[1]);

            return rect.IntersectsWith(rSource);
        }

        public RoundedRectangle(Point point, Point point1) : base(point)
        {
            this.Fill = Painter.SelectedFill.Clone();
            this.Line = Painter.SelectedLine.Clone();
            Name = "RoundRectangle_#" + Name;
            RadiusX = 20;
            RadiusY = 20;
        } 

        public override void Draw(DrawingContext drawingContext)
        {
            
            Pen pen = new Pen(Brushes.Aqua, 5);
            Vector point0 = Point.Subtract(points[0], points[1]);
            drawingContext.DrawRoundedRectangle(this.Fill, this.Line, new Rect(points[1], point0), RadiusX, RadiusY);
        }

        public override void AddPoint(Point point)
        {
            points[1] = point;
        }
    }
}
