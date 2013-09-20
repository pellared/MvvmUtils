using System;
using System.Text;

namespace Pellared.Utils.Contracts
{
    public static partial class ArgumentValidations
    {
        private static bool IsOfType<T>(T value, Type type)
        {
            bool result = value != null && type.IsAssignableFrom(value.GetType());
            return result;
        }

        public static IArgument<T> IsOfType<T, TException>(this IArgument<T> argument, Type type, Func<IArgument<T>, TException> exceptionDelegate)
            where TException : Exception
        {
            return argument.Is(value => IsOfType(value, type), exceptionDelegate);
        }

        public static IArgument<T> IsOfType<T>(this IArgument<T> argument, Type type, string conditionDescription)
        {
            return argument.Is(value => IsOfType(value, type), conditionDescription);
        }

        public static IArgument<T> IsOfType<T>(this IArgument<T> argument, Type type)
        {
            return argument.IsOfType(type, "'{0}' should be of type " + type.FullName + ". Actual: " + argument.Value.GetType().FullName);
        }
    }
}