using System;

namespace Pellared.Common.Conditions
{
    public static partial class ArgumentValidations
    {
        private static bool IsNotNull<T>(T value)
            where T : class
        {
            bool result = value != null;
            return result;
        }

        public static Argument<T> IsNotNull<T>(this Argument<T> argument, string conditionDescription = "")
            where T : class
        {
            Throw.IfNull(argument.Value, argument.Name);
            return argument;
        }
    }
}