using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WpfApp1.Helpers
{
    class XamlSerializationWrapper<TElement> : IXmlSerializable
    {
        public TElement Element { get; private set; }

        protected XamlSerializationWrapper()
        {
        }

        public XamlSerializationWrapper(TElement element)
        {
            this.Element = element;
        }

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var element = (XElement)XElement.ReadFrom(reader);
            Element = (TElement)XamlReader.Parse(element.Elements().Single().ToString());
        }

        public void WriteXml(XmlWriter writer)
        {
            XamlWriter.Save(Element, writer);
        }
    }
}
