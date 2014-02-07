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

        public static Argument<string> IsNotNullOrWhiteSpace<TException>(this Argument<string> argument, Func<Argument<string>, TException> exceptionDelegate)
            where TException : Exception
        {
            return argument.Is(IsNotNullOrWhiteSpace, exceptionDelegate);
        }

        public static Argument<string> IsNotNullOrWhiteSpace(this Argument<string> argument, string conditionDescription)
        {
            return argument.Is(IsNotNullOrWhiteSpace, conditionDescription);
        }

        public static Argument<string> IsNotNullOrWhiteSpace(this Argument<string> argument)
        {
            return argument.IsNotNullOrWhiteSpace(string.Format(IsNotNullOrWhiteSpaceConditionDescription, argument.Name, argument.Value));
        }
    }
}