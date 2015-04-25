using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Utilities.Serialization
{
    public static partial class Serialization
    {
        public static string XmlSerialize<T>(T serialObject)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (TextWriter writer = new StringWriter(sb))
            {
                ser.Serialize(writer, serialObject);
                return writer.ToString();
            }
        }

        public static string XmlSerialize(object serialObject)
        {
            StringBuilder sb = new StringBuilder(5000);
            XmlSerializer ser = new XmlSerializer(serialObject.GetType());
            using (TextWriter writer = new StringWriter(sb))
            {
                ser.Serialize(writer, serialObject);
                return writer.ToString();
            }
        }

        public static T XmlDeserialize<T>(string str)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(str))
            {
                return (T)mySerializer.Deserialize(reader);
            }
        }

        public static object XmlDeserialize(string str, Type type)
        {
            XmlSerializer mySerializer = new XmlSerializer(type);
            using (TextReader reader = new StringReader(str))
            {
                return mySerializer.Deserialize(reader);
            }
        }
    }
}
