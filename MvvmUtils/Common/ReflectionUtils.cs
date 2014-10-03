using Pellared.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Pellared.Common
{
    public static class ReflectionUtils
    {
        public static IEnumerable<PropertyInfo> GetPropertyInfos(Type type)
        {
            Ensure.NotNull(type, "type");

            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && p.CanWrite);
        }

        public static object GetPropertyValue(object obj, string propertyName)
        {
            Ensure.NotNull(obj, "obj");
            Ensure.NotEmpty(propertyName, "propertyName");

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
            Ensure.NotNull(obj, "obj");
            Ensure.NotEmpty(propertyName, "propertyName");

            object result = GetPropertyValue(obj, propertyName);
            if (result == null)
            {
                return default(T);
            }

            // throws InvalidCastException if types are incompatible
            return (T)result;
        }

        public static void CopyProperties<TParent, TChild>(TParent source, TChild target)
            where TChild : TParent
        {
            Type sourceType = source.GetType();
            Type targetType = target.GetType();
            
            foreach (PropertyInfo propertyInfo in GetPropertyInfos(sourceType))
            {
                object sourceValue = propertyInfo.GetValue(source, null);
                propertyInfo.SetValue(target, sourceValue, null);
            }
        }

        public static void CopyObjectProperties(object source, object target)
        {
            Type sourceType = source.GetType();
            Type targetType = target.GetType();

            if (!sourceType.IsAssignableFrom(targetType))
            {
                throw new ArgumentException("taget is not assignable to source type");
            }

            foreach (PropertyInfo propertyInfo in GetPropertyInfos(sourceType))
            {
                object sourceValue = propertyInfo.GetValue(source, null);
                propertyInfo.SetValue(target, sourceValue, null);
            }
        }
    }
}