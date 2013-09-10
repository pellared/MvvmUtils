using System.Collections.Generic;
using System.Linq;

namespace Pellared.Utils.Collections
{
    public class RecentSet<T> : IEnumerable<T>
    {
        public delegate void ItemTrimedHandler(T item);

        public event ItemTrimedHandler ItemTrimed = delegate { };

        private readonly List<T> list;
        private readonly int maxSize = -1;

        public RecentSet()
        {
            list = new List<T>();
        }

        public RecentSet(int maxSize)
        {
            list = new List<T>();
            this.maxSize = maxSize;
        }

        public RecentSet(IEnumerable<T> items)
        {
            list = new List<T>(items);
        }

        public RecentSet(int maxSize, IEnumerable<T> items)
        {
            list = new List<T>(items);
            this.maxSize = maxSize;

            TrimList();
        }

        public int Count
        {
            get { return list.Count; }
        }

        public void Add(T item)
        {
            int i = list.IndexOf(item);
            if (i > -1)
                list.RemoveAt(i);

            list.Insert(0, item);

            TrimList();
        }

        public void Remove(T item)
        {
            list.Remove(item);
        }

        private void TrimList()
        {
            if (maxSize != -1)
            {
                while (list.Count > maxSize)
                {
                    ItemTrimed(list.ElementAt(list.Count - 1));
                    list.RemoveAt(list.Count - 1);
                }
            }
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion
    }
}