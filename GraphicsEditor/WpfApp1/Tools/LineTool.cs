using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Figures;

namespace WpfApp1.Tools
{
    class LineTool : Tool
    {
        public override void MouseDown(Point pos)
        {
            Painter.Figures.Add(new Line(pos));
        }
        public override void MouseMove(Point pos)
        {
            Painter.Figures[Painter.Figures.Count - 1].AddPoint(pos);
        }
        public override void MouseUp(Point pos)
        {

        }
    }
}
