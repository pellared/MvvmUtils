using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.Common
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty(this IEnumerable collection)
        {
            return collection == null || !collection.Cast<object>().Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static bool IsEmpty(this IEnumerable collection)
        {

            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            return IsEmpty(collection.Cast<object>());
        }

        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            return !collection.Any();
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, T element)
        {
            return source.Except(new[] { element });
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            Ensure.NotNull(source, "source");
            Ensure.NotNull(predicate, "predicate");

            return source.Where(item => !predicate(item));
        }

        public static IEnumerable<TItem> ExceptNull<TItem, TValue>(this IEnumerable<TItem> source, Func<TItem, TValue> selector)
            where TValue : class
        {
            Ensure.NotNull(source, "source");
            Ensure.NotNull(selector, "selector");

            return source.Where(item => selector(item) != null);
        }

        public static IEnumerable<T> ExceptNull<T>(this IEnumerable<T> source)
            where T : class
        {
            Ensure.NotNull(source, "source");

            return source.Where(item => item != null);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Ensure.NotNull(action, "action");

            foreach (T elem in source)
            {
                action(elem);
            }
        }

        public static bool AnyTrue(this IEnumerable<bool> bools)
        {
            Ensure.NotNull(bools, "bools");

            bool result = bools.Any(x => x);
            return result;
        }

        public static T? FirstOrNull<T>(this IEnumerable<T> collection)
            where T : struct
        {
            Ensure.NotNull(collection, "collection");

            try
            {
                return collection.First();
            }
            catch (InvalidOperationException)
            {
                // The source sequence is empty
                return null;
            }
        }

        public static T? FirstOrNull<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
            where T : struct
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(predicate, "predicate");

            try
            {
                return collection.First(predicate);
            }
            catch (InvalidOperationException)
            {
                // The source sequence is empty
                return null;
            }
        }
    }
}
