using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public bool isPressed; 


        public MainWindow()
        {
            InitializeComponent();
            MyCanvas.Children.Add(Painter.FigureHost);
            for (int i = 0; i < Painter.Tools.Count; i++)
            {
                string st = "../icons/" + Painter.Tools[i].GetType().Name + ".png";
                ImageBrush img = new ImageBrush();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(st, UriKind.Relative);
                bi3.EndInit();
                img.ImageSource = bi3;
                Button btn = new Button();
                MyDockPanel.Children.Add(btn);
                btn.BorderBrush = Brushes.Black;
                btn.Name = "btn" + i;
                btn.Height = 35;
                btn.Width = 50;
                btn.Background = img;
                btn.Content = "";
                btn.Tag = i;
                btn.HorizontalAlignment = HorizontalAlignment.Left;
                btn.Click += new RoutedEventHandler(Tool_Click);
            }

            Brush[] colors =
            {
                Brushes.Crimson,Brushes.Maroon,Brushes.DeepPink,Brushes.DarkOrange,Brushes.Yellow,Brushes.Fuchsia,Brushes.BlueViolet,Brushes.Indigo,Brushes.Lime,Brushes.Teal,
                Brushes.Aqua,Brushes.LightCyan,Brushes.Blue,Brushes.Navy,Brushes.Ivory,Brushes.Black,

            };

            foreach (var brush in colors)
            {
                Button newButton = new Button()
                {
                    Width = 20,
                    Height = 20,
                    Background = brush,
                    Tag = brush
                };
                newButton.Click += new RoutedEventHandler(ButtonFill_Click);
                DockPanelFill.Children.Add(newButton);

            }

            foreach (var brush in colors)
            {
                Button newButton = new Button()
                {
                    Width = 20,
                    Height = 20,
                    Background = brush,
                    Tag = brush
                };
                newButton.Click += new RoutedEventHandler(ButtonLine_Click);
                DockPanelLine.Children.Add(newButton);

            }
        }

        //Tool selectedTool = new LineTool(); 


        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isPressed = true;
            Painter.SelectedTool.MouseDown(e.GetPosition(MyCanvas));
            //selectedTool.MouseDown(e.GetPosition(Canvas), Canvas); 
            Invalidate();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                Painter.SelectedTool.MouseMove(e.GetPosition(MyCanvas));
                //selectedTool.MouseMove(e.GetPosition(Canvas), Canvas); 
                Invalidate();
            }

        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPressed = false;
            // selectedTool.MouseUp(e.GetPosition(Canvas), Canvas); 
            Invalidate();
        }

        public void Invalidate()
        {
            Painter.FigureHost.children.Clear();
            var dv = new DrawingVisual();
            var dc = dv.RenderOpen();
            foreach (var figure in Painter.Figures)
            {
                figure.Draw(dc);
            }
            dc.Close();
            Painter.FigureHost.children.Add(dv);
        }


        private void Tool_Click(object sender, RoutedEventArgs e)
        {
            Painter.SelectedTool = Painter.Tools[Convert.ToInt32((sender as Button).Tag)]; 
        }

        private void ButtonFill_Click(object sender, RoutedEventArgs e)
        {
            Painter.SelectedFill = (sender as Button).Tag as Brush;
        }

        private void ButtonLine_Click(object sender, RoutedEventArgs e)
        {
            Painter.SelectedLine = new Pen ((sender as Button).Background, 2.0);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //canvas.Width = Window.width  //размер холста изменяется вместе с изменением размера окна
        }
        /* private void Button_Click(object sender, RoutedEventArgs e)
{

}*/
    }
}
