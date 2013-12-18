using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Pellared.Utils
{
    public static class ReflectionUtils
    {
        public static IEnumerable<PropertyInfo> GetPropertyInfos(Type type)
        {
            Throw.IfNull(type, "type");

            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite);
        }

        public static object GetPropertyValue(object obj, string propertyName)
        {
            Throw.IfNull(obj, "obj");
            Throw.IfNot<ArgumentException>(!string.IsNullOrEmpty(propertyName));

            foreach (String part in propertyName.Split('.'))
            {
                if (obj == null)
                {
                    return null;
                }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null)
                {
                    return null;
                }

                obj = info.GetValue(obj, null);
            }

            return obj;
        }

        public static T GetPropertyValue<T>(object obj, string propertyName)
        {
            Throw.IfNull(obj, "obj");
            Throw.IfNot<ArgumentException>(!string.IsNullOrEmpty(propertyName));

            object result = GetPropertyValue(obj, propertyName);
            if (result == null)
            {
                return default(T);
            }

            // throws InvalidCastException if types are incompatible
            return (T)result;
        }

        [Obsolete]
        private static bool CopyAllProperties(object source, object target)
        {
            Type sourceType = source.GetType();
            Type targetType = target.GetType();

            if (!targetType.IsAssignableFrom(sourceType))
            {
                return false;
            }

            foreach (PropertyInfo propertyInfo in GetPropertyInfos(sourceType))
            {
                object sourceValue = propertyInfo.GetValue(source, null);
                propertyInfo.SetValue(target, sourceValue, null);
            }

            return true;
        }

        [Obsolete]
        private static IEnumerable<string> GetAllPropertyNames(object instance)
        {
            Type instanceType = instance.GetType();
            return GetPropertyInfos(instanceType).Select(x => x.Name);
        }
    }
}