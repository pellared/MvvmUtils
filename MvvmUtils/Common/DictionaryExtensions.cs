using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.Common
{
    public static class DictionaryExtenstions
    {
        public static TValue GetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
                where TValue : class
        {
            Ensure.NotNull(dictionary, "dictionary");

            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : null;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            Ensure.NotNull(dictionary, "dictionary");

            TValue result;
            return dictionary.TryGetValue(key, out result)? result : default(TValue);
        }

        public static TValue? GetValueOrNullable<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : struct
        {
            Ensure.NotNull(dictionary, "dictionary");

            TValue value;
            return dictionary.TryGetValue(key, out value) ? (TValue?)value : null;
        }
    }
}
