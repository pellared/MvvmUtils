using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Pellared.Utils
{
    public static class ExpressionUtils
    {
        public static string ExtractPropertyName<TProperty>(Expression<Func<TProperty>> propertySelector)
        {
            // var propName1 = GetPropertyName(() => x.Property1);
            return ExtractPropertyNameImpl(propertySelector);
        }

        public static string ExtractPropertyName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> propertySelector)
        {
            // var propName2 = GetPropertyName<ObjectType, int>(y => y.Property2);
            return ExtractPropertyNameImpl(propertySelector);
        }

        public static string ExtractPropertyName<TEntity>(Expression<Func<TEntity, object>> propertySelector)
        {
            // var propName3 = GetPropertyName<ObjectType>(y => y.Property3);
            return ExtractPropertyNameImpl(propertySelector);
        }

        private static string ExtractPropertyNameImpl(LambdaExpression propertySelector)
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
    }
}