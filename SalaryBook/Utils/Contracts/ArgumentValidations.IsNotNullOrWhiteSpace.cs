using System;
using System.Text;

namespace Pellared.Utils.Contracts
{
    public static partial class ArgumentValidations
    {
        private const string IsNotNullOrWhiteSpaceConditionDescription = "'{0}' should be not null or whitespace. Actual: {1}";

        private static bool IsNotNullOrWhiteSpace(string value)
        {
            bool result = !string.IsNullOrWhiteSpace(value);
            return result;
        }

        public static IArgument<string> IsNotNullOrWhiteSpace<TException>(this IArgument<string> argument, Func<IArgument<string>, TException> exceptionDelegate)
            where TException : Exception
        {
            return argument.Is(IsNotNullOrWhiteSpace, exceptionDelegate);
        }

        public static IArgument<string> IsNotNullOrWhiteSpace(this IArgument<string> argument, string conditionDescription)
        {
            return argument.Is(IsNotNullOrWhiteSpace, conditionDescription);
        }

        public static IArgument<string> IsNotNullOrWhiteSpace(this IArgument<string> argument)
        {
            return argument.IsNotNullOrWhiteSpace(IsNotNullOrWhiteSpaceConditionDescription);
        }
    }
}