using System.Text.RegularExpressions;

namespace Pellared.Common
{
    public static class Regexes
    {
        public const string NumberRegex = @"^[0-9]+$";

        public const string EmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        public const string UserNameRegex = @"^[a-z0-9_-]{3,16}$";

        public const string UrlRegex = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?}$";

        public const string IpRegex =
            @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
    }
}