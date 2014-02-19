using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.Common
{
    public class MemberEqualityComparer<TItem, TKey> : EqualityComparer<TItem>
    {
        private readonly Func<TItem, TKey> selector;
        private readonly IEqualityComparer<TKey> equalityComparer;

        public MemberEqualityComparer(Func<TItem, TKey> selector)
        {
            Ensure.NotNull(selector, "keySelector");

            this.selector = selector;
            this.equalityComparer = EqualityComparer<TKey>.Default;
        }

        public MemberEqualityComparer(Func<TItem, TKey> selector, IEqualityComparer<TKey> equalityComparer)
        {
            Ensure.NotNull(selector, "keySelector");
            Ensure.NotNull(equalityComparer, "keyEqualityComparer");

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
