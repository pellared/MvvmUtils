using System;
using System.Text.RegularExpressions;

namespace Pellared.Utils
{
    public static class Regexes
    {
        public static Regex Number { get { return new Regex(@"^\d+$"); } }

        public static Regex Email { get { return new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"); } }
    }
}