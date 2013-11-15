using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Pellared.Utils.Collections
{
    public class ItemTrimedArgs<T> : EventArgs
    {
        public ItemTrimedArgs(T item)
        {
            Item = item;
        }

        public T Item { get; private set; }
    }

    public class RecentSet<T> : IEnumerable<T>
    {
        public event EventHandler<ItemTrimedArgs<T>> ItemTrimed = delegate { };

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
            Throw.IfNull(items, "items");

            list = new List<T>(items);
        }

        public RecentSet(int maxSize, IEnumerable<T> items)
        {
            Throw.IfNull(items, "items");

            this.maxSize = maxSize;
            list = new List<T>(items);

            TrimList();
        }

        public void Add(T item)
        {
            int i = list.IndexOf(item);
            if (i > -1)
            {
                list.RemoveAt(i);
            }

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
                    ItemTrimed(this, new ItemTrimedArgs<T>(list.Last()));
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion
    }
}