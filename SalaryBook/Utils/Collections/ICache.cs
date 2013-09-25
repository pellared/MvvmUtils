namespace Pellared.Utils.Collections
{
    public interface ICache<TKey, TValue>
    {
        void Add(TKey id, TValue item);

        TValue Get(TKey id);

        void Remove(TKey id);
    }
}