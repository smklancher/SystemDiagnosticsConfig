using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SystemDiagnosticsConfig
{
    public class ConfigFile
    {
        private readonly string filename;
        protected readonly XDocument xdoc;
        private SystemDiagnosticsConfigCT sysdiag;

        public ConfigFile(string file)
        {
            filename = file;
            xdoc = XDocument.Load(filename);
        }

        public ConfigFile(XDocument doc)
        {
            xdoc = doc;
        }

        private XElement SysDiagElementOrNull()
        {
            return xdoc.Descendants("system.diagnostics").FirstOrDefault();
        }

        public SystemDiagnosticsConfigCT SysDiag
        {
            get
            {
                if (sysdiag == null && SysDiagElementOrNull()!=null)
                {
                    sysdiag = Deserialize<SystemDiagnosticsConfigCT>(SysDiagElementOrNull().ToString());
                }
                return sysdiag;
            } 
        }

        private static T Deserialize<T>(string data) where T : class, new()
        {
            if (string.IsNullOrEmpty(data))
                return null;

            var ser = new XmlSerializer(typeof(T));

            using (var sr = new StringReader(data))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public static XElement SerializeToXElement(object o)
        {
            var doc = new XDocument();
            using (XmlWriter writer = doc.CreateWriter())
            {
                //this avoids xml namespace declaration
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces(
                                   new[] { XmlQualifiedName.Empty });
                XmlSerializer serializer = new XmlSerializer(o.GetType(), "");
                serializer.Serialize(writer, o, ns);
            }

            return doc.Root;
        }
    }
}
