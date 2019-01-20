using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Tools
{
    class HandTool : Tool
    {
        public HandTool()
        {

        }
        private Point beginning;
        public override void MouseDown(Point pos)
        {
            base.MouseDown(pos);
            beginning = pos;
        }
        public override void MouseMove(Point pos)
        {
            Painter.Hand(Point.Subtract(pos, beginning));
            beginning = pos;
        }
    }
}
