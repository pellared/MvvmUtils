using System;
using System.Text.RegularExpressions;

namespace Pellared.Utils
{
    public static class Regexes
    {
        public const string NumberRegex = @"^\d+$";
        public static Regex Number { get { return new Regex(NumberRegex); } }

        public const string EmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        public static Regex Email { get { return new Regex(EmailRegex); } }
    }
}