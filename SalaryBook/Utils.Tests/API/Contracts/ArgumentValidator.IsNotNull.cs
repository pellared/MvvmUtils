using System;

namespace Pellared.Utils.Contracts
{
    public static partial class ArgumentValidatorExtensions
    {
        private static readonly string IsNotNullConditionDescription = "'{0}' should be not null. Actual: {1}";

        private static bool IsNotNull<T>(T value)
            where T : class
        {
            bool result = value != null;
            return result;
        }

        public static ArgumentValidator<T> IsNotNull<T, TException>(this ArgumentValidator<T> validator, Func<Argument<T>, TException> exceptionDelegate)
            where T : class
            where TException : Exception
        {
            return validator.Is(IsNotNull, exceptionDelegate);
        }
         
        public static ArgumentValidator<T> IsNotNull<T>(this ArgumentValidator<T> validator, string conditionDescription)
            where T : class
        {
            conditionDescription = validator.BuildConditionDescription(conditionDescription);
            return validator.IsNotNull(arg => new ArgumentNullException(arg.Name, conditionDescription));
        }

        public static ArgumentValidator<T> IsNotNull<T>(this ArgumentValidator<T> validator)
            where T : class
        {
            return validator.IsNotNull(IsNotNullConditionDescription);
        }
    }
}