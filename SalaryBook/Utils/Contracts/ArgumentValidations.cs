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
            conditionDescription = argument.BuildConditionDescription(conditionDescription);
            return argument.Is(condition, arg => new ArgumentException(conditionDescription, arg.Name));
        }

        /// <summary>
        /// Returns formated condition description.
        /// </summary>
        /// <param name="argument">Object with extension method.</param>
        /// <param name="format">{0} for argument's name, {1} for argument's value.</param>
        /// <returns>Formated condition description</returns>
        public static string BuildConditionDescription<T>(this IArgument<T> argument, string format)
        {
            return string.Format(format, argument.Name, argument.Value);
        }
    }
}