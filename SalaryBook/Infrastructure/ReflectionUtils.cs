using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Pellared.Infrastructure
{
    public static class ReflectionUtils
    {
        public static bool CopyAllProperties(object source, object target)
        {
            Type sourceType = source.GetType();
            Type targetType = target.GetType();
            
            if (!IsContravariant(targetType, sourceType)) return false;

            foreach (PropertyInfo propertyInfo in GetPropertyInfos(sourceType))
            {
                object sourceValue = propertyInfo.GetValue(source, null);
                propertyInfo.SetValue(target, sourceValue, null);
            }
            return true;
        }

        public static string ExtractPropertyName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> propertySelector)
        {
            if (propertySelector == null)
                throw new ArgumentNullException("propertySelector");

            MemberExpression member = propertySelector.Body as MemberExpression
                                      ?? ((UnaryExpression)propertySelector.Body).Operand as MemberExpression;

            if (member == null)
                throw new InvalidOperationException("Expression is not an access expression.");

            PropertyInfo property = member.Member as PropertyInfo;
            if (property == null)
                throw new InvalidOperationException("Member in expression is not a property.");

            MethodInfo getMethod = property.GetGetMethod(true);
            if (!getMethod.IsStatic)
                return member.Member.Name;
            else
                throw new ArgumentException("Property in expression is static.");
        }

        public static IEnumerable<PropertyInfo> GetPropertyInfos(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite);
        }

        public static IEnumerable<string> GetAllPropertyNames(object instance)
        {
            Type instanceType = instance.GetType();
            return GetPropertyInfos(instanceType).Select(x => x.Name);
        }

        public static object GetPropertyValue(object obj, string propertyName)
        {
            foreach (String part in propertyName.Split('.'))
            {
                if (obj == null)
                    return null;

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null)
                    return null;

                obj = info.GetValue(obj, null);
            }

            return obj;
        }

        public static T GetPropertyValue<T>(object obj, String propertyName)
        {
            object result = GetPropertyValue(obj, propertyName);
            if (result == null)
                return default(T);

            // throws InvalidCastException if types are incompatible
            return (T)result;
        }

        public static bool IsContravariant(Type child, Type parent)
        {
            while (true)
            {
                if (child == null) return false;
                if (child == parent) return true;

                child = child.BaseType;
            }
        }
    }
}