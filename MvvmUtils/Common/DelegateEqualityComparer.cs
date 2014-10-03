using Pellared.Common;
using System;
using System.Collections.Generic;

namespace Pellared.Common
{
    public class DelegateEqualityComparer<T> : IEqualityComparer<T>
    {
        readonly Func<T, T, bool> comparer;
        readonly Func<T, int> hash;

        public DelegateEqualityComparer(Func<T, T, bool> comparer)
            : this(comparer, t => 0) // NB Cannot assume anything about how e.g., t.GetHashCode() interacts with the comparer's behavior
        {
            Ensure.NotNull(comparer, "comparer");
        }

        public DelegateEqualityComparer(Func<T, T, bool> comparer, Func<T, int> hash)
        {
            Ensure.NotNull(comparer, "comparer");
            Ensure.NotNull(hash, "hash");

            this.comparer = comparer;
            this.hash = hash;
        }

        public bool Equals(T x, T y)
        {
            return comparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return hash(obj);
        }
    }
}