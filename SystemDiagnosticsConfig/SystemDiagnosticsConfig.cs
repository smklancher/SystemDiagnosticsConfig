
namespace SystemDiagnosticsConfig
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Xml.Serialization;
    
    
    
    public partial class SystemDiagnosticsConfigCT
    {
        
    }

    public partial class ListenerElementCT
    {
        [System.ComponentModel.DefaultValueAttribute("None")]
        [XmlAttribute("traceOutputOptions", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TraceOutputOptionsString
        {
            get
            {
                return this._traceOutputOptions.ToString();
            }
            set
            {
                bool parseSuccess=Enum.TryParse(value,out this._traceOutputOptions);
                if (!parseSuccess)
                {
                    Debug.Write($"Ignoring invalid TraceOutputOptions: {value}");
                }
            }
        }


        /// <summary>
        /// <para>Optional System.String </para>
        /// </summary>
        [XmlAttribute("maxFileSizeKB", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "string")]
        public string MaxFileSizeKB { get; set; }

        /// <summary>
        /// <para>Optional System.String []</para>
        /// </summary>
        [XmlAttribute("maxFilesAmount", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "string")]
        public string MaxFilesAmount { get; set; }
    }
}
