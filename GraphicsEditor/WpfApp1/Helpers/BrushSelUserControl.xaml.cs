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

namespace WpfApp1.Helpers
{
    /// <summary>
    /// Логика взаимодействия для BrushSelUserControl.xaml
    /// </summary>
    public partial class BrushSelUserControl : UserControl
    {
        public static readonly DependencyProperty SelectedIndexProperty;
        public static readonly DependencyProperty SelectedItemProperty;

        static BrushSelUserControl()
        {
            SelectedIndexProperty = DependencyProperty.Register("SelectedIndex", typeof(int), typeof(BrushSelUserControl));
            SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(SolidColorBrush), typeof(BrushSelUserControl));
        }

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public object SelectedItem
        {
            get
            {
                return GetValue(SelectedItemProperty) as SolidColorBrush;
            }
            set {
                SetValue(SelectedItemProperty, value); }
        }

        public BrushSelUserControl()
        {
            InitializeComponent();

            Binding selectedIndexBinding = new Binding("SelectedIndex");
            selectedIndexBinding.Source = Content;
            selectedIndexBinding.Mode = BindingMode.TwoWay;

            this.SetBinding(BrushSelUserControl.SelectedIndexProperty, selectedIndexBinding);

            Binding selectedItemBinding = new Binding("SelectedItem");
            selectedItemBinding.Source = Content;
            selectedItemBinding.Mode = BindingMode.TwoWay;

            this.SetBinding(BrushSelUserControl.SelectedItemProperty, selectedItemBinding);
        }
    }
}
