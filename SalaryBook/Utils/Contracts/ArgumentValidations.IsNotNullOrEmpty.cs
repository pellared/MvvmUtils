using System;
using System.Text;

namespace Pellared.Utils.Contracts
{
    public static partial class ArgumentValidations
    {
        private const string IsNotNullOrEmptyConditionDescription = "'{0}' should be not null or empty. Actual: {1}";

        private static bool IsNotNullOrEmpty(string value)
        {
            bool result = !string.IsNullOrEmpty(value);
            return result;
        }

        public static IArgument<string> IsNotNullOrEmpty<TException>(this IArgument<string> argument, Func<IArgument<string>, TException> exceptionDelegate)
            where TException : Exception
        {
            return argument.Is(IsNotNullOrEmpty, exceptionDelegate);
        }

        public static IArgument<string> IsNotNullOrEmpty(this IArgument<string> argument, string conditionDescription)
        {
            return argument.Is(IsNotNullOrEmpty, conditionDescription);
        }

        public static IArgument<string> IsNotNullOrEmpty(this IArgument<string> argument)
        {
            return argument.IsNotNullOrEmpty(string.Format(IsNotNullOrEmptyConditionDescription, argument.Name, argument.Value));
        }
    }
}