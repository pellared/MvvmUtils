using System;
using System.Linq.Expressions;

namespace Pellared.Utils.Contracts
{
    public static class Check
    {
         public static IArgument<T> If<T>(Expression<Func<T>> argumentExpression)
         {
             return new Argument<T>(argumentExpression);
         }
    }
}