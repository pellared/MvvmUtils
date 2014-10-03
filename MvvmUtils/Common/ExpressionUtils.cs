using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Pellared.Common
{
    public static class ExpressionUtils
    {
        public static T GetValue<T>(Expression<Func<T>> argument)
        {
            object value;

            var memberExpression = argument.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new InvalidOperationException("Expression is not an access expression.");
            }

            var constantExpression = memberExpression.Expression as ConstantExpression;
            if (constantExpression != null)
            {
                if (memberExpression.Member.MemberType == MemberTypes.Property)
                {
                    value = ((PropertyInfo)memberExpression.Member).GetValue(constantExpression.Value, null);
                }
                else
                {
                    value = ((FieldInfo)memberExpression.Member).GetValue(constantExpression.Value);
                }
            }
            else
            {
                value = argument.Compile().DynamicInvoke();
            }

            return (T)value;
        }

        /// <summary>
        /// Gets the name of any argument given in the lambda expression.
        /// Sample:
        /// int argument = 10;
        /// string name = ExpressionUtils.GetName(() => argument);
        /// </summary>
        /// <typeparam name="T">Argument type</typeparam>
        /// <param name="selector">Selector for the name of the argument</param>
        /// <returns>Argument name</returns>
        public static string GetName<T>(Expression<Func<T>> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            MemberExpression member = RemoveUnary(selector.Body);
            if (member == null)
            {
                throw new InvalidOperationException("Unable to get name from expression.");
            }

            return member.Member.Name;
        }

        /// <summary>
        /// Gets the name of the property given in the lambda expression.
        /// Sample:
        /// string propertyName = ExpressionUtils.GetPropertyName(() => x.Property);
        /// </summary>
        /// <typeparam name="TProperty">Property type</typeparam>
        /// <param name="propertySelector">Selector for the name of the property</param>
        /// <returns></returns>
        public static string GetPropertyName<TProperty>(Expression<Func<TProperty>> propertySelector)
        {
            return GetPropertyNameImpl(propertySelector);
        }

        /// <summary>
        /// Gets the name of the property given in the lambda expression.
        /// Sample:
        /// <![CDATA[
        /// string propertyName = ExpressionUtils.GetPropertyName<Entity, int>(y => y.Property);
        /// ]]>
        /// </summary>
        /// <typeparam name="TEntity">Entity containing the property type</typeparam>
        /// <typeparam name="TProperty">Propety type</typeparam>
        /// <param name="propertySelector">Selector for the name of the property</param>
        /// <returns></returns>
        public static string GetPropertyName<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> propertySelector)
        {
            return GetPropertyNameImpl(propertySelector);
        }

        /// <summary>
        /// Gets the name of the property given in the lambda expression.
        /// Sample:
        /// <![CDATA[
        /// string propertyName = ExpressionUtils.GetPropertyName<Entity, int>(y => y.Property);
        /// ]]>
        /// </summary>
        /// <typeparam name="TEntity">Entity containing the property type</typeparam>
        /// <param name="propertySelector">Selector for the name of the property</param>
        /// <returns></returns>
        public static string GetPropertyName<TEntity>(Expression<Func<TEntity, object>> propertySelector)
        {
            return GetPropertyNameImpl(propertySelector);
        }

        private static string GetPropertyNameImpl(LambdaExpression propertySelector)
        {
            if (propertySelector == null)
            {
                throw new ArgumentNullException("propertySelector");
            }

            MemberExpression member = RemoveUnary(propertySelector.Body);
            if (member == null)
            {
                throw new InvalidOperationException("Expression is not an access expression.");
            }

            var property = member.Member as PropertyInfo;
            if (property == null)
            {
                throw new InvalidOperationException("Member in expression is not a property.");
            }

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