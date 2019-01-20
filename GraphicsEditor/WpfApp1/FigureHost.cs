using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp1
{
    class FigureHost : FrameworkElement
    {
        public VisualCollection children;

        public FigureHost()
        {
            children = new VisualCollection(this);
        }

        protected override int VisualChildrenCount => children.Count;

        protected override Visual GetVisualChild(int index)
        {
            return children[index];
        }
    }
}
