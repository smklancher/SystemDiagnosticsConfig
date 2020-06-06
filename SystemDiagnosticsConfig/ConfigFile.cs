using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    [TypeConverter(typeof(BlankExpandConverter))]
    public class ConfigFile
    {
        private const string sdkeyname = "system.diagnostics";
        private SystemDiagnosticsConfigCT sysdiag;

        public ConfigFile(string file)
        {
            Filename = file;
            XDoc = XDocument.Load(Filename);
            //Listeners = GetLogListeners();
        }

        public ConfigFile(XDocument doc)
        {
            XDoc = doc;
        }

        public IEnumerable<ConfigListener> NonDefinitionListeners
        {
            get
            {
                var DefListeners = Definitions.Select(x=>x.Listener);
                foreach(var l in SysDiag.AllListeners)
                {
                    //Not already in the list of listeners claimed by definitions
                    if (!DefListeners.Contains(l))
                    {
                        yield return new ConfigListener(this,l);
                    }
                }
            }
        }

        [Browsable(false)]
        public string Filename { get; private set; }

        /// <summary>
        /// Config file XML
        /// </summary>
        [Browsable(false)]
        public XDocument XDoc { get; }

        private List<LogDefinition> AddedDefinitions=new List<LogDefinition>();

        public LogDefinition AddDefinition<T>()
        {
            //if (!typeof(T).IsAssignableFrom(typeof(LogDefinition)))
            if (!typeof(LogDefinition).IsAssignableFrom(typeof(T)))
            {
                throw new InvalidOperationException($"Can only add types from {nameof(LogDefinition)}");
            }
            var ld=(LogDefinition)Activator.CreateInstance(typeof(T), this);
            AddedDefinitions.Add(ld);
            return ld;
        }

        [Browsable(false)]
        public IEnumerable<LogDefinition> Definitions
        {
            get
            {
                //Debug.Write(GetType().ToString());
                var props = GetType().GetProperties();
                foreach(var p in props)
                {
                    //Debug.WriteLine($"{p.Name} = {p.PropertyType.ToString()}");
                    if (p.CanRead && typeof(LogDefinition).IsAssignableFrom(p.PropertyType))
                    {
                        yield return (LogDefinition)p.GetValue(this);
                    }
                }
                foreach(var ld in AddedDefinitions)
                {
                    yield return ld;
                }
            }
        }

        //private LogDefinitionCollection ldc = new LogDefinitionCollection();

        //public LogDefinitionCollection DefinitionCollection
        //{
        //    get
        //    {
        //        foreach(var d in Definitions)
        //        {
        //            if (!ldc.Contains(d))
        //            {
        //                ldc.Add(d);
        //            }
        //        }
        //        return ldc;
        //    }
        //}

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

        private Dictionary<string, XNode> NodeIds { get; set; }

        [Browsable(false)]
        public SystemDiagnosticsConfigCT SysDiag
        {
            get
            {
                if (sysdiag == null)
                {
                    XElement input = SysDiagElement(out bool WasComment);
                    sysdiag = XmlSerializationElement.EnhancedDeserialize<SystemDiagnosticsConfigCT>(input, out Dictionary<string, XNode> nodeIds);
                    NodeIds = nodeIds;
                    sysdiag.IsComment = WasComment;
                }
                return sysdiag;
            } 
        }
        

        /// <summary>
        /// Reserialized XML of the SysDiag object 
        /// </summary>
        /// <returns></returns>
        public XElement SysDiagObjXml()
        {
            return XmlSerializationElement.EnhancedSerializeToXElement(SysDiag, NodeIds);
        }


        public void SaveXml()
        {
            // Update the xml by serializing the SysDiag object back into the XDoc
            SysDiagElement().ReplaceWith(SysDiagObjXml());
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


    }
}
