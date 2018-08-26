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
        private const string sdkeyname = "system.diagnostics";
        private readonly string filename;
        private SystemDiagnosticsConfigCT sysdiag;

        public ConfigFile(string file)
        {
            filename = file;
            XDoc = XDocument.Load(filename);
        }

        public ConfigFile(XDocument doc)
        {
            XDoc = doc;
        }

        public XDocument XDoc { get; }

        public XElement SysDiagElement()
        {
            // get existing element
            var sd = XDoc.Root.Elements(sdkeyname).FirstOrDefault(); 

            // or get commented out element
            if (sd == null)
            {
                var sdcomment = XDoc.Root.GetDescendantsCommentedOut(sdkeyname).FirstOrDefault();
                sdcomment?.TryUncomment(out sd);
            }

            // or add default element
            if (sd == null)
            {
                sd = new XElement(sdkeyname);
                XDoc.Root.Add(sd);
            }
            return sd;
        }

        public SystemDiagnosticsConfigCT SysDiag
        {
            get
            {
                sysdiag = Deserialize<SystemDiagnosticsConfigCT>(SysDiagElement().ToString());
                return sysdiag;
            } 
        }

        public void UpdateFromSysDiag()
        {
            SysDiagElement().ReplaceWith(SysDiagObjXml());
        }

        public XElement SysDiagObjXml()
        {
            return SerializeToXElement(SysDiag);
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

        private static XElement SerializeToXElement(object o)
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
