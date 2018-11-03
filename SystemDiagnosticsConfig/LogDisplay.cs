using System.Collections.Generic;
using System.IO;

namespace SystemDiagnosticsConfig
{
    public abstract class LogDisplay
    {

        public virtual IEnumerable<string> AvailableLevels { get { yield return Level; } }
        //public abstract string ConfigFilename { get; }
        //public abstract ConfigFile Config { get; protected set; }
        public abstract string Details { get; }
        public abstract bool Enabled { get; set; }
        public abstract string Level { get; set; }
        //public abstract ListenerElementCT Listener { get; protected set; }
        //public abstract string ListenerType { get; set; }
        public abstract string LogLocation { get; set; }
        //public abstract FileInfo LogFile { get; }
        //public abstract string LogFileSize { get; }

        public abstract string ConfigLocation { get; }
        public abstract string Name { get; }

        public abstract void SaveConfig();

        public abstract void OpenConfig();

        public abstract void OpenLog();
    }
}