using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SystemDiagnosticsConfig
{
    public class XmlSerializationElement // : IXmlSerializable
    {
        [XmlAnyElement]
        public XmlNode[] UnknownNodes;

        [XmlAnyAttribute]
        public XmlAttribute[] UnknownAttibutes;

        /// <summary>
        /// Object was programmatically uncommented before deserializing xml
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IsComment { get; set; }

        //XmlSchema IXmlSerializable.GetSchema()
        //{
        //    return null;
        //}

        //void IXmlSerializable.ReadXml(XmlReader reader)
        //{
        //    var s = new XmlSerializer(GetType());
        //    s.Deserialize(reader);
        //}

        //void IXmlSerializable.WriteXml(XmlWriter writer)
        //{
        //    var s = new XmlSerializer(GetType());
        //    s.Serialize(writer, this);
        //}
    }
}
