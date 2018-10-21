using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDiagnosticsConfig;

namespace SystemDiagnosticsConfig
{
    [TypeConverter(typeof(BlankExpandConverter))]
    public abstract class LogDefinition
    {
        public LogDefinition(ConfigFile config)
        {
            this.Config = config;
            LoadLogDetails();
        }

        public ConfigFile Config { get; protected set; }
        public ListenerElementCT Listener { get; protected set; }

        protected Dictionary<string, string> DetailParts { get; } = new Dictionary<string, string>();

        public virtual string Details
        {
            get
            {
                return string.Join(", ", DetailParts.Values);
            }
        }


        protected abstract bool IsTrace { get; }

        protected abstract string DefaultListenerName { get; }
        protected abstract string DefaultListenerType { get; }

        public abstract string DefaultLocation { get; set; }
        public abstract string Name { get; }

        public abstract string Level { get; set; }
        public virtual IEnumerable<string> AvailableLevels { get { yield return Level; } }

        public IEnumerable<string> StandardLevels
        {
            get
            {
                //Off, Critical, Error, Warning, Information, Verbose or All)
                yield return "Off";
                yield return "Error";
                yield return "Warning";
                yield return "Information";
                yield return "Verbose";
                yield return "All";
            }
        }

        /// <summary>
        /// Return the existing listener element that meets this definition or null if not found
        /// </summary>
        /// <returns></returns>
        protected abstract ListenerElementCT FindExistingListener();

        public string ConfigFilename => Config.Filename;

        /// <summary>
        /// File at Location if rooted path, or file in config folder if not
        /// </summary>
        public FileInfo LogFile
        {
            get
            {
                FileInfo f = null;
                try
                {
                    if (Path.IsPathRooted(Location))
                    {
                        f = new FileInfo(Location);
                    }
                    else
                    {
                        string logfileInConfigPath = Path.Combine(Path.GetDirectoryName(Config.Filename), Location);
                        f = new FileInfo(logfileInConfigPath);
                    }
                        
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    //not a valid file path
                }
                return f;
            }
        }

        public string LogFileSize
        {
            get
            {
                if (LogFile?.Exists ?? false)
                {
                    return ToBytesCount(LogFile.Length);
                }
                return string.Empty;
            }
        }
        public static string ToBytesCount(long bytes)
        {
            int unit = 1024;
            if (bytes < unit) return bytes + " b";
            int exp = (int)(Math.Log(bytes) / Math.Log(unit));
            return string.Format("{0:##.##} {1}b", bytes / Math.Pow(unit, exp), "KMGTPE"[exp - 1]);
        }


        public bool Enabled
        {
            get
            {
                return Listener?.IsAttached ?? false; 
            }
            set
            {
                if (value)
                {
                    Listener.ReattachToParent();
                }
                else
                {
                    Listener.DetachFromParent();
                }
            }
        }

        public virtual string Location
        {
            get
            {
                return Listener?.InitializeData ?? string.Empty;
            }
            set
            {
                if (Listener != null)
                {
                    Listener.InitializeData = value;
                }
            }

        }


        public virtual string ListenerType
        {
            get
            {
                return Listener?.Type ?? string.Empty;
            }
            set
            {
                if (Listener != null)
                {
                    Listener.Type = value;
                }
            }

        }


        protected virtual void ApplyDefaults()
        {
            Listener.Name = DefaultListenerName;
            Listener.Type = DefaultListenerType;
            Listener.InitializeData = DefaultLocation;
        }

        private void LoadLogDetails()
        {
            Listener=FindExistingListener();
            if (Listener == null)
            {
                //Create listener Trace or Shared (preferring not to create a single source listener)
                Listener=Config.SysDiag.AddListener(IsTrace ? ListenerLocation.Trace : ListenerLocation.Shared);

                //Set defaults on the listener
                ApplyDefaults();
            }

            CreateDependentElements();

            DetailParts[nameof(LogFileSize)] = LogFileSize;
            DetailParts[nameof(FileLockInfo.LockDesc)] = FileLockInfo.LockDesc(LogFile?.FullName);
        }

        /// <summary>
        /// Create any switches, sources/listener-references
        /// </summary>
        protected virtual void CreateDependentElements()
        {

        }

        
    }
}
