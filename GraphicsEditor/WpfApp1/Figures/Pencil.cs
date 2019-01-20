using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1.Figures
{
    class Pencil : Figure
    {
        public Pencil(Point point) : base(point)
        {
            this.Fill = Painter.SelectedFill.Clone();
            this.Line = Painter.SelectedLine.Clone();
        }

        public override void Draw(DrawingContext drawingContext)
        {

            Pen pen = new Pen(Brushes.Aqua, 5);
            var geometry = new StreamGeometry();
            using (StreamGeometryContext ctx = geometry.Open())
            {
                ctx.BeginFigure(points[0], false, false);
                for (var i = 1; i < points.Count; ++i)
                {
                    ctx.LineTo(points[i], true, false);
                }

            }

            drawingContext.DrawGeometry(null, this.Line, geometry);

        }

        public override void AddPoint(Point point)
        {
            points.Add(point);
        }


    }
}
