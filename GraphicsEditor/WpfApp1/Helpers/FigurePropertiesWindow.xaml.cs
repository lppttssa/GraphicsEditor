using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Figures;

namespace WpfApp1.Helpers
{
    /// <summary>
    /// Логика взаимодействия для FigurePropertiesWindow.xaml
    /// </summary>
    public partial class FigurePropertiesWindow : Window
    {
        private bool IsRoundCorner = false;

        private List<LineStyle> _lines = new List<LineStyle>()
        {
            new LineStyle()
            {
                StrokeThickness =1,
                StrokeDashArray = new DoubleCollection(){1, 0}
            },
            new LineStyle()
            {
                StrokeThickness =2,
                StrokeDashArray = new DoubleCollection(){2,2}
            },
            new LineStyle()
            {
                StrokeThickness =3,
                StrokeDashArray = new DoubleCollection(){3,3}
            },
            new LineStyle()
            {
                StrokeThickness =4,
                StrokeDashArray = new DoubleCollection(){4,4}
            },
            new LineStyle()
            {
                StrokeThickness =5,
                StrokeDashArray = new DoubleCollection(){5,5}
            },
            new LineStyle()
            {
                StrokeThickness =6,
                StrokeDashArray = new DoubleCollection(){6,6}
            },
            new LineStyle()
            {
                StrokeThickness =7,
                StrokeDashArray = new DoubleCollection(){7,7}
            },
            new LineStyle()
            {
                StrokeThickness =8,
                StrokeDashArray = new DoubleCollection(){6, 3}
            },
            new LineStyle()
            {
                StrokeThickness =9,
                StrokeDashArray = new DoubleCollection(){8, 4}
            },
            new LineStyle()
            {
                StrokeThickness =10,
                StrokeDashArray = new DoubleCollection(){10, 2 }
            }
        };
        public List<LineStyle> Lines
        {
            get
            {
                return _lines;
            }
        }

        private List<Figure> _figures;
        public List<Figure> Figures
        {
            get
            {
                return _figures;
            }
            set
            {
                _figures = value;
                IsRoundCorner = _figures.All(f => f is RoundedRectangle);
                SetFigures();
            }
        }

        private void SetFigures()
        {
            panel.Children.Clear();
            var label = new TextBlock
            {
                Margin = new Thickness(2),
                Text = $"Выбраны фигуры ({_figures.Count} шт): "
            };
            panel.Children.Add(label);

            if (_figures.Count == 0)
            {
                foreach (UIElement item in mainGrid.Children)
                {
                    item.IsEnabled = false;
                }
                return;
            } else
            {
                foreach (UIElement item in mainGrid.Children)
                {
                    item.IsEnabled = true;
                }
            }

            foreach (var f in _figures)
            {
                 label = new TextBlock();
                label.Margin = new Thickness(2);
                label.Text = f.Name+"; ";
                panel.Children.Add(label);
            }
            if (_figures.Count == 1)
            {
                var f = _figures[0];
                var brushes = Palette.Brushes.ToList();
               
                int i = _lines.FindIndex(x => x.StrokeThickness == f.Line.Thickness);
                comboLinesWidth.SelectedIndex = i;
                i = brushes.IndexOf(f.Line.Brush as SolidColorBrush);
                brushStroke.SelectedIndex = i;
                i = brushes.IndexOf(f.Fill as SolidColorBrush);
                brushFill.SelectedIndex = i;
                if (IsRoundCorner)
                {
                    var roundRect = f as RoundedRectangle;
                    sliderRadiusX.Value = roundRect.RadiusX;
                    sliderRadiusY.Value = roundRect.RadiusY;
                }
            }
            sliderRadiusX.IsEnabled = IsRoundCorner;
            sliderRadiusY.IsEnabled = IsRoundCorner;
            labelRadiusX.IsEnabled = IsRoundCorner;
            labelRadiusY.IsEnabled = IsRoundCorner;
        }

        public FigurePropertiesWindow()
        {
            InitializeComponent();
            comboLinesWidth.ItemsSource = Lines;
            comboLinesStyle.ItemsSource = Lines;
        }

        private void BtnApplyClick(object sender, RoutedEventArgs e)
        {
            object objThick = comboLinesWidth.SelectedItem;
            object objStroke = brushStroke.SelectedItem;
            object objFill = brushFill.SelectedItem;
            object objStyle = comboLinesStyle.SelectedItem;

            foreach (var f in _figures)
            {
                if (objStroke != null)
                {
                    f.Line.Brush = objStroke as SolidColorBrush;
                }
                if (objThick != null)
                {
                    f.Line.Thickness = (objThick as LineStyle).StrokeThickness;
                }
                if (objFill != null)
                {
                    f.Fill = objFill as SolidColorBrush;
                }             
                if (objStyle != null)
                {
                    var dashes = (objStyle as LineStyle).StrokeDashArray;
                    f.Line.DashStyle = new DashStyle(dashes, dashes.Last());
                }
                if (IsRoundCorner)
                {
                    var roundRect = f as RoundedRectangle;
                    roundRect.RadiusX = sliderRadiusX.Value;
                    roundRect.RadiusY = sliderRadiusY.Value;
                }
            }
            Painter.Invalidate();
        }
    }
}
