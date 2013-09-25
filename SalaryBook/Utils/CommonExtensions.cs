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