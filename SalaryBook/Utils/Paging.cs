using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pellared.Utils
{
    public static class Paging
    {
        public static IQueryable<T> Page<T, TOrderKey>(this IQueryable<T> collection, Expression<Func<T, TOrderKey>> orderSelector, int pageNumber, int pageSize)
        {
            return collection.OrderBy(orderSelector).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> Page<T, TOrderKey>(this IEnumerable<T> collection, Func<T, TOrderKey> orderSelector, int pageNumber, int pageSize)
        {
            return collection.OrderBy(orderSelector).Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> Page<T, TOrderKey>(this IQueryable<T> collection, int pageNumber, int pageSize)
        {
            return collection.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> Page<T, TOrderKey>(this IEnumerable<T> collection, int pageNumber, int pageSize)
        {
            return collection.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static int PageCount<T>(this IQueryable<T> collection, int pageSize)
        {
            return PageCount(collection.Count(), pageSize);
        }

        public static int PageCount<T>(this IEnumerable<T> collection, int pageSize)
        {
            return PageCount(collection.Count(), pageSize);
        }

        public static int PageCount(int recordCount, int pageSize)
        {
            int pageCount = ((recordCount - 1) / pageSize) + 1;
            return pageCount;
        }
    }
}