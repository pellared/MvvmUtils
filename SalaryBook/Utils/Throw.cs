using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pellared.Utils
{
    public static class Throw
    {
        [DebuggerStepThrough]
        public static void IfNullOrEmpty<TException>(string @string, params object[] parameters) where TException : Exception
        {
            If<TException>(string.IsNullOrEmpty(@string), parameters);
        }

        [DebuggerStepThrough]
        public static void IfNullOrEmpty<TExpcetion, TElement>(IEnumerable<TElement> enumerable, params object[] parameters) where TExpcetion : Exception
        {
            If<TExpcetion>(enumerable == null || !enumerable.Any(), parameters);
        }

        [DebuggerStepThrough]
        public static void IfNullOrEmpty(string @string, string argumentName)
        {
            IfNullOrEmpty(@string, argumentName,
                string.Format("Argument '{0}' cannot be null or empty.", argumentName));
        }

        [DebuggerStepThrough]
        public static void IfNullOrEmpty(string @string, string argumentName, string message)
        {
            IfNullOrEmpty<ArgumentException>(@string, message, argumentName);
        }

        [DebuggerStepThrough]
        public static void IfNullOrEmpty<TElement>(IEnumerable<TElement> enumerable, string argumentName)
        {
            IfNullOrEmpty(enumerable, argumentName,
                string.Format("Argument '{0}' cannot be null or empty.", argumentName));
        }

        [DebuggerStepThrough]
        public static void IfNullOrEmpty<TElement>(IEnumerable<TElement> enumerable, string argumentName, string message)
        {
            IfNullOrEmpty<ArgumentException, TElement>(enumerable, message, argumentName);
        }

        [DebuggerStepThrough]
        public static void IfNull(object @object, string argumentName)
        {
            IfNull<ArgumentNullException>(@object, argumentName);
        }

        [DebuggerStepThrough]
        public static void IfNull<TException>(object @object, params object[] parameters) where TException : Exception
        {
            If<TException>(@object == null, parameters);
        }

        [DebuggerStepThrough]
        public static void If(bool condition, string argumentName, string message)
        {
            If<ArgumentException>(condition, message, argumentName);
        }

        [DebuggerStepThrough]
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