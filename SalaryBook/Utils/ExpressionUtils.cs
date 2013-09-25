using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Pellared.Utils
{
    public static class ExpressionUtils
    {
        public static string GetPropertyName<TProperty>(Expression<Func<TProperty>> propertySelector)
        {
            // var propName1 = GetPropertyName(() => x.Property1);
            return GetPropertyNameImpl(propertySelector);
        }

        public static string GetPropertyName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> propertySelector)
        {
            // var propName2 = GetPropertyName<ObjectType, int>(y => y.Property2);
            return GetPropertyNameImpl(propertySelector);
        }

        public static string GetPropertyName<TEntity>(Expression<Func<TEntity, object>> propertySelector)
        {
            // var propName3 = GetPropertyName<ObjectType>(y => y.Property3);
            return GetPropertyNameImpl(propertySelector);
        }

        private static string GetPropertyNameImpl(LambdaExpression propertySelector)
        {
            if (propertySelector == null)
                throw new ArgumentNullException("propertySelector");

            MemberExpression member = RemoveUnary(propertySelector.Body);
            if (member == null)
                throw new InvalidOperationException("Expression is not an access expression.");

            PropertyInfo property = member.Member as PropertyInfo;
            if (property == null)
                throw new InvalidOperationException("Member in expression is not a property.");

            return member.Member.Name;
        }

        public static string GetName<T>(Expression<Func<T>> selector)
        {
            if (selector == null)
                throw new ArgumentNullException("selector");

            MemberExpression member = RemoveUnary(selector.Body);
            if (member == null)
                throw new InvalidOperationException("Unable to get name from expression.");

            return member.Member.Name;
        }

        private static MemberExpression RemoveUnary(Expression toUnwrap)
        {
            if (toUnwrap is UnaryExpression)
            {
                return ((UnaryExpression)toUnwrap).Operand as MemberExpression;
            }

            return toUnwrap as MemberExpression;
        }
    }
}