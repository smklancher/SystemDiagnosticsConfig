using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SystemDiagnosticsConfig
{
    public abstract class LogDisplay
    {

        public virtual IEnumerable<string> AvailableLevels { get { yield return Level; } }
        //public abstract string ConfigFilename { get; }
        //public abstract ConfigFile Config { get; protected set; }
        public virtual string Details { get=>string.Empty; }
        public abstract bool Enabled { get; set; }
        public virtual string Level { get; set; } = string.Empty;
        //public abstract ListenerElementCT Listener { get; protected set; }
        //public abstract string ListenerType { get; set; }
        public abstract string LogLocation { get; set; }
        //public abstract FileInfo LogFile { get; }
        //public abstract string LogFileSize { get; }

        public abstract string ConfigLocation { get; }
        public abstract string Name { get; }

        public abstract void SaveConfig();
        public virtual void OpenConfig()
        {
            OpenFileOrParentFolder(ConfigLocation);
        }

        public virtual void OpenLog()
        {
            OpenFileOrParentFolder(LogLocation);
        }

        private void OpenFileOrParentFolder(string path)
        {
            try
            {
                FileInfo f = new FileInfo(path);
                if (f.Exists)
                {
                    Process.Start(f.FullName);
                }
                else if (f.Directory.Exists)
                {
                    Process.Start(f.Directory.FullName);
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}