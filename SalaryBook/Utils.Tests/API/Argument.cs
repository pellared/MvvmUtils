using Pellared.Utils.Contracts.Tests;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Pellared.Utils
{
    public class Argument<T>
    {
        public Argument(T value, string name)
        {
            Value = value;
            Name = name;
        }

        public Argument(Expression<Func<T>> argumentExpression)
        {
            if (argumentExpression == null)
                throw new ArgumentNullException("argumentExpression");

            Value = ExpressionUtilss.GetValue(argumentExpression);
            Name = ExpressionUtils.GetName(argumentExpression);
        }

        public T Value { get; private set; }
        public string Name { get; private set; }

        public static implicit operator T(Argument<T> arg)
        {
            return arg.Value;
        }
    }
}