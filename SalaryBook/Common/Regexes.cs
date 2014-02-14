using System.Text.RegularExpressions;

namespace Pellared.Common
{
    public static class Regexes
    {
        public const string NumberRegex = @"^[0-9]+$";

        public static Regex Number
        {
            get { return new Regex(NumberRegex); }
        }

        public const string EmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        public static Regex Email
        {
            get { return new Regex(EmailRegex); }
        }

        public const string UserNameRegex = @"^[a-z0-9_-]{3,16}$";

        public static Regex UserName
        {
            get { return new Regex(UserNameRegex); }
        }

        public const string UrlRegex = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?}$";

        public static Regex Url
        {
            get { return new Regex(UrlRegex); }
        }

        public const string IpRegex =
            @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        public static Regex Ip
        {
            get { return new Regex(IpRegex); }
        }
    }
}