using System;
using System.Linq.Expressions;

namespace Pellared.Utils.Contracts
{
    public static class Check
    {
         public static Argument<T> If<T>(Expression<Func<T>> argumentExpression)
         {
             return new Argument<T>(argumentExpression);
         }
    }
}