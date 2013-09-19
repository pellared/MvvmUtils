using CuttingEdge.Conditions;
using System;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace Pellared.Utils.Tests
{
    public static class Require
    {
        public static ConditionValidator<T> That<T>(Expression<Func<T>> argumentExpression)
        {
            Contract.Requires(argumentExpression != null);

            T value = argumentExpression.Compile()();
            string argumentName = ExpressionUtils.GetParameterName(argumentExpression);
            return Condition.Requires(value, argumentName);
        }
    }

    public static class Require<TException>
        where TException : Exception
    {
        public static ConditionValidator<TEntity> That<TEntity>(Expression<Func<TEntity>> argumentExpression)
        {
            Contract.Requires(argumentExpression != null);

            TEntity value = argumentExpression.Compile()();
            string argumentName = ExpressionUtils.GetParameterName(argumentExpression);
            return Condition.WithExceptionOnFailure<TException>().Requires(value, argumentName);
        }
    }
}