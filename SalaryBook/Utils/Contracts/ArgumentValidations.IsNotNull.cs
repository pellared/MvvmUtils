using System;

namespace Pellared.Utils.Contracts
{
    public static partial class ArgumentValidations
    {
        private const string IsNotNullConditionDescription = "'{0}' should be not null. Actual: {1}";

        private static bool IsNotNull<T>(T value)
            where T : class
        {
            bool result = value != null;
            return result;
        }

        public static Argument<T> IsNotNull<T, TException>(this Argument<T> validator, Func<Argument<T>, TException> exceptionDelegate)
            where T : class
            where TException : Exception
        {
            return validator.Is(IsNotNull, exceptionDelegate);
        }
         
        public static Argument<T> IsNotNull<T>(this Argument<T> validator, string conditionDescription)
            where T : class
        {
            conditionDescription = validator.BuildConditionDescription(conditionDescription);
            return validator.IsNotNull(arg => new ArgumentNullException(arg.Name, conditionDescription));
        }

        public static Argument<T> IsNotNull<T>(this Argument<T> validator)
            where T : class
        {
            return validator.IsNotNull(IsNotNullConditionDescription);
        }
    }
}