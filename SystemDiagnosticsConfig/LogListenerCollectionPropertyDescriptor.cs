using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDiagnosticsConfig
{
    class LogListenerCollectionPropertyDescriptor : PropertyDescriptor
    {
        private LogListenerCollection collection = null;
        private int index = -1;

        public LogListenerCollectionPropertyDescriptor(LogListenerCollection coll, int idx) :
            base("#" + idx.ToString(), null)
        {
            this.collection = coll;
            this.index = idx;
        }

        public override AttributeCollection Attributes
        {
            get
            {
                return new AttributeCollection(null);
            }
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get
            {
                return this.collection.GetType();
            }
        }

        public override string DisplayName
        {
            get
            {
                LogListener c = this.collection[index];
                return $"({c.ListenerLocation.ToString()}) {c.ListenerName}";
            }
        }

        public override string Description
        {
            get
            {
                var c = this.collection[index];
                return $"({c.ListenerLocation.ToString()}) {c.ListenerName}\nSources: {String.Join(", ",c.Sources.Select(x=>x.Name))}";
            }
        }

        public override object GetValue(object component)
        {
            return this.collection[index];
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override string Name
        {
            get { return "#" + index.ToString(); }
        }

        public override Type PropertyType
        {
            get { return this.collection[index].GetType(); }
        }

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override void SetValue(object component, object value)
        {
            // this.collection[index] = value;
        }
    }
}
