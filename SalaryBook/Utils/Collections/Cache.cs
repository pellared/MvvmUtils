using System.Collections.Generic;

namespace Pellared.Utils.Collections
{
    public class Cache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> buffer;
        private readonly RecentSet<TKey> recentSet;

        public Cache(int maxItemsCount)
        {
            buffer = new Dictionary<TKey, TValue>();
            recentSet = new RecentSet<TKey>(maxItemsCount);
            recentSet.ItemTrimed += RemoveFromBuffer;
        }

        /// <summary>
        /// Gets the item from the cache.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Throws when item is with given key is not in cache.</exception>
        public TValue GetItem(TKey id)
        {
            TValue tile = buffer[id];
            recentSet.Add(id);
            return tile;
        }

        /// <summary>
        /// Adds item to cache.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        public void Add(TKey id, TValue item)
        {
            if (buffer.ContainsKey(id))
            {
                buffer[id] = item;
            }
            else
            {
                buffer.Add(id, item);
            }

            recentSet.Add(id);
        }

        public void Remove(TKey id)
        {
            buffer.Remove(id);
            recentSet.Remove(id);
        }

        public IDictionary<TKey, TValue> ToDictionary()
        {
            return buffer;
        }

        private void RemoveFromBuffer(object sender, ItemTrimedArgs<TKey> args)
        {
            buffer.Remove(args.Item);
        }
    }
}