using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemDiagnosticsConfig
{
    internal class ConfigFileConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
        {
            if (destType == typeof(string) && value is ConfigFile)
            {
                // Cast the value to an Employee type
                ConfigFile c = (ConfigFile)value;

                // Return department and department role separated by comma.
                return String.Empty; // System.IO.Path.GetFileNameWithoutExtension(c.Filename);
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
    //internal class LogConfigConverter : ExpandableObjectConverter
    //{
    //    public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
    //    {
    //        if (destType == typeof(string) && value is LogListener)
    //        {
    //            // Cast the value to an Employee type
    //            LogListener c = (LogListener)value;

    //            return String.Empty; // System.IO.Path.GetFileName(c.LogFileName);
    //        }
    //        return base.ConvertTo(context, culture, value, destType);
    //    }
    //}

    internal class BlankExpandConverter: ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
        {
            return String.Empty; 
        }
    }


}
