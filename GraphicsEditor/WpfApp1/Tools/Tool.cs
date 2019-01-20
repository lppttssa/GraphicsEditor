using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Figures;

namespace WpfApp1.Tools
{
    public class Tool
    {
        public Type Figure;
        

       
        public virtual void MouseDown(Point pos)
        {
            /*Figure MyFigure = (Activator.CreateInstance(Figure) as Figure);
            MyFigure.Fill = Painter.SelectedFill;
            MyFigure.Line = Painter.SelectedLine;
            Console.WriteLine(Figure);
            Console.WriteLine('1');
            Painter.Figures.Add(MyFigure);*/


        }
        public virtual void MouseMove(Point pos)
        {

        }
        public virtual void MouseUp(Point pos)
        {

        }
    }
}
