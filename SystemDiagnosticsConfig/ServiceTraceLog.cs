using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDiagnosticsConfig
{
    
    // Represent 1 listener to N sources
    // Auto convert source listeners single shared xml listener?  (what to do with other types?) 

    //public class ServiceTraceLog : LogListener
    //{
        //public SerivceTraceLog(SystemDiagnosticsConfigCT SysDiag) : base(SysDiag)
        //{
        //    //consider other BCL sources: https://stackoverflow.com/a/9561273/221018
        //    //https://docs.microsoft.com/en-us/dotnet/framework/wcf/diagnostics/tracing/recommended-settings-for-tracing-and-message-logging
        //    //https://docs.microsoft.com/en-us/dotnet/framework/wcf/samples/tracing-and-message-logging

        //    ListenerName = "xml";
        //    ListenerType = "System.Diagnostics.XmlWriterTraceListener";
        //    ListenerLocation = ListenerLocation.Shared;
        //}
        
        ////private string ListenerName { get; set; } = "xml";
        ////private string ListenerType { get; set; } = "System.Diagnostics.XmlWriterTraceListener";
        ////private ListenerLocation ListenerLocation { get; set; } = ListenerLocation.Shared;

        //private List<string> DefaultSources { get; } = new List<string>() { "System.ServiceModel" };
        
        //private void SetSources()
        //{
        //    foreach(var s in DefaultSources)
        //    {
        //        var src=SysDiag.Sources.Where(x => x.Name.ToLower() == s.ToLower()).FirstOrDefault();
        //        if (src == null)
        //        {
        //            src = new SourceElementCT();
        //            src.Name = s;
        //            //TODO: add shared listner reference by name
        //            SysDiag.Sources.Add(src);
        //        }
        //    }
        //}

        //public override string LogFileName {
        //    get
        //    {
        //        return ConfigHelper.GetSharedListenerOrNull(SysDiag, ListenerName)?.InitializeData ?? String.Empty;
        //    }
        //    set
        //    {
        //        ConfigHelper.SetSharedListener(SysDiag, ListenerName, ListenerType, value);
        //    }
        //}
        //public override bool Enabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //}
}




