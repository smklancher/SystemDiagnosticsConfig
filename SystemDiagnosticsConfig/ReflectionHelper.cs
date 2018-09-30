using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SystemDiagnosticsConfig
{
    public class ReflectionHelper
    {

        /// <summary>
        /// Given "() => this.MyProperty)": 
        /// 1. If MyProperty is null it will be set to a new instance of its type
        /// 2. Return MyProperty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property">"() => this.MyProperty)"</param>
        /// <returns></returns>
        public static T ValueOrSetAndReturnDefault<T>(Expression<Func<T>> property) where T : class, new()
        {
            var prop = ((MemberExpression)property.Body).Member as PropertyInfo;
            if (prop == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }

            var compiled = property.Compile();
            var parent = compiled.Target;

            var value=prop.GetValue(parent) as T;

            if (value != null)
            {
                return value;
            }

            // property value is null, so assign new object and return it
            prop.SetValue(parent, new T());

            value = prop.GetValue(parent) as T;
            return value;
        }
    }
}
