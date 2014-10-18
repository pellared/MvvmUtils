using Pellared.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pellared.MvvmUtils.Validation
{
    public enum ArrayFormat
    {
        First,
        Multiline
    }

    // TODO: object to ValidationError
    public static class ArrayFormatter
    {
        public static Func<IEnumerable<object>, string> GetErrorFormatter(ArrayFormat arrayFormat)
        {
            switch (arrayFormat)
            {
                case ArrayFormat.First:
                    return GetFirstString;

                case ArrayFormat.Multiline:
                    return GetMultilineString;

                default:
                    throw new ArgumentOutOfRangeException("arrayFormat");
            }
        }

        public static string GetFirstString(IEnumerable<object> errors)
        {
            object error = errors.FirstOrDefault();
            return error.EqualsDefault() ? null : error.ToString();
        }

        public static string GetMultilineString(IEnumerable<object> errors)
        {
            return errors.Any() ? string.Join(Environment.NewLine, errors.Select(x => x.ToString())) : null;
        }
    }
}