using System.Collections.Generic;

namespace Pellared.Common.Collections
{
    public interface ICache<TKey, TValue>
    {
        void Add(TKey id, TValue item);

        TValue Get(TKey id);

        void Remove(TKey id);
    }

    public class Cache<TKey, TValue> : ICache<TKey, TValue>
    {
        public Cache(IDictionary<TKey, TValue> buffer, IRecentSet<TKey> recentSet)
        {
            Buffer = buffer;
            RecentSet = recentSet;
            RecentSet.ItemTrimed += RemoveFromBuffer;
        }

        public Cache(int maxItemsCount)
            : this(new Dictionary<TKey, TValue>(maxItemsCount), new RecentSet<TKey>(maxItemsCount))
        {
        }

        public IDictionary<TKey, TValue> Buffer { get; private set; }

        public IRecentSet<TKey> RecentSet { get; private set; }

        /// <summary>
        ///     Gets the item from the cache.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Throws when item is with given key is not in cache.</exception>
        public TValue Get(TKey id)
        {
            TValue result = Buffer[id];
            RecentSet.Add(id);
            return result;
        }

        /// <summary>
        ///     Adds item to cache.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        public void Add(TKey id, TValue item)
        {
            if (Buffer.ContainsKey(id))
            {
                Buffer[id] = item;
            }
            else
            {
                Buffer.Add(id, item);
            }

            RecentSet.Add(id);
        }

        public void Remove(TKey id)
        {
            Buffer.Remove(id);
            RecentSet.Remove(id);
        }

        private void RemoveFromBuffer(object sender, ItemTrimedArgs<TKey> args)
        {
            Buffer.Remove(args.Item);
        }
    }
}