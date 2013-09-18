using System;
using System.Linq.Expressions;

namespace Pellared.Utils.Contracts
{
    public static class Require
    {
         public static ArgumentValidator<T> Argument<T>(Expression<Func<T>> argumentExpression)
         {
             if (argumentExpression == null)
                 throw new ArgumentNullException("argumentExpression");

             T value = argumentExpression.Compile()();
             string name = GetArgumentName(argumentExpression);
             Argument<T> argument = new Argument<T>(value, name);
             return new ArgumentValidator<T>(argument);
         }

         private static string GetArgumentName<T>(Expression<Func<T>> parameterExpression)
         {
             dynamic body = parameterExpression.Body;
             return body.Member.Name;
         }
    }
}