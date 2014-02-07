using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.Utils.Contracts.Tests
{
    public static class ExpressionUtilss
    {
        public static T GetValue<T>(Expression<Func<T>> argument)
        {
            var memberExpression = (MemberExpression)argument.Body;
            object value;
            if (memberExpression.Expression.NodeType == ExpressionType.Constant)
            {
                var constantExpression = (ConstantExpression)memberExpression.Expression;
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
    }
}
