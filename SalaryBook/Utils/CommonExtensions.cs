using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Pellared.Utils
{
    public static class CommonExtensions
    {
        public static bool EqualsDefault<T>(this T @this)
        {
            return Equals(@this, default(T));
        }

        public static void SafeDispose(this IDisposable disposable)
        {
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public static string ToInvariantString(this object @this)
        {
            var formattable = @this as IFormattable;
            if (formattable != null)
            {
                return formattable.ToString(null, CultureInfo.InvariantCulture);
            }
            return @this.ToString();
        }

        public static bool IsNullOrEmpty(this IEnumerable collection)
        {
            return collection == null || !collection.Cast<object>().Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Empty();
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool Empty(this IEnumerable collection)
        {
            
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            return Empty(collection.Cast<object>());
        }

        public static bool Empty<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            return !collection.Any();
        }
    }
}