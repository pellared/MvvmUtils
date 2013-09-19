using System;
using System.Linq.Expressions;

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

            Value = argumentExpression.Compile()();
            Name = ExpressionUtils.GetParameterName(argumentExpression);
        }

        public T Value { get; private set; }
        public string Name { get; private set; }
    }
}