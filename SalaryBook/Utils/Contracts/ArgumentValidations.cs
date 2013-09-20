using System;
using System.Collections.Generic;
using System.Linq;

namespace Pellared.Utils.Contracts
{
    public static partial class ArgumentValidations
    {
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
    }
}