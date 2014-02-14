using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pellared.Common.Conditions
{
    public static class Throw
    {
        public static void IfNullOrEmpty(string text, string argumentName, string message = "")
        {
            IfNull(text, argumentName, message);
            If(text.Empty(), argumentName, message);
        }

        public static void IfNullOrEmpty<TElement>(IEnumerable<TElement> enumerable, string argumentName, string message = "")
        {
            IfNull(enumerable, argumentName, message);
            If(enumerable.Empty(), argumentName, message);
        }

        public static void IfNull<T>(T value, string argumentName, string message = "")
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName, message);
            }
        }

        public static void IfNot(bool condition, string argumentName, string message = "")
        {
            If(!condition, argumentName, message);
        }

        public static void IfNot<TException>(bool condition, params object[] parameters) where TException : Exception
        {
            If<TException>(!condition, parameters);
        }

        public static void If(bool condition, string argumentName, string message)
        {
            if (condition)
            {
                throw new ArgumentException(message, argumentName);
            }
        }

        public static void If<TException>(bool condition, params object[] parameters) where TException : Exception
        {
            if (condition)
            {
                var types = new List<Type>();
                var args = new List<object>();
                foreach (object p in parameters ?? Enumerable.Empty<object>())
                {
                    types.Add(p.GetType());
                    args.Add(p);
                }

                var constructor = typeof(TException).GetConstructor(types.ToArray());
                var exception = constructor.Invoke(args.ToArray()) as TException;
                throw exception;
            }
        }
    }
}