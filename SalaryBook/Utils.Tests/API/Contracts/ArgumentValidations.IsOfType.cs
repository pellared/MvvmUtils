﻿using System;
using System.Text;

namespace Pellared.Utils.Contracts
{
    public static partial class ArgumentValidations
    {
        private const string IsOfTypeConditionDescription = "'{0}' should be of type {2}. Actual: {1}";

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
            return argument.IsOfType(type, string.Format(IsOfTypeConditionDescription, argument.Name, argument.Value.GetType().FullName, type.FullName));
        }
    }
}