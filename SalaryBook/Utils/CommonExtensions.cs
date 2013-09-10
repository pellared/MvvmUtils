using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pellared.Utils
{
    public static class CommonExtensions
    {
        public static bool EqualsDefault<T>(this T argument)
        {
            return Equals(argument, default(T));
        }
    }
}