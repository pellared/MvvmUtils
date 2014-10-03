using System;
using System.ComponentModel;
using System.Reflection;

namespace Pellared.Common
{
    public static class EnumUtils
    {
        public static TEnum ToEnum<TEnum>(this string text) 
            where TEnum : struct, IConvertible
        {
            Ensure.NotEmpty(text, "text");
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enum type");
            }

            return (TEnum)Enum.Parse(typeof(TEnum), text, false);
        }

        public static TEnum ToEnum<TEnum>(this string text, bool ignoreCase) 
            where TEnum : struct, IConvertible
        {
            Ensure.NotEmpty(text, "text");
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enum type");
            }

            return (TEnum)Enum.Parse(typeof(TEnum), text, ignoreCase);
        }


        public static TEnum ToEnum<TEnum>(this string text, TEnum defaultValue)
        {
            if (!Enum.IsDefined(typeof(TEnum), text))
                return defaultValue;

            return (TEnum)Enum.Parse(typeof(TEnum), text, false);
        }

        public static TEnum ToEnum<TEnum>(this string strEnumValue, TEnum defaultValue, bool ignoreCase)
        {
            if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
                return defaultValue;

            return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue, ignoreCase);
        }

        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }
    }
}