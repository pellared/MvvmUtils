using System;
using System.Linq.Expressions;

namespace Pellared.Common.Conditions
{
    public static class Require
    {
        public static Argument<T> That<T>(T argument, string name = "")
        {
            return new Argument<T>(argument, name);
        }

        public static Argument<T> That<T>(Expression<Func<T>> argumentExpression)
        {
            T value = ExpressionUtils.GetValue(argumentExpression);
            string name = ExpressionUtils.GetName(argumentExpression);
            return new Argument<T>(value, name);
        }
    }
}