using Pellared.Common.Conditions;
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
        }

        public DelegateEqualityComparer(Func<T, T, bool> comparer, Func<T, int> hash)
        {
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

    public class DelegateEqualityComparer<TItem, TKey> : EqualityComparer<TItem>
    {
        private readonly Func<TItem, TKey> selector;
        private readonly IEqualityComparer<TKey> equalityComparer;

        public DelegateEqualityComparer(Func<TItem, TKey> selector)
        {
            Throw.IfNull(selector, "keySelector");

            this.selector = selector;
            this.equalityComparer = EqualityComparer<TKey>.Default;
        }

        public DelegateEqualityComparer(Func<TItem, TKey> selector, IEqualityComparer<TKey> equalityComparer)
        {
            Throw.IfNull(selector, "keySelector");
            Throw.IfNull(equalityComparer, "keyEqualityComparer");

            this.selector = selector;
            this.equalityComparer = equalityComparer;
        }

        public override bool Equals(TItem x, TItem y)
        {
            return equalityComparer.Equals(selector(x), selector(y));
        }

        public override int GetHashCode(TItem obj)
        {
            return equalityComparer.GetHashCode(selector(obj));
        }
    }
}