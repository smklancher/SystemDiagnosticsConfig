//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// This code was generated by XmlSchemaClassGenerator version 2.0.147.0.
namespace SystemDiagnosticsConfig
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Xml.Serialization;
    
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("SystemDiagnosticsConfigCT")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute("system.diagnostics")]
    public partial class SystemDiagnosticsConfigCT : XmlSerializationElement
    {
        
        /// <summary>
        /// <para>Optional System.Diagnostics.AssertSection </para>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("assert")]
        public AssertCT Assert { get; set; }
        
        /// <summary>
        /// <para>Optional System.Diagnostics.PerfCounterSection </para>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("performanceCounters")]
        public PerformanceCountersCT PerformanceCounters { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<SourceElementCT> _sources;
        
        /// <summary>
        /// <para>Optional System.Diagnostics.SourceElementsCollection </para>
        /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute("sources")]
        [System.Xml.Serialization.XmlArrayItemAttribute("source")]
        public System.Collections.ObjectModel.Collection<SourceElementCT> Sources
        {
            get
            {
                return this._sources;
            }
            private set
            {
                this._sources = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Sources-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Sources collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SourcesSpecified
        {
            get
            {
                return (this.Sources.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="SystemDiagnosticsConfigCT" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="SystemDiagnosticsConfigCT" /> class.</para>
        /// </summary>
        public SystemDiagnosticsConfigCT()
        {
            this._sources = new System.Collections.ObjectModel.Collection<SourceElementCT>();
        }
        
        /// <summary>
        /// <para>Optional System.Diagnostics.ListenerElementsCollection </para>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("sharedListeners")]
        public ListenersCT SharedListeners { get; set; }
        
        /// <summary>
        /// <para>Optional System.Diagnostics.SwitchElementsCollection </para>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("switches")]
        public SwitchesCT Switches { get; set; }
        
        /// <summary>
        /// <para>Optional System.Diagnostics.TraceSection </para>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("trace")]
        public TraceCT Trace { get; set; }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("assertCT")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AssertCT : XmlSerializationElement
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private bool _assertuienabled = true;
        
        /// <summary>
        /// <para>Optional System.Boolean [True]</para>
        /// </summary>
        [System.ComponentModel.DefaultValueAttribute(true)]
        [System.Xml.Serialization.XmlAttributeAttribute("assertuienabled", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="boolean")]
        public bool Assertuienabled
        {
            get
            {
                return this._assertuienabled;
            }
            set
            {
                this._assertuienabled = value;
            }
        }
        
        /// <summary>
        /// <para>Optional System.String []</para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("logfilename", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string Logfilename { get; set; }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("performanceCountersCT")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PerformanceCountersCT : XmlSerializationElement
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private int _filemappingsize = 524288;
        
        /// <summary>
        /// <para>Optional System.Int32 [524288]</para>
        /// </summary>
        [System.ComponentModel.DefaultValueAttribute(524288)]
        [System.Xml.Serialization.XmlAttributeAttribute("filemappingsize", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="int")]
        public int Filemappingsize
        {
            get
            {
                return this._filemappingsize;
            }
            set
            {
                this._filemappingsize = value;
            }
        }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("sourcesCT")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SourcesCT : XmlSerializationElement
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<SourceElementCT> _source;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("source")]
        public System.Collections.ObjectModel.Collection<SourceElementCT> Source
        {
            get
            {
                return this._source;
            }
            private set
            {
                this._source = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Source-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Source collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SourceSpecified
        {
            get
            {
                return (this.Source.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="SourcesCT" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="SourcesCT" /> class.</para>
        /// </summary>
        public SourcesCT()
        {
            this._source = new System.Collections.ObjectModel.Collection<SourceElementCT>();
        }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("SourceElementCT")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SourceElementCT : XmlSerializationElement
    {
        
        /// <summary>
        /// <para>Optional System.Diagnostics.ListenerElementsCollection </para>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("listeners")]
        public ListenersCT Listeners { get; set; }
        
        /// <summary>
        /// <para>Required System.String []</para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("name", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string Name { get; set; }
        
        /// <summary>
        /// <para>Optional System.String </para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("switchName", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string SwitchName { get; set; }
        
        /// <summary>
        /// <para>Optional System.String </para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("switchValue", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string SwitchValue { get; set; }
        
        /// <summary>
        /// <para>Optional System.String </para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("switchType", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string SwitchType { get; set; }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("listenersCT")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListenersCT : XmlSerializationElement
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<object> _clear;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("clear")]
        public System.Collections.ObjectModel.Collection<object> Clear
        {
            get
            {
                return this._clear;
            }
            private set
            {
                this._clear = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Clear-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Clear collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ClearSpecified
        {
            get
            {
                return (this.Clear.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="ListenersCT" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="ListenersCT" /> class.</para>
        /// </summary>
        public ListenersCT()
        {
            this._clear = new System.Collections.ObjectModel.Collection<object>();
            this._remove = new System.Collections.ObjectModel.Collection<ListenersCTRemove>();
            this._add = new System.Collections.ObjectModel.Collection<ListenerElementCT>();
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<ListenersCTRemove> _remove;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("remove")]
        public System.Collections.ObjectModel.Collection<ListenersCTRemove> Remove
        {
            get
            {
                return this._remove;
            }
            private set
            {
                this._remove = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Remove-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Remove collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RemoveSpecified
        {
            get
            {
                return (this.Remove.Count != 0);
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<ListenerElementCT> _add;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("add")]
        public System.Collections.ObjectModel.Collection<ListenerElementCT> Add
        {
            get
            {
                return this._add;
            }
            private set
            {
                this._add = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Add-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Add collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AddSpecified
        {
            get
            {
                return (this.Add.Count != 0);
            }
        }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ListenersCTRemove", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListenersCTRemove : XmlSerializationElement
    {
        
        /// <summary>
        /// <para>Required System.String </para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("name", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string Name { get; set; }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ListenerElementCT")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ListenerElementCT : XmlSerializationElement
    {
        
        /// <summary>
        /// <para>Optional System.Diagnostics.FilterElement </para>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("filter")]
        public FilterCT Filter { get; set; }
        
        /// <summary>
        /// <para>Required System.String </para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("name", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string Name { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private ListenerElementCTTraceOutputOptions _traceOutputOptions = SystemDiagnosticsConfig.ListenerElementCTTraceOutputOptions.None;
        
        /// <summary>
        /// <para>Optional System.Diagnostics.TraceOptions [None]</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ListenerElementCTTraceOutputOptions TraceOutputOptions
        {
            get
            {
                return this._traceOutputOptions;
            }
            set
            {
                this._traceOutputOptions = value;
            }
        }
        
        /// <summary>
        /// <para>Optional System.String </para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("type", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string Type { get; set; }
        
        /// <summary>
        /// <para>Optional System.String []</para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("initializeData", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string InitializeData { get; set; }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("filterCT")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FilterCT : XmlSerializationElement
    {
        
        /// <summary>
        /// <para>Optional System.String []</para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("initializeData", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string InitializeData { get; set; }
        
        /// <summary>
        /// <para>Required System.String []</para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("type", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string Type { get; set; }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("ListenerElementCTTraceOutputOptions")]
    public enum ListenerElementCTTraceOutputOptions
    {
        
        /// <summary>
        /// </summary>
        None,
        
        /// <summary>
        /// </summary>
        LogicalOperationStack,
        
        /// <summary>
        /// </summary>
        DateTime,
        
        /// <summary>
        /// </summary>
        Timestamp,
        
        /// <summary>
        /// </summary>
        ProcessId,
        
        /// <summary>
        /// </summary>
        ThreadId,
        
        /// <summary>
        /// </summary>
        Callstack,
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("switchesCT")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SwitchesCT : XmlSerializationElement
    {
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<object> _clear;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("clear")]
        public System.Collections.ObjectModel.Collection<object> Clear
        {
            get
            {
                return this._clear;
            }
            private set
            {
                this._clear = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Clear-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Clear collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ClearSpecified
        {
            get
            {
                return (this.Clear.Count != 0);
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Initialisiert eine neue Instanz der <see cref="SwitchesCT" /> Klasse.</para>
        /// <para xml:lang="en">Initializes a new instance of the <see cref="SwitchesCT" /> class.</para>
        /// </summary>
        public SwitchesCT()
        {
            this._clear = new System.Collections.ObjectModel.Collection<object>();
            this._remove = new System.Collections.ObjectModel.Collection<SwitchesCTRemove>();
            this._add = new System.Collections.ObjectModel.Collection<SwitchElementCT>();
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<SwitchesCTRemove> _remove;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("remove")]
        public System.Collections.ObjectModel.Collection<SwitchesCTRemove> Remove
        {
            get
            {
                return this._remove;
            }
            private set
            {
                this._remove = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Remove-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Remove collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RemoveSpecified
        {
            get
            {
                return (this.Remove.Count != 0);
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private System.Collections.ObjectModel.Collection<SwitchElementCT> _add;
        
        /// <summary>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("add")]
        public System.Collections.ObjectModel.Collection<SwitchElementCT> Add
        {
            get
            {
                return this._add;
            }
            private set
            {
                this._add = value;
            }
        }
        
        /// <summary>
        /// <para xml:lang="de">Ruft einen Wert ab, der angibt, ob die Add-Collection leer ist.</para>
        /// <para xml:lang="en">Gets a value indicating whether the Add collection is empty.</para>
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AddSpecified
        {
            get
            {
                return (this.Add.Count != 0);
            }
        }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("SwitchesCTRemove", AnonymousType=true)]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SwitchesCTRemove : XmlSerializationElement
    {
        
        /// <summary>
        /// <para>Required System.String []</para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("name", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string Name { get; set; }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("SwitchElementCT")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SwitchElementCT : XmlSerializationElement
    {
        
        /// <summary>
        /// <para>Required System.String []</para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("name", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string Name { get; set; }
        
        /// <summary>
        /// <para>Required System.String </para>
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute("value", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="string")]
        public string Value { get; set; }
    }
    
    /// <summary>
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("XmlSchemaClassGenerator", "2.0.147.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute("traceCT")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TraceCT : XmlSerializationElement
    {
        
        /// <summary>
        /// <para>Optional System.Diagnostics.ListenerElementsCollection </para>
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("listeners")]
        public ListenersCT Listeners { get; set; }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private bool _autoflush = false;
        
        /// <summary>
        /// <para>Optional System.Boolean [False]</para>
        /// </summary>
        [System.ComponentModel.DefaultValueAttribute(false)]
        [System.Xml.Serialization.XmlAttributeAttribute("autoflush", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="boolean")]
        public bool Autoflush
        {
            get
            {
                return this._autoflush;
            }
            set
            {
                this._autoflush = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private int _indentsize = 4;
        
        /// <summary>
        /// <para>Optional System.Int32 [4]</para>
        /// </summary>
        [System.ComponentModel.DefaultValueAttribute(4)]
        [System.Xml.Serialization.XmlAttributeAttribute("indentsize", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="int")]
        public int Indentsize
        {
            get
            {
                return this._indentsize;
            }
            set
            {
                this._indentsize = value;
            }
        }
        
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        private bool _useGlobalLock = true;
        
        /// <summary>
        /// <para>Optional System.Boolean [True]</para>
        /// </summary>
        [System.ComponentModel.DefaultValueAttribute(true)]
        [System.Xml.Serialization.XmlAttributeAttribute("useGlobalLock", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="boolean")]
        public bool UseGlobalLock
        {
            get
            {
                return this._useGlobalLock;
            }
            set
            {
                this._useGlobalLock = value;
            }
        }
    }
}

