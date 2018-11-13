using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDiagnosticsConfig
{
    [TypeConverter(typeof(BlankExpandConverter))]
    public class LogListener
    {
       
        //public LogListener(SystemDiagnosticsConfigCT SysDiag, ListenerElementCT listener)
        //{
        //    this.SysDiag = SysDiag;
        //    this.listener = listener;
        //    Sources = listener.Sources();
        //}

        //public SystemDiagnosticsConfigCT SysDiag { get; protected set; }
        //public ListenerElementCT listener { get; protected set; }

        //public string ListenerName { get { return listener.Name; } set { listener.Name = value; } }
        //public string ListenerType { get { return listener.Type; } set { listener.Type = value; } }
        //public string FileName { get { return listener.InitializeData; } set { listener.InitializeData = value; } }
        //public string Verbosity { get { return listener.Type; } set { listener.Type = value; } }
        //public ListenerLocation ListenerLocation { get { return listener.Location; } protected set { listener.Location = value; } }
        
        //public List<SourceElementCT> Sources { get; protected set; }

        //public abstract string LogFileName { get; set; }

        //public abstract bool Enabled { get; set; }

        ///// <summary>
        ///// Either the single source that owns this listener, or the sources that use this shared listener
        ///// </summary>
        ///// <returns></returns>
        //private List<SourceElementCT> GetSources()
        //{
        //    var list = new List<SourceElementCT>();
        //    if (ListenerLocation == ListenerLocation.Shared)
        //    {
        //        //SysDiag.Sources.Where(x=>x.Listeners?.Add.)
        //    }
        //    if (ListenerLocation == ListenerLocation.Source)
        //    {
        //        //probably need a reference passed in at creation
        //    }
        //    return list;
        //}
        
    }
}
