using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemDiagnosticsConfig;

namespace SystemDiagnosticsConfig
{
    public abstract class LogDefinition
    {
        public LogDefinition(ConfigFile config)
        {
            this.Config = config;
            LoadLogDetails();
        }

        public ConfigFile Config { get; protected set; }
        public ListenerElementCT Listener { get; protected set; }


        public virtual string Details { get; set; }

        protected abstract bool IsTrace { get; }

        protected abstract string DefaultListenerName { get; }
        protected abstract string DefaultListenerType { get; }

        protected abstract string DefaultLocation { get; }
        public abstract string Name { get; }

        public abstract string Level { get; set; }
        public virtual IEnumerable<string> AvailableLevels { get => Enumerable.Empty<string>(); }

        /// <summary>
        /// Return the existing listener element that meets this definition or null if not found
        /// </summary>
        /// <returns></returns>
        protected abstract ListenerElementCT FindExistingListener();

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
        }



            
    }
}
