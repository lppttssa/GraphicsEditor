using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Helpers;
using WpfApp1.Tools;
using Microsoft.Win32;
using WpfApp1.Figures;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public bool isPressed;

        protected FigurePropertiesWindow wndProp = null;

        protected List<Figure> selectedFigures;

        protected List<Figure> cloneFigures;

        protected Point mousePos;

        public static readonly RoutedCommand CopyCommand = 
            new RoutedUICommand("Copy", "CopyCommand", typeof(MainWindow), 
            new InputGestureCollection(new InputGesture[]
        {
            new KeyGesture(Key.C, ModifierKeys.Control)
        }));

        public static readonly RoutedCommand PasteCommand =
            new RoutedUICommand("Paste", "PasteCommand", typeof(MainWindow),
            new InputGestureCollection(new InputGesture[]
        {
            new KeyGesture(Key.V, ModifierKeys.Control)
        }));

        public static readonly RoutedCommand IncreaseZOrderCommand =
            new RoutedUICommand("IncreaseZOrder", "IncreaseZOrderCommand", typeof(MainWindow),
            new InputGestureCollection(new InputGesture[]
        {
            new KeyGesture(Key.Q, ModifierKeys.Control)
        }));

        public MainWindow()
        {
            InitializeComponent();
            this.Closed += MainWindow_Closed;
            MyCanvas.Children.Add(Painter.FigureHost);
            Painter.MainCanvas = MyCanvas;
            Painter.MainScrollViewer = MyScrollViewer;
            for (int i = 0; i < Painter.Tools.Count; i++)  //кнопки инструментов
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
                btn.Height = 30;
                btn.Width = 40;
                btn.Background = img;
                btn.Content = "";
                btn.Tag = i;
                btn.HorizontalAlignment = HorizontalAlignment.Left;
                btn.Click += new RoutedEventHandler(Tool_Click);
                if (Painter.Tools[i] is ZoomRectangleTool)
                {
                    var zTool = Painter.Tools[i] as ZoomRectangleTool;
                    zTool.EventZoomRectangle += ZTool_EventZoomRectangle;
                } else
                if (Painter.Tools[i] is SelectRectangleTool)
                {
                    var sTool = Painter.Tools[i] as SelectRectangleTool;
                    sTool.EventSelectRectangle += EventSelectRectangle;
                }
            }

            Brush[] colors =                //заполнение палитры
            {
                Brushes.Crimson,Brushes.Maroon,Brushes.DeepPink,Brushes.DarkOrange,Brushes.Yellow,Brushes.Fuchsia,Brushes.BlueViolet,Brushes.Indigo,Brushes.Lime,Brushes.Teal,
                Brushes.Aqua,Brushes.LightCyan,Brushes.Blue,Brushes.Navy,Brushes.Ivory,Brushes.Black,

            };

            foreach (var brush in colors)    //кнопки палитры
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

            foreach (var brush in colors)     //кнопки палитры
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

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            if (wndProp != null)
            {
                wndProp.Close();
            }
        }

        private void EventSelectRectangle(Rect rect)  //выделение и окно свойств
        {
            selectedFigures = Painter.SelectFigures(rect);
            if (wndProp == null)
            {
                wndProp = new FigurePropertiesWindow();
                wndProp.Closed += wndProp_Closed;
            }
            wndProp.Figures = selectedFigures;
            wndProp.Show();
        }

        private void wndProp_Closed(object sender, EventArgs e)
        {
            wndProp = null;
        }

        private void ZTool_EventZoomRectangle(Rect rect)  //Zoom
        {
            float fraction = (float)(MyScrollViewer.ActualWidth / rect.Width);
            Point pt = rect.BottomRight;
            pt.Offset(-rect.Width/2, -rect.Height/2);
            Painter.Zoom(fraction, pt);
            Painter.Invalidate();
        }


        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isPressed = true;
            Painter.SelectedTool.MouseDown(e.GetPosition(MyCanvas));
            //selectedTool.MouseDown(e.GetPosition(Canvas), Canvas); 
            Painter.Invalidate();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos = e.GetPosition(MyCanvas);

            if (isPressed)
            {
                Painter.SelectedTool.MouseMove(e.GetPosition(MyCanvas));
                //selectedTool.MouseMove(e.GetPosition(Canvas), Canvas); 
                Painter.Invalidate();
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isPressed = false;
            Painter.SelectedTool.MouseUp(e.GetPosition(MyCanvas));
            Painter.Invalidate();
        }




        private void Tool_Click(object sender, RoutedEventArgs e) //выбранный инструмент
        {
            Painter.SelectedTool = Painter.Tools[Convert.ToInt32((sender as Button).Tag)]; 
        }

        private void ButtonFill_Click(object sender, RoutedEventArgs e)  //заливка
        {
            Painter.SelectedFill = (sender as Button).Tag as Brush;
            btn1.Background = Painter.SelectedFill;
        }

        private void ButtonLine_Click(object sender, RoutedEventArgs e)  //линия
        {
            Painter.SelectedLine = new Pen ((sender as Button).Background, 2.0);
            btn2.Background = (sender as Button).Background;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //canvas.Width = Window.width  //размер холста изменяется вместе с изменением размера окна
            MyCanvas.Width = e.NewSize.Width;
            MyCanvas.Height = e.NewSize.Height;
        }

        private void BtnZoomInClick(object sender, RoutedEventArgs e) //Zoom c кнопки
        {
            Painter.Zoom(1.1f);
            Painter.Invalidate();
        }

        private void BtnZoomOutClick(object sender, RoutedEventArgs e)  //Zoom с кнопки
        {
            Painter.Zoom(0.9f);
            Painter.Invalidate();
        }

        private void MenuSaveClick(object sender, RoutedEventArgs e) //сохранение в КАРТОХЕ
        {
            MemoryStream ms = Painter.SaveFiguresAsStream();
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.potato) |*.potato";
            if (saveFileDialog.ShowDialog() == true)
            {
                string fname = saveFileDialog.FileName;
                System.IO.File.WriteAllBytes(fname, ms.ToArray());
                /*MessageBox.Show("Готово!");*/
            }
        }

        private void MenuExportAsPngClick(object sender, RoutedEventArgs e) //сохранение в png
        {
            MemoryStream ms = Painter.SaveCanvasAsStream();

            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.png) |*.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                string fname = saveFileDialog.FileName;
                System.IO.File.WriteAllBytes(fname, ms.ToArray());
                /*MessageBox.Show("Готово!");*/
            }
        }

        private void MenuOpenClick(object sender, RoutedEventArgs e)  //открыть КАРТОХУ
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.potato) |*.potato";
            if (openFileDialog.ShowDialog() == true)
            {
                string fname = openFileDialog.FileName;
                byte[] bytes = System.IO.File.ReadAllBytes(fname);
                Painter.OpenFiguresFromBytes(bytes);
                /*MessageBox.Show("Готово!");*/
            }
        }

        private void MenuNewClick(object sender, RoutedEventArgs e)  //открыть новый файл
        {
            Painter.Figures.Clear();
            Figure.counter = 0;
            Painter.Invalidate();
        }

        private void Copy()  //копи
        {
            if (selectedFigures == null) return;
            if (cloneFigures != null) cloneFigures.Clear();
            cloneFigures = new List<Figure>();
            foreach (var f in selectedFigures)
            {
                Type type = f.GetType();
                string xml = Helper.ToXml(f);
                var clone = (Figure)Helper.FromXml(type, xml);
                cloneFigures.Add(clone);
            }
        }

        private void Paste()  //пэйст
        {
            if (cloneFigures == null) return;

            Painter.InsertCloneFigures(cloneFigures, mousePos);

            cloneFigures.Clear();
            cloneFigures = null;
        }

        private void IncreaseZOrder()  //Z-порядок
        {
            if (selectedFigures == null) return;
            foreach (var figCurrent in selectedFigures)
            {
                int index = Painter.Figures.IndexOf(figCurrent);
                if (index > -1)
                {
                    if (index < Painter.Figures.Count-1)
                    {
                        var figNext = Painter.Figures[index + 1];
                        Painter.Figures[index + 1] = figCurrent;
                        Painter.Figures[index] = figNext;
                    }
                }
            }
            Painter.Invalidate();
        }

        private void MenuCopyClick(object sender, RoutedEventArgs e)
        {
            Copy();
        }

        private void MenuPasteClick(object sender, RoutedEventArgs e)
        {
            Paste();
        }

        private void MenuIncreaseZOrderClick(object sender, RoutedEventArgs e)
        {
            IncreaseZOrder();
        }

        private void MenuCopyClick(object sender, ExecutedRoutedEventArgs e)
        {
            Copy();
        }

        private void MenuPasteClick(object sender, ExecutedRoutedEventArgs e)
        {
            Paste();
        }

        private void MenuIncreaseZOrderClick(object sender, ExecutedRoutedEventArgs e)
        {
            IncreaseZOrder();
        }

        private void MenuExitClick(object sender, RoutedEventArgs e)
        {
            wndProp?.Close();
            this.Close();
        }

        private void imgMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MyCanvas.Height = e.NewSize.Height;
            MyCanvas.Width = e.NewSize.Width;
        }
    }
}
