using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WpfApp1.Figures;

namespace WpfApp1.Helpers
{
    public static class Helper
    {

        public static object FromXml(Type t, string xml)
        {
            var dcs = new DataContractSerializer(t);
            var sr = new StringReader(xml);
            var xr = XmlReader.Create(sr);
            return Cast(t, dcs.ReadObject(xr));
        }

        public static string ToXml(object cts)  
        {
            Type t = cts.GetType();
            var dcs = new DataContractSerializer(t);
            using (var sb = new StringWriter())
            {
                using (var xs = XmlWriter.Create(sb, new XmlWriterSettings()
                    { Indent = false }))
                {
                    dcs.WriteObject(xs, cts);
                }
                return sb.ToString().Replace("\r\n", " ");
            }
        }

        private static Func<object, object> MakeCastDelegate(Type from, Type to)
        {
            var p = Expression.Parameter(typeof(object)); 
            return Expression.Lambda<Func<object, object>>(
                Expression.Convert(Expression.ConvertChecked(Expression.Convert(p, from), to), typeof(object)),
                p).Compile();
        }

        private static readonly Dictionary<Tuple<Type, Type>, Func<object, object>> CastCache
        = new Dictionary<Tuple<Type, Type>, Func<object, object>>();

        private static Func<object, object> GetCastDelegate(Type from, Type to)
        {
            lock (CastCache)
            {
                var key = new Tuple<Type, Type>(from, to);
                Func<object, object> cast_delegate;
                if (!CastCache.TryGetValue(key, out cast_delegate))
                {
                    cast_delegate = MakeCastDelegate(from, to);
                    CastCache.Add(key, cast_delegate);
                }
                return cast_delegate;
            }
        }

        public static object Cast(Type t, object o)
        {
            return GetCastDelegate(o.GetType(), t).Invoke(o);
        }
    }
}
