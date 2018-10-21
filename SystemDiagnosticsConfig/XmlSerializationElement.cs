using System;
using System.Collections.Generic;
using System.IO;
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

        private Dictionary<string, XNode> NodeIds;

        public XElement OriginalXml(Dictionary<string, XNode> nodeIds)
        {
            nodeIds.TryGetValue(XmlNodeId, out XNode value);
            return value as XElement;
        }

        #region Serialization

        public static T EnhancedDeserialize<T>(XElement data, out Dictionary<string, XNode> nodeIds) where T : class, new()
        {
            CreateNodeIds(data, out nodeIds);
            T obj = Deserialize<T>(data.ToString());

            // This only sets the nodeids to the top level object, so... not that useful?
            XmlSerializationElement xse = obj as XmlSerializationElement;
            if (xse !=null)
            {
                xse.NodeIds = nodeIds;
            }

            return obj;
        }

        public static XElement EnhancedSerializeToXElement(object o, Dictionary<string, XNode> nodeIds)
        {
            XElement el = SerializeToXElement(o);

            RestoreComments(el, nodeIds);
            RemoveNodeIds(el);

            return el;
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

        #endregion


        #region Comment Preservation


        [System.ComponentModel.DefaultValueAttribute("")]
        [System.Xml.Serialization.XmlAttributeAttribute(nameof(XmlNodeId), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "string")]
        public string XmlNodeId { get; set; }

        private static string XmlNodeIdString => nameof(XmlNodeId);

        /// <summary>
        /// Create an attribute with an ID on each element, and add to dictionary
        /// </summary>
        /// <param name="input"></param>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        private static void CreateNodeIds(XElement input, out Dictionary<string, XNode> nodeIds)
        {
            nodeIds = new Dictionary<string, XNode>();

            long id = 0;
            var elements = input.Descendants();
            foreach (var e in elements)
            {
                nodeIds.Add(id.ToString(), e);
                e.SetAttributeValue(XmlNodeIdString, id);
                id++;
            }
        }

        /// <summary>
        /// Remove node ID attributes
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static void RemoveNodeIds(XElement input)
        {
            var elements = input.Descendants();
            foreach (var e in elements)
            {
                e.SetAttributeValue(XmlNodeIdString, null);
            }
        }

        private static XElement OriginalElementByNodeId(XElement input, Dictionary<string, XNode> nodeIds)
        {
            //does the xname need to use parent element namespace?
            string id = input.Attribute(XmlNodeIdString)?.Value;
            if (id == null) return null;
            nodeIds.TryGetValue(id, out XNode node);
            return (XElement)node;
        }

        /// <summary>
        /// Copy comments over from original xml if its parent element from the original is found
        /// </summary>
        /// <param name="input"></param>
        /// <param name="nodeIds"></param>
        private static void RestoreComments(XElement input, Dictionary<string, XNode> nodeIds)
        {
            var elements = input.Descendants();
            foreach (var e in elements)
            {
                var comments = OriginalElementByNodeId(e, nodeIds)?.Nodes()?.OfType<XComment>();
                if (comments != null)
                {
                    foreach (var c in comments)
                    {
                        e.AddFirst(c);
                    }

                }
            }
        }
        #endregion
    }
}
