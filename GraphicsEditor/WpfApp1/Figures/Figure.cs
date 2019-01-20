using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ServiceModel;
using System.Runtime.Serialization;
using WpfApp1.Helpers;

namespace WpfApp1.Figures
{
    [DataContract]
    public class Figure
    {
        public static int counter = 0;

        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [DataMember(Name = "points")]
        public List<Point> points;

        public Brush Fill { get; set; }

        [DataMember(Name="Fill")]
        private XamlSerializationWrapper<Brush> BrushSerializer
        {
            get { return new XamlSerializationWrapper<Brush>(Fill); }
            set { Fill = value.Element; }
        }

        public Pen Line;

        [DataMember(Name = "Line")]
        private XamlSerializationWrapper<Pen> LineSerializer
        {
            get { return new XamlSerializationWrapper<Pen>(Line); }
            set { Line = value.Element; }
        }

        virtual public bool HasIntersection(Rect rect)
        {
            return false;
        }

        public Figure()
        {

        }

        public Figure(Point point)
        {
            counter++;
            Name = counter.ToString();
            points = new List<Point> { point, point };
        }

        public virtual void Draw(DrawingContext drawingContext)
        {

        }

        public virtual void AddPoint(Point point)
        {

        }
    }

}
