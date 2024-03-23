
namespace SystemDiagnosticsConfig
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System.Linq;
    using System.ComponentModel;

    public partial class SystemDiagnosticsConfigCT
    {
        [XmlIgnore()]
        public Collection<SourceElementCT> SourcesEx
        {
            get
            {
                //this is needed since the Sources setter is private
                if (Sources == null)
                {
                    Sources = new Collection<SourceElementCT>();
                }
                return Sources;
            }
        }

        [XmlIgnore()]
        public SwitchesCT SwitchesEx
        {
            get
            {
                if (Switches == null)
                {
                    Switches = new SwitchesCT();
                }
                return Switches;
            }
        }


        /// <summary>
        /// Ensures extra properties are initialized on listener objects
        /// </summary>
        [XmlIgnore()]
        public ListenersCT SharedListenersEx
        {
            get
            {
                if (SharedListeners == null)
                {
                    SharedListeners = new ListenersCT();
                }
                SharedListeners?.Initialize(this);
                return SharedListeners;
            }

            set
            {
                value?.Initialize(this);
                SharedListeners = value;
            }
        }

        [XmlIgnore()]
        public IEnumerable<ListenerElementCT> AllListeners
        {
            get
            {
                if(SharedListeners?.Add != null)
                {
                    foreach (var l in SharedListenersEx.Add)
                    {
                        yield return l;
                    }
                }

                if (Trace?.ListenersEx?.Add != null)
                {
                    foreach (var l in Trace.ListenersEx.Add)
                    {
                        yield return l;
                    }
                }

                if (SourcesEx != null)
                {
                    foreach (var l in SourcesEx.SelectMany(x => x.ListenersEx.Add))
                    {
                        yield return l;
                    }
                }

                
            }
        }



        public ListenerElementCT AddListener(ListenerLocation location)
        {
            return AddListener(location, null);
        }

        /// <summary>
        /// Create a new listener that is attached in the right location
        /// </summary>
        /// <param name="location"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public ListenerElementCT AddListener(ListenerLocation location, SourceElementCT source)
        {
            ListenerElementCT listener=new ListenerElementCT();
            switch (location)
            {
                case ListenerLocation.Trace:
                    if (Trace == null)
                    {
                        Trace = new TraceCT();
                    }
                    Trace.ListenersEx.Add.Add(listener);
                    listener.Initialize(Trace, Trace.Listeners);
                    break;
                case ListenerLocation.ReferenceToShared:
                case ListenerLocation.Source:
                    if (source == null)
                    {
                        throw new ArgumentNullException(nameof(source));
                    }
                    source.ListenersEx.Add.Add(listener);
                    listener.Initialize(source, source.Listeners);
                    break;
                case ListenerLocation.Shared:
                    SharedListenersEx.Add.Add(listener);
                    listener.Initialize(this, SharedListeners);
                    break;
                case ListenerLocation.Unknown:
                default:
                    throw new InvalidOperationException("Cannot add listener to unknown location");
            }

            return listener;
        }
    }

    public partial class TraceCT
    {
        /// <summary>
        /// Ensures extra properties are initialized on listener objects
        /// </summary>
        [XmlIgnore()]
        public ListenersCT ListenersEx
        {
            get
            {
                if (Listeners == null)
                {
                    Listeners = new ListenersCT();
                }
                Listeners?.Initialize(this);
                return Listeners;
            }

            set
            {
                value?.Initialize(this);
                Listeners = value;
            }
        }
    }

    

    public partial class SourceElementCT
    {
        /// <summary>
        /// <para>Optional System.Boolean [False]</para>
        /// </summary>
        [DefaultValue(false)]
        [XmlAttribute("propagateActivity", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "boolean")]
        public bool propagateActivity { get; set; }


        /// <summary>
        /// Ensures extra properties are initialized on listener objects
        /// </summary>
        [XmlIgnore()]
        public ListenersCT ListenersEx
        {
            get
            {
                if (Listeners == null)
                {
                    Listeners = new ListenersCT();
                }
                Listeners?.Initialize(this);
                return Listeners;
            }

            set
            {
                value?.Initialize(this);
                Listeners = value;
            }
        }


        /// <summary>
        /// Shared listeners referencing this source
        /// </summary>
        /// <param name="SysDiag"></param>
        /// <returns></returns>
        public IEnumerable<ListenerElementCT> SharedListeners(SystemDiagnosticsConfigCT SysDiag)
        {
            foreach (var l in ListenersEx.Add)
            {
                if (l.Location == ListenerLocation.ReferenceToShared)
                {
                    yield return l.ReferencedListener(SysDiag);
                }
            }
        }

        public SwitchElementCT AddReferenceToSwitch(SystemDiagnosticsConfigCT SysDiag, string switchName, string defaultValue)
        {
            var sw = SysDiag.SwitchesEx.Add.Where(x => x.Name.ToLower() == switchName.ToLower()).FirstOrDefault();
            if (sw == null)
            {
                sw = new SwitchElementCT();
                sw.Name = switchName;
                sw.Value = defaultValue;
                SysDiag.SwitchesEx.Add.Add(sw);
            }

            this.SwitchName = SwitchName;
            return sw;
        }
    }
    
    public partial class ListenersCT
    {
        public void Initialize(XmlSerializationElement parent)
        {
            foreach(var l in this.Add)
            {
                l.Initialize(parent, this);
            }
        }
    }

    public partial class ListenerElementCT
    {
        [XmlIgnore()]
        public ListenerLocation Location { get; set; }

        [XmlIgnore()]
        public XmlSerializationElement Parent { get; private set; }

        [XmlIgnore()]
        public ListenersCT ParentCollection { get; private set; }

        public void Initialize(XmlSerializationElement parent, ListenersCT parentCollection)
        {
            this.Parent = parent;
            this.ParentCollection = parentCollection;

            if (parent is SystemDiagnosticsConfigCT)
            {
                Location = ListenerLocation.Shared; //actual listener
            }else if (parent is TraceCT)
            {
                Location = ListenerLocation.Trace;
            }else if (parent is SourceElementCT)
            {
                if (String.IsNullOrWhiteSpace(Type))
                {
                    Location = ListenerLocation.ReferenceToShared; //reference to actual listener
                }
                else
                {
                    Location = ListenerLocation.Source;
                }
            }
        }

        /// <summary>
        /// Remove this listener instance from it's parent's listeners collection
        /// </summary>
        public void DetachFromParent()
        {
            ParentCollection.Add.Remove(this);
        }

        public void ReattachToParent()
        {
            if (!IsAttached)
            {
                ParentCollection.Add.Add(this);
            }
        }

        [XmlIgnore()]
        public bool IsAttached
        {
            get
            {
                return ParentCollection.Add.Contains(this);
            }
        }


        
        public List<SourceElementCT> Sources()
        {
            var list = new List<SourceElementCT>();
            if (Location == ListenerLocation.Source || Location == ListenerLocation.ReferenceToShared)
            {
                list.Add((SourceElementCT)Parent);
            }
            else if (Location==ListenerLocation.Shared)
            {
                var SysDiag=(SystemDiagnosticsConfigCT)Parent;
                var s = SysDiag.Sources.Where(
                    source => source.ListenersEx.Add.Where(
                        l => l.Name.ToLower() == this.Name.ToLower()
                    ).FirstOrDefault() != null);
                list.AddRange(s);
            }else if (Location == ListenerLocation.Trace)
            {
                //trace has no sources
            }
                return list;
        }
        
        public ListenerElementCT ReferencedListener(SystemDiagnosticsConfigCT SysDiag)
        {
            if (Location == ListenerLocation.ReferenceToShared)
            {
                // need reference to top level sysdiag to get to shared listeners
                return SysDiag.SharedListenersEx.Add.Where(x => x.Name.ToLower() == Name.ToLower()).FirstOrDefault(); 
            }

            return null;
        }

        public SourceElementCT AddReferenceToSource(SystemDiagnosticsConfigCT SysDiag, string sourceName)
        {
            switch (Location)
            {
                case ListenerLocation.Shared:
                    //continue below
                    break;
                case ListenerLocation.Source:
                case ListenerLocation.ReferenceToShared:
                    return Sources().FirstOrDefault();
                case ListenerLocation.Unknown:
                case ListenerLocation.Trace:
                default:
                    throw new InvalidOperationException($"{Location.ToString("g")} cannot add reference to a source: {this.Name}");
            }

            var source= SysDiag.SourcesEx.Where(x => x.Name.ToLower() == sourceName.ToLower()).FirstOrDefault();
            if (source == null)
            {
                source = new SourceElementCT();
                source.Name = sourceName;
                SysDiag.SourcesEx.Add(source);
            }

            var lref=source.ListenersEx.Add.Where(x => x.Name.ToLower() == Name.ToLower()).FirstOrDefault();
            if (lref == null)
            {
                lref=SysDiag.AddListener(ListenerLocation.ReferenceToShared, source);
                lref.Name = this.Name;
            }

            return source;
        }

        [DefaultValue("None")]
        [XmlAttribute("traceOutputOptions", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TraceOutputOptionsString { get; set; }
        // enum needs to be redefined with Flags/powers-of-two

        //{
        //    // Generated serialization of the enum did not work directly, so serialize as string.
        //    get
        //    {
        //        return this._traceOutputOptions.ToString("g");
        //    }
        //    set
        //    {
        //        bool parseSuccess=Enum.TryParse(value,out this._traceOutputOptions);
        //        if (!parseSuccess)
        //        {
        //            Debug.Write($"Ignoring invalid TraceOutputOptions: {value}");
        //        }
        //    }
        //}


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
