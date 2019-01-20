using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfApp1.Figures;
using WpfApp1.Helpers;
using WpfApp1.Tools;

namespace WpfApp1
{

    static class Painter
    {
        public static Canvas MainCanvas;
        public static ScrollViewer MainScrollViewer;

        public static List<Figure> Figures = new List<Figure>();


        public static void Invalidate()
        {
            FigureHost.children.Clear();
            var dv = new DrawingVisual();
            var dc = dv.RenderOpen();
            foreach (var figure in Painter.Figures)
            {
                figure.Draw(dc);
            }
            dc.Close();
            FigureHost.children.Add(dv);
        }

        public static readonly List<Tool> Tools = new List<Tool>  //массив инструментов
        {
            new PencilTool(),
            new LineTool(),
            new EllipseTool(),
            new RectangleTool(),
            new RoundedRectangleTool(),
            new HandTool(),
            new ZoomRectangleTool(),
            new SelectRectangleTool()
        };
        public static Brush SelectedFill = Brushes.Aqua;
        public static Pen SelectedLine = new Pen(Brushes.Aqua, 2);

        public static void Hand(Vector vector)  //рука
        {
            foreach (var figure in Figures)
                for (int i = 0; i < figure.points.Count; i++)
                    figure.points[i] = Point.Add(figure.points[i], vector);
        }

        public static void Zoom(float factor)  //Зум
        {
            Matrix m = new Matrix();
            m.Scale(factor, factor);
            foreach (var figure in Figures)
            {
                for (int i = 0; i < figure.points.Count; i++)
                {
                    figure.points[i] = Point.Multiply(figure.points[i], m);
                }
            }
            MainCanvas.Width *= factor;
            MainCanvas.Height *= factor;
        }

        public static void Zoom(float factor, Point point) //Зум с коэффициентом
        {
            if (factor == 0 || float.IsInfinity(factor)) return;
            Matrix m = new Matrix();
            m.Scale(factor, factor);
            foreach (var figure in Figures)
            {
                for (int i = 0; i < figure.points.Count; i++)
                {
                    figure.points[i] = Point.Multiply(figure.points[i], m);
                }
            }
            MainCanvas.Width *= factor;
            MainCanvas.Height *= factor;
            Point newPoint = Point.Multiply(point, m);
            double OffsetX = newPoint.X - point.X;
            double OffsetY = newPoint.Y - point.Y;
            MainScrollViewer.ScrollToHorizontalOffset(OffsetX);
            MainScrollViewer.ScrollToVerticalOffset(OffsetY);
        }


        public static Tool SelectedTool = Tools[0];

        public static FigureHost FigureHost = new FigureHost();

        public static Vector Move; 

        public static List<Figure> SelectFigures(Rect rect)
        {
            var list = new List<Figure>();
            foreach (var fig in Figures)
            {
                if (fig.HasIntersection(rect))
                {
                    list.Add(fig);
                }
            }
            return list;
        }

        internal static MemoryStream SaveCanvasAsStream()
        {
            Rect rect = new Rect(MainCanvas.RenderSize);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Right, (int)rect.Bottom, 96d, 96d, System.Windows.Media.PixelFormats.Default);
rtb.Render(MainCanvas);

            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            pngEncoder.Save(ms);
            ms.Close();
            return ms;
        }

        internal static MemoryStream SaveFiguresAsStream() //сохранение фигур
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            foreach (Figure f in Figures)
            {
                string str = string.Empty;
                sw.WriteLine(f.GetType().FullName);
                str = Helper.ToXml(f);
                sw.WriteLine(str);
            }
            sw.Flush();
            ms.Close();
            return ms;
        }

        internal static void OpenFiguresFromBytes(byte[] bytes)  //открыть сохраненные фигуры
        {
            var figures = new List<Figure>();
            var ms = new MemoryStream(bytes);
            var sr = new StreamReader(ms);
            while(!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                Type type = Type.GetType(str);
                str = sr.ReadLine();
                var fig = (Figure) Helper.FromXml(type, str);
                figures.Add(fig);
            }
            ms.Close();
            Figures = figures;
            Figure.counter = figures.Count;
            Invalidate();
        }

        internal static void InsertCloneFigures(List<Figure> cloneFigures, Point mousePos) //вставить фигуры
        {
            double left = cloneFigures.Min(f => f.points.Min(p => p.X));
            double top = cloneFigures.Min(f => f.points.Min(p => p.Y));

            double deltaX = mousePos.X - left;
            double deltaY = mousePos.Y - top;
            foreach (var fig in cloneFigures)
            {
                for (int i = 0; i < fig.points.Count; i++)
                {
                    fig.points[i] = new Point(fig.points[i].X+deltaX,  fig.points[i].Y+deltaY);
                }
                Figures.Add(fig);
            }
            Invalidate();
        }
    }
}
