using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private SystemDiagnosticsConfigCT sysdiag;
        public string Filename { get; private set; }

        public ConfigFile(string file)
        {
            Filename = file;
            XDoc = XDocument.Load(Filename);
        }

        public ConfigFile(XDocument doc)
        {
            XDoc = doc;
        }

        /// <summary>
        /// Config file XML
        /// </summary>
        public XDocument XDoc { get; }

        /// <summary>
        /// XML from the system.diagnostics section, whether existing, commented out, or newly created
        /// ALWAYS UNCOMMENTS commented element
        /// </summary>
        /// <returns></returns>
        public XElement SysDiagElement()
        {
            return SysDiagElement(out _);
        }

        /// <summary>
        /// XML from the system.diagnostics section, whether existing, commented out, or newly created
        /// ALWAYS UNCOMMENTS commented element
        /// </summary>
        /// <param name="WasComment">Notes whether it was deserialzed from a comment or was newly created</param>
        /// <returns></returns>
        public XElement SysDiagElement(out bool WasComment)
        {
            WasComment = false;

            // get existing element
            var sd = XDoc.Root.Elements(sdkeyname).FirstOrDefault(); 

            // or get commented out element
            if (sd == null)
            {
                IEnumerable<XComment> enumerable = XDoc.Root.GetDescendantsCommentedOut(sdkeyname);
                var sdcomment = enumerable.FirstOrDefault();
                sdcomment?.TryUncomment(out sd);
                WasComment = true;
            }

            // or add default element
            if (sd == null)
            {
                sd = new XElement(sdkeyname);
                XDoc.Root.Add(sd);
                WasComment = true; //"comment" in that it was not present in the original xml
                Debug.Print($"Creating blank {sdkeyname} instance");
            }
            return sd;
        }

        public SystemDiagnosticsConfigCT SysDiag
        {
            get
            {
                if (sysdiag == null)
                {
                    sysdiag = Deserialize<SystemDiagnosticsConfigCT>(SysDiagElement(out bool WasComment).ToString());
                    sysdiag.IsComment = WasComment;
                    if (WasComment)
                    {
                        Debug.Print($"Found and uncommented {sdkeyname} instance");
                    }
                }
                return sysdiag;
            } 
        }

        /// <summary>
        /// Update serialize the SysDiag object back into XDoc
        /// </summary>
        public void UpdateFromSysDiag()
        {
            SysDiagElement().ReplaceWith(SysDiagObjXml());
        }

        public void SaveXml()
        {
            UpdateFromSysDiag();
            if (SysDiag.IsComment)
            {
                SysDiagElement().CommentOut();
                XDoc.Save(Filename);
                // This implicitly uncomments the internal xml
                SysDiagElement();
            }
            else
            {
                XDoc.Save(Filename);
            }
        }

        public void SaveXml(string filename)
        {
            this.Filename = filename;
            SaveXml();
        }

        /// <summary>
        /// Reserialized XML of the SysDiag object 
        /// </summary>
        /// <returns></returns>
        public XElement SysDiagObjXml()
        {
            var xml = SerializeToXElement(SysDiag);
            return xml;
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
