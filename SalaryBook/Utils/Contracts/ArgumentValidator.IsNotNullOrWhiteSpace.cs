using System;
using System.Text;

namespace Pellared.Utils.Contracts
{
    public static partial class ArgumentValidatorExtensions
    {
        private static readonly string IsNotNullOrWhiteSpaceConditionDescription = "'{0}' should be not null or whitespace. Actual: {1}";

        private static bool IsNotNullOrWhiteSpace(string value)
        {
            bool result = !string.IsNullOrWhiteSpace(value);
            return result;
        }

        public static ArgumentValidator<string> IsNotNullOrWhiteSpace<TException>(this ArgumentValidator<string> validation, Func<Argument<string>, TException> exceptionDelegate)
            where TException : ArgumentException
        {
            return validation.Is(IsNotNullOrWhiteSpace, exceptionDelegate);
        }

        public static ArgumentValidator<string> IsNotNullOrWhiteSpace(this ArgumentValidator<string> validation, string conditionDescription)
        {
            return validation.Is(IsNotNullOrWhiteSpace, conditionDescription);
        }

        public static ArgumentValidator<string> IsNotNullOrWhiteSpace(this ArgumentValidator<string> validation)
        {
            string conditionDescription = validation.BuildConditionDescription(IsNotNullOrWhiteSpaceConditionDescription);
            return validation.IsNotNullOrWhiteSpace(conditionDescription);
        }
    }
}