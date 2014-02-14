using CuttingEdge.Conditions;
using System;
using System.Linq.Expressions;

namespace Pellared.Common.Tests
{
    public static class ConditionEx
    {
        public static ConditionValidator<T> Requires<T>(Expression<Func<T>> argumentExpression)
        {
            if (argumentExpression == null)
                throw new ArgumentNullException("argumentExpression");

            T value = ExpressionUtils.GetValue(argumentExpression);
            string argumentName = ExpressionUtils.GetName(argumentExpression);
            return Condition.Requires(value, argumentName);
        }

        public static ConditionValidator<T> Ensures<T>(Expression<Func<T>> argumentExpression)
        {
            if (argumentExpression == null)
                throw new ArgumentNullException("argumentExpression");

            T value = ExpressionUtils.GetValue(argumentExpression);
            string argumentName = ExpressionUtils.GetName(argumentExpression);
            return Condition.Ensures(value, argumentName);
        }

        public static ConditionValidator<T> Is<T>(this ConditionValidator<T> validator, bool condition, string conditionDescription = "")
        {
            return validator.Evaluate(condition, conditionDescription);
        }

        public static ConditionValidator<T> Is<T>(this ConditionValidator<T> validator, Func<T, bool> condition, string conditionDescription = "")
        {
            return validator.Evaluate(condition(validator.Value), conditionDescription);
        }
    }

    public static class Condition<TException>
        where TException : Exception
    {
        public static ConditionValidator<TEntity> Requires<TEntity>(Expression<Func<TEntity>> argumentExpression)
        {
            if (argumentExpression == null)
                throw new ArgumentNullException("argumentExpression");

            TEntity value = ExpressionUtils.GetValue(argumentExpression);
            string argumentName = ExpressionUtils.GetName(argumentExpression);
            return Condition.WithExceptionOnFailure<TException>().Requires(value, argumentName);
        }
    }
}