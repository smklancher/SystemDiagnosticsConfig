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
    public class ConfigListener : LogDisplay
    {
        public ConfigListener(ConfigFile config, ListenerElementCT listener)
        {
            this.Config = config;
            this.Listener = listener;
            //LoadLogDetails();
        }

        public ConfigFile Config { get; protected set; }
        public ListenerElementCT Listener { get; protected set; }

        //protected Dictionary<string, string> DetailParts { get; } = new Dictionary<string, string>();

        //public override string Details
        //{
        //    get
        //    {
        //        return string.Join(", ", DetailParts.Values);
        //    }
        //}
        

        //public override IEnumerable<string> AvailableLevels { get { yield return Level; } }

        //public IEnumerable<string> StandardLevels
        //{
        //    get
        //    {
        //        //Off, Critical, Error, Warning, Information, Verbose or All)
        //        yield return "Off";
        //        yield return "Error";
        //        yield return "Warning";
        //        yield return "Information";
        //        yield return "Verbose";
        //        yield return "All";
        //    }
        //}

        /// <summary>
        /// Return the existing listener element that meets this definition or null if not found
        /// </summary>
        ///// <returns></returns>
        //protected abstract ListenerElementCT FindExistingListener();
        

        public override string ConfigLocation => Config.Filename;

        ///// <summary>
        ///// File at Location if rooted path, or file in config folder if not
        ///// </summary>
        //public FileInfo LogFile
        //{
        //    get
        //    {
        //        FileInfo f = null;
        //        try
        //        {
        //            if (Path.IsPathRooted(LogLocation))
        //            {
        //                f = new FileInfo(LogLocation);
        //            }
        //            else
        //            {
        //                string logfileInConfigPath = Path.Combine(Path.GetDirectoryName(Config.Filename), LogLocation);
        //                f = new FileInfo(logfileInConfigPath);
        //            }
                        
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine(ex.ToString());
        //            //not a valid file path
        //        }
        //        return f;
        //    }
        //}

        //public string LogFileSize
        //{
        //    get
        //    {
        //        if (LogFile?.Exists ?? false)
        //        {
        //            return ToBytesCount(LogFile.Length);
        //        }
        //        return string.Empty;
        //    }
        //}
        //public static string ToBytesCount(long bytes)
        //{
        //    int unit = 1024;
        //    if (bytes < unit) return bytes + " b";
        //    int exp = (int)(Math.Log(bytes) / Math.Log(unit));
        //    return string.Format("{0:##.##} {1}b", bytes / Math.Pow(unit, exp), "KMGTPE"[exp - 1]);
        //}


        public override bool Enabled
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

        public override string LogLocation
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


        public string ListenerType
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

        public override string Name => $"{Listener.Location.ToString()} Listener: {Listener.Name}";


        //protected virtual void ApplyDefaults()
        //{
        //    Listener.Name = DefaultListenerName;
        //    Listener.Type = DefaultListenerType;
        //    Listener.InitializeData = DefaultLocation;
        //}

        //private void LoadLogDetails()
        //{
        //    Listener=FindExistingListener();
        //    if (Listener == null)
        //    {
        //        //Create listener Trace or Shared (preferring not to create a single source listener)
        //        Listener=Config.SysDiag.AddListener(IsTrace ? ListenerLocation.Trace : ListenerLocation.Shared);

        //        //Set defaults on the listener
        //        ApplyDefaults();
        //    }

        //    CreateDependentElements();

        //    DetailParts[nameof(LogFileSize)] = LogFileSize;
        //    DetailParts[nameof(FileLockInfo.LockDesc)] = FileLockInfo.LockDesc(LogFile?.FullName);
        //}

        ///// <summary>
        ///// Create any switches, sources/listener-references
        ///// </summary>
        //protected virtual void CreateDependentElements()
        //{

        //}

        //public override void OpenLog()
        //{
        //    // event log (check type, or recognize name) = open event log
        //    if (ListenerType.Contains("eventlog"))
        //    {
        //        Process.Start("eventvwr.msc");
        //        return;
        //    }

        //    FileInfo f = LogFile;

        //    if (f.Exists)
        //    {
        //        Process.Start(f.FullName);
        //    }
        //    else if (f.Directory.Exists)
        //    {
        //        Process.Start(f.Directory.FullName);
        //    }

        //    // if no criteria above is met, do nothing
        //}

        public override void SaveConfig()
        {
            Config.SaveXml();
        }

        //public override void OpenConfig()
        //{
        //    Process.Start(Config.Filename);
        //}
    }
}
