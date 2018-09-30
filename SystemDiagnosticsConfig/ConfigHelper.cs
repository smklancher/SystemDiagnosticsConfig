using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDiagnosticsConfig;

namespace SystemDiagnosticsConfig
{
    public enum ListenerLocation
    {
        Unknown,
        Trace,
        Source,
        Shared,
        ReferenceToShared
    }

    public class ConfigHelper
    {
        

        public static ListenerElementCT SetSharedListener(SystemDiagnosticsConfigCT SysDiag, string name, string type, string initializeData)
        {
            return SetListener(SysDiag, name, type, initializeData, ListenerLocation.Shared);
        }

        public static ListenerElementCT SetTraceListener(SystemDiagnosticsConfigCT SysDiag, string name, string type, string initializeData)
        {
            return SetListener(SysDiag, name, type, initializeData, ListenerLocation.Trace);
        }

        public static ListenerElementCT GetListenerOrNull(SystemDiagnosticsConfigCT SysDiag, string name)
        {
            return SysDiag?.Trace?.ListenersEx?.Add?.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public static ListenerElementCT GetSharedListenerOrNull(SystemDiagnosticsConfigCT SysDiag, string name)
        {
            return SysDiag?.SharedListenersEx?.Add?.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }
        public static ListenerElementCT GetSharedListenerOrNull(SystemDiagnosticsConfigCT SysDiag, string sourceName, string name)
        {
            return SysDiag?.Sources?.Where(x => x.Name.ToLower() == sourceName.ToLower()).FirstOrDefault()?.ListenersEx?.Add?.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public static ListenerElementCT SetListener(SystemDiagnosticsConfigCT SysDiag, string name, string type, string initializeData, ListenerLocation listenerType)
        {
            ListenersCT listeners = null;

            switch (listenerType)
            {
                case ListenerLocation.Shared:
                    //if (SysDiag.SharedListeners == null)
                    //{
                    //    SysDiag.SharedListeners = new ListenersCT();
                    //}
                    listeners = SysDiag.SharedListenersEx;
                    break;

                case ListenerLocation.Trace:
                    if (SysDiag.Trace == null)
                    {
                        SysDiag.Trace = new TraceCT();
                    }

                    //if (SysDiag.Trace.Listeners == null)
                    //{
                    //    SysDiag.Trace.Listeners = new ListenersCT();
                    //}
                    listeners = SysDiag.Trace.ListenersEx;
                    break;

                case ListenerLocation.Source:
                    throw new NotImplementedException();
                case ListenerLocation.Unknown:
                    throw new InvalidOperationException();
            }

            // Get the trace listener with given name if exists
            var add = listeners.Add.Where(x => x.Name == name).FirstOrDefault();

            if (add == null)
            {
                // Add new if no listener with this name
                add = new ListenerElementCT
                {
                    Name = name,
                    Type = type,
                    InitializeData = initializeData
                };
                listeners.Add.Add(add);
            }
            else
            {
                add.Type = type;
                add.InitializeData = initializeData;
            }

            return add;
        }
    }
}
