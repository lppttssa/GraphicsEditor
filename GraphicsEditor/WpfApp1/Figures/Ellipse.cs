using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1.Figures
{
    [DataContract]
    public class Ellipse : Figure
    {
        public Ellipse()
        {

        }
       

        public override bool HasIntersection(Rect rect) //пересечение с фигурой
        {
            Vector point0 = Point.Subtract(points[1], points[0]) / 2;
            var geomE = new EllipseGeometry();
            geomE.Center = Point.Add(points[0], point0);
            geomE.RadiusX = point0.X;
            geomE.RadiusY = point0.Y;

            var geomR = new RectangleGeometry();
            geomR.Rect = rect;

            double tolerance = 0.25;
            ToleranceType tolerancetype = ToleranceType.Relative;

            var res = geomR.FillContainsWithDetail(geomE, tolerance, tolerancetype);

            return res != IntersectionDetail.Empty;
        }

        public Ellipse(Point point, Point point1) : base(point)
        {
            this.Fill = Painter.SelectedFill.Clone();
            this.Line = Painter.SelectedLine.Clone();
            Name = "ellipse_#" + Name;
        }

        public override void Draw(DrawingContext drawingContext)
        {
            Vector point0 = Point.Subtract(points[1], points[0])/2;
            drawingContext.DrawEllipse(this.Fill, this.Line, 
                Point.Add(points[0], point0), point0.X, point0.Y);
        }

        public override void AddPoint(Point point)
        {
            points[1] = point;
        }
    }
}
