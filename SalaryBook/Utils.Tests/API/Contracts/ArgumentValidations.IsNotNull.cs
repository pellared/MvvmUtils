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

        public static Argument<T> IsNotNull<T, TException>(this Argument<T> argument, Func<Argument<T>, TException> exceptionDelegate)
            where T : class
            where TException : Exception
        {
            return argument.Is(IsNotNull, exceptionDelegate);
        }

        public static Argument<T> IsNotNull<T>(this Argument<T> argument, string conditionDescription)
            where T : class
        {
            return argument.IsNotNull(arg => new ArgumentNullException(arg.Name, conditionDescription));
        }

        public static Argument<T> IsNotNull<T>(this Argument<T> argument)
            where T : class
        {
            return argument.IsNotNull(string.Format(IsNotNullConditionDescription, argument.Name, argument.Value));
        }
    }
}