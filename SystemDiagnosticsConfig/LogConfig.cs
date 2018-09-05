using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDiagnosticsConfig
{
    public abstract class LogConfig
    {
        protected SystemDiagnosticsConfigCT SysDiag;
        public LogConfig(SystemDiagnosticsConfigCT SysDiag)
        {
            this.SysDiag = SysDiag;
        }

        public abstract string LogFileName { get; set; }

        public abstract bool Enabled { get; set; }
    }
}
