using System;
using System.Text;

namespace Pellared.Common.Conditions
{
    public static partial class ArgumentValidations
    {
        private const string IsNotNullOrEmptyConditionDescription = "'{0}' should be not null or empty. Actual: {1}";

        private static bool IsNotNullOrEmpty(string value)
        {
            bool result = !string.IsNullOrEmpty(value);
            return result;
        }

        public static Argument<string> IsNotNullOrEmpty(this Argument<string> argument, string conditionDescription)
        {
            return argument.Is(IsNotNullOrEmpty, conditionDescription);
        }

        public static Argument<string> IsNotNullOrEmpty(this Argument<string> argument)
        {
            return argument.IsNotNullOrEmpty(string.Format(IsNotNullOrEmptyConditionDescription, argument.Name, argument.Value));
        }
    }
}