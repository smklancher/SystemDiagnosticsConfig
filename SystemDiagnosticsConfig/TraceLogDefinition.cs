using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDiagnosticsConfig
{
    public abstract class TraceLogDefinition : LogDefinition
    {
        public TraceLogDefinition(ConfigFile config) : base(config)
        {

        }

        protected override bool IsTrace => true;

        public override IEnumerable<string> AvailableLevels
        {
            get
            {
                yield return "Trace";
            }
        }

        public override string Level { get => "Trace"; set { } }

        protected override ListenerElementCT FindExistingListener()
        {
            if (Config.SysDiag.Trace != null)
            {
                foreach (var l in Config.SysDiag.Trace.ListenersEx.Add)
                {
                    if (l.Name.ToLower() == DefaultListenerName.ToLower())
                    {
                        return l;
                    }
                }
            }
            return null;
        }
    }
}
