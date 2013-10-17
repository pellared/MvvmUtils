using System;
using System.Globalization;

namespace Pellared.Utils
{
    public static class CommonExtensions
    {
        public static bool EqualsDefault<T>(this T @this)
        {
            return Equals(@this, default(T));
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
    }
}