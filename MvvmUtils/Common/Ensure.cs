using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pellared.Common
{
    public static class Ensure
    {
        public static void NotEmpty(string text)
        {
            NotNull(text);
            That(text.Any(), "argument cannot be empty");
        }

        public static void NotEmpty(string text, string argumentName)
        {
            NotNull(text, argumentName);
            That(text.Any(), "argument cannot be empty", argumentName);
        }

        public static void NotEmpty(string text, string argumentName, string message)
        {
            NotNull(text, argumentName, message);
            That(text.Any(), message, argumentName);
        }

        public static void NotEmpty<TElement>(IEnumerable<TElement> enumerable)
        {
            NotNull(enumerable);
            That(enumerable.Any(), "argument cannot be empty");
        }

        public static void NotEmpty<TElement>(IEnumerable<TElement> enumerable, string argumentName)
        {
            NotNull(enumerable, argumentName);
            That(enumerable.Any(), "argument cannot be empty", argumentName);
        }

        public static void NotEmpty<TElement>(IEnumerable<TElement> enumerable, string argumentName, string message)
        {
            NotNull(enumerable, argumentName, message);
            That(enumerable.Any(), message, argumentName);
        }

        public static void NotNull<T>(T value)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
        }

        public static void NotNull<T>(T value, string argumentName)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void NotNull<T>(T value, string argumentName, string message)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName, message);
            }
        }

        public static void Range(bool condition)
        {
            if (!condition)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public static void Range(bool condition, string message)
        {
            if (!condition)
            {
                throw new ArgumentOutOfRangeException(message);
            }
        }

        public static void Range(bool condition, string message, string argumentName)
        {
            if (!condition)
            {
                throw new ArgumentOutOfRangeException(message, argumentName);
            }
        }

        public static void That(bool condition)
        {
            if (!condition)
            {
                throw new ArgumentException();
            }
        }

        public static void That(bool condition, string message)
        {
            if (!condition)
            {
                throw new ArgumentException(message);
            }
        }

        public static void That(bool condition, string message, string argumentName)
        {
            if (!condition)
            {
                throw new ArgumentException(message, argumentName);
            }
        }

        public static void That<TException>(bool condition, params object[] parameters) where TException : Exception
        {
            if (!condition)
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