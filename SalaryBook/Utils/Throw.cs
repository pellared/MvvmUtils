using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pellared.Utils
{
    public static class Throw
    {
        [DebuggerStepThrough]
        public static void IfNullOrEmpty(string texr, string argumentName)
        {
            IfNullOrEmpty(texr, argumentName,
                string.Format("Argument '{0}' cannot be null or empty.", argumentName));
        }

        [DebuggerStepThrough]
        public static void IfNullOrEmpty(string text, string argumentName, string message)
        {
            IfNullOrEmpty<ArgumentException>(text, message, argumentName);
        }


        [DebuggerStepThrough]
        public static void IfNullOrEmpty<TException>(string text, params object[] parameters) where TException : Exception
        {
            If<TException>(string.IsNullOrEmpty(text), parameters);
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
        public static void IfNullOrEmpty<TExpcetion, TElement>(IEnumerable<TElement> enumerable, params object[] parameters) where TExpcetion : Exception
        {
            If<TExpcetion>(enumerable == null || !enumerable.Any(), parameters);
        }

        [DebuggerStepThrough]
        public static void IfNull(object value, string argumentName) 
        {
            IfNull<ArgumentNullException>(value, argumentName);
        }

        [DebuggerStepThrough]
        public static void IfNull<TException>(object value, params object[] parameters) where TException : Exception
        {
            If<TException>(value == null, parameters);
        }

        [DebuggerStepThrough]
        public static void IfNot(bool condition, string argumentName, string message)
        {
            If(!condition, argumentName, message);
        }

        [DebuggerStepThrough]
        public static void IfNot<TException>(bool condition, params object[] parameters) where TException : Exception
        {
            If<TException>(!condition, parameters);
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