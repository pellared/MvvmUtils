using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pellared.Utils
{
    public static class Paging
    {
        public static IEnumerable<IEnumerable<TSource>> Paginate<TSource>(this IEnumerable<TSource> source, int pageSize)
        {
            if (source == null) throw new ArgumentNullException("source");

            TSource[] bucket = null;
            var count = 0;

            foreach (var item in source)
            {
                if (bucket == null)
                {
                    bucket = new TSource[pageSize];
                }

                bucket[count++] = item;

                // The bucket is fully buffered before it's yielded
                if (count != pageSize)
                {
                    continue;
                }

                // Select is necessary so bucket contents are streamed too
                yield return bucket;

                bucket = null;
                count = 0;
            }

            // Return the last bucket with all remaining elements
            if (bucket != null && count > 0)
            {
                yield return bucket;
            }
        }

        public static IQueryable<T> Page<T, TOrderKey>(this IQueryable<T> collection, Expression<Func<T, TOrderKey>> orderSelector, int pageNumber, int pageSize)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (orderSelector == null) throw new ArgumentNullException("orderSelector");

            return collection.OrderBy(orderSelector).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> Page<T, TOrderKey>(this IEnumerable<T> collection, Func<T, TOrderKey> orderSelector, int pageNumber, int pageSize)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (orderSelector == null) throw new ArgumentNullException("orderSelector");

            return collection.OrderBy(orderSelector).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> Page<T, TOrderKey>(this IQueryable<T> collection, int pageNumber, int pageSize)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            return collection.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> Page<T, TOrderKey>(this IEnumerable<T> collection, int pageNumber, int pageSize)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            return collection.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static int PageCount<T>(this IQueryable<T> collection, int pageSize)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            return PageCount(collection.Count(), pageSize);
        }

        public static int PageCount<T>(this IEnumerable<T> collection, int pageSize)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            return PageCount(collection.Count(), pageSize);
        }

        public static int PageCount(int recordCount, int pageSize)
        {
            int pageCount = ((recordCount - 1) / pageSize) + 1;
            return pageCount;
        }

        public static int PageNumber(int elementNumber, int pageSize)
        {
            return elementNumber / pageSize;
        }
    }
}