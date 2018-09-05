using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SystemDiagnosticsConfig
{
    public static class XmlUtils
    {
        public static XNode GetXElement(this XmlNode node)
        {
            XDocument xDoc = new XDocument();
            using (XmlWriter xmlWriter = xDoc.CreateWriter())
                node.WriteTo(xmlWriter);
            return xDoc.Root;
        }

        public static XmlNode GetXmlNode(this XNode element)
        {
            using (XmlReader xmlReader = element.CreateReader())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                return xmlDoc;
            }
        }

        /// <summary>
        /// Returns true if the text of the comment correctly parses as an XElement, available as out el (but with no parent!).
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="el"></param>
        /// <returns></returns>
        public static bool IsCommentValidXml(this XComment comment, out XElement el)
        {
            XDocument doc;
            try
            {
                doc = XDocument.Parse(comment.Value.Trim());
                el = doc.Root;
                return true;
            }
            catch (Exception)
            {
                el = null;
                return false;
            }
        }

        /// <summary>
        /// Return true if able to parse the comment as XML and replace comment with resulting XElement
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public static bool TryUncomment(this XComment comment, out XElement el)
        {
            if (comment.IsCommentValidXml(out el))
            {
                var parent = comment.Parent;
                var next = comment.NextNode;
                var prev = comment.PreviousNode;
                comment.ReplaceWith(el);
                //need to update el with the "connected" version of the element (same name, prev & next nodes)
                el=parent.Elements(el.Name).Where(x => x.NextNode == next && x.PreviousNode == prev).First();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Replace this element with a comment containing the element
        /// </summary>
        /// <param name="el"></param>
        public static void CommentOut(this XElement el)
        {
            XComment content = new XComment(el.ToString());
            el.ReplaceWith(content);
        }

        /// <summary>
        /// Return descendants by name even if they are commented out
        /// </summary>
        /// <param name="xbase"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IEnumerable<XComment> GetDescendantsCommentedOut(this XElement xbase, string name)
        {
            //get all comments that contain this name
            var comments = xbase.DescendantNodes().OfType<XComment>().Where(x => x.Value.Contains(name));


            // return any that parse to valid xml with this element name
            var commentedElements = comments.Where(x => {
                if (x.IsCommentValidXml(out XElement el))
                {
                    return (el.Name == name);
                }
                return false;
            });

            return commentedElements;
        }
    }
}
