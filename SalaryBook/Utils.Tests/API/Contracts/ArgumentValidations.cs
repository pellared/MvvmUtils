using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pellared.Utils.Contracts
{
    public static partial class ArgumentValidations
    {
        private const string IsConditionDescription = @"'{0}' should satisfy {2}. Actual: {1}";

        public static IArgument<T> Is<T, TException>(this IArgument<T> argument, Func<T, bool> condition, Func<IArgument<T>, TException> exceptionDelegate)
            where TException : Exception
        {
            if (!condition(argument.Value))
            {
                throw exceptionDelegate(argument);
            }

            return argument;
        }

        public static IArgument<T> Is<T>(this IArgument<T> argument, Func<T, bool> condition, string conditionDescription = "")
        {
            return argument.Is(condition, arg => new ArgumentException(conditionDescription, arg.Name));
        }

        public static IArgument<T> Is<T>(this IArgument<T> argument, Expression<Func<T, bool>> conditionExpression)
        {
            string condition = conditionExpression.Body.ToString();
            string conditionDescription = string.Format(IsConditionDescription, argument.Name, argument.Value, condition);
            return argument.Is(conditionExpression.Compile(), arg => new ArgumentException(conditionDescription, arg.Name));
        }
    }
}