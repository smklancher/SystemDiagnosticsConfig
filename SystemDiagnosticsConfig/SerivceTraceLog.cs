using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDiagnosticsConfig
{
    public class SerivceTraceLog : LogConfig
    {
        public SerivceTraceLog(SystemDiagnosticsConfigCT SysDiag) : base(SysDiag)
        {
            //consider other BCL sources: https://stackoverflow.com/a/9561273/221018
            //https://docs.microsoft.com/en-us/dotnet/framework/wcf/diagnostics/tracing/recommended-settings-for-tracing-and-message-logging
            //https://docs.microsoft.com/en-us/dotnet/framework/wcf/samples/tracing-and-message-logging

            //make all sources use shared listener
        }

        private string ListenerName { get; set; } = "xml";
        private string ListenerType { get; set; } = "System.Diagnostics.XmlWriterTraceListener";
        private ListenerLocation ListenerLocation { get; set; } = ListenerLocation.Shared;
        

        public override string LogFileName {
            get
            {
                return ConfigHelper.GetSharedListenerOrNull(SysDiag, ListenerName)?.InitializeData ?? String.Empty;
            }
            set
            {
                ConfigHelper.SetSharedListener(SysDiag, ListenerName, ListenerType, value);
            }
        }
        public override bool Enabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}




