
namespace SystemDiagnosticsConfig
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;
    
    
    
    public partial class SystemDiagnosticsConfigCT
    {
        
    }

    public partial class ListenerElementCT
    {


        /// <summary>
        /// <para>Optional System.String </para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("maxFileSizeKB", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "string")]
        public string MaxFileSizeKB { get; set; }

        /// <summary>
        /// <para>Optional System.String []</para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("maxFilesAmount", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "string")]
        public string MaxFilesAmount { get; set; }
    }
}
