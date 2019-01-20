using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Figures;

namespace WpfApp1.Tools
{
    class SelectRectangleTool : Tool
    {

        public event Action<Rect> EventSelectRectangle;

        public override void MouseDown(Point pos)
        {
            Painter.Figures.Add(new SelectRectangle(pos));
        }
        public override void MouseMove(Point pos)
        {
            Painter.Figures[Painter.Figures.Count - 1].AddPoint(pos);
        }
        public override void MouseUp(Point pos)
        {
            var rectangle = (SelectRectangle)Painter.Figures[Painter.Figures.Count - 1];
            Painter.Figures.RemoveAt(Painter.Figures.Count - 1);
            Rect rect = new Rect(rectangle.points[0], rectangle.points[1]);

            EventSelectRectangle?.Invoke(rect);
        }
    }
}
