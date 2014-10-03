using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Pellared.Common
{
    public static class CommonExtensions
    {
        public static bool EqualsDefault<T>(this T instance)
        {
            return Equals(instance, default(T));
        }

        public static void SafeDispose(this IDisposable disposable)
        {
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public static string ToInvariantString(this object instance)
        {
            var formattable = instance as IFormattable;
            if (formattable != null)
            {
                return formattable.ToString(null, CultureInfo.InvariantCulture);
            }

            return instance.ToString();
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}