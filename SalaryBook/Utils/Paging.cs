using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace Pellared.Utils
{
    public static class Paging
    {
        public static IEnumerable<IEnumerable<TSource>> Paginate<TSource>(this IEnumerable<TSource> collection, int pageSize)
        {
            Contract.Requires<ArgumentNullException>(collection != null, "collection");
            Contract.Requires<ArgumentOutOfRangeException>(pageSize > 0, "pageSize must be a positive number");

            TSource[] bucket = null;
            int count = 0;

            foreach (TSource item in collection)
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

        public static IQueryable<T> Page<T, TOrderKey>(this IQueryable<T> collection, Expression<Func<T, TOrderKey>> orderSelector,
            int pageNumber, int pageSize)
        {
            Contract.Requires<ArgumentNullException>(collection != null, "collection");
            Contract.Requires<ArgumentNullException>(orderSelector != null, "orderSelector");
            Contract.Requires<ArgumentOutOfRangeException>(pageNumber >= 0, "pageNumber must be a natural number");
            Contract.Requires<ArgumentOutOfRangeException>(pageSize > 0, "pageSize must be a positive number");

            return collection.OrderBy(orderSelector).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> Page<T, TOrderKey>(this IEnumerable<T> collection, Func<T, TOrderKey> orderSelector,
            int pageNumber, int pageSize)
        {
            Contract.Requires<ArgumentNullException>(collection != null, "collection");
            Contract.Requires<ArgumentNullException>(orderSelector != null, "orderSelector");
            Contract.Requires<ArgumentOutOfRangeException>(pageNumber >= 0, "pageNumber must be a natural number");
            Contract.Requires<ArgumentOutOfRangeException>(pageSize > 0, "pageSize must be a positive number");

            return collection.OrderBy(orderSelector).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> Page<T, TOrderKey>(this IQueryable<T> collection, int pageNumber, int pageSize)
        {
            Contract.Requires<ArgumentNullException>(collection != null, "collection");
            Contract.Requires<ArgumentOutOfRangeException>(pageNumber >= 0, "pageNumber must be a natural number");
            Contract.Requires<ArgumentOutOfRangeException>(pageSize > 0, "pageSize must be a positive number");

            return collection.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> Page<T, TOrderKey>(this IEnumerable<T> collection, int pageNumber, int pageSize)
        {
            Contract.Requires<ArgumentNullException>(collection != null, "collection");
            Contract.Requires<ArgumentOutOfRangeException>(pageNumber >= 0, "pageNumber must be a natural number");
            Contract.Requires<ArgumentOutOfRangeException>(pageSize > 0, "pageSize must be a positive number");

            return collection.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static int PageCount<T>(this IQueryable<T> collection, int pageSize)
        {
            Contract.Requires<ArgumentNullException>(collection != null, "collection");
            Contract.Requires<ArgumentOutOfRangeException>(pageSize > 0, "pageSize must be a positive number");

            return PageCount(collection.Count(), pageSize);
        }

        public static int PageCount<T>(this IEnumerable<T> collection, int pageSize)
        {
            Contract.Requires<ArgumentNullException>(collection != null, "collection");
            Contract.Requires<ArgumentOutOfRangeException>(pageSize > 0, "pageSize must be a positive number");

            return PageCount(collection.Count(), pageSize);
        }

        public static int PageCount(int elementCount, int pageSize)
        {
            Contract.Requires<ArgumentOutOfRangeException>(elementCount >= 0, "elementCount must be a natural number");
            Contract.Requires<ArgumentOutOfRangeException>(pageSize > 0, "pageSize must be a positive number");

            int pageCount = ((elementCount - 1) / pageSize) + 1;
            return pageCount;
        }

        public static int PageNumber(int elementNumber, int pageSize)
        {
            Contract.Requires<ArgumentOutOfRangeException>(elementNumber >= 0, "elementNumber must be a natural number");
            Contract.Requires<ArgumentOutOfRangeException>(pageSize > 0, "pageSize must be a positive number");

            return elementNumber / pageSize;
        }
    }
}