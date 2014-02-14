using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pellared.Common.Conditions
{
    public static partial class ArgumentValidations
    {
        public static Argument<T> Is<T>(this Argument<T> argument, Func<T, bool> condition, string conditionDescription = "")
        {
            Throw.IfNot(condition(argument.Value), conditionDescription);

            return argument;
        }

        // very slow...
        public static Argument<T> Is<T>(this Argument<T> argument, Expression<Func<T, bool>> conditionExpression)
        {
            string condition = conditionExpression.Body.ToString();
            string conditionDescription = "'" + argument.Name + "' should satisfy: " + condition + ". Acutal: " + argument.Value;
            Throw.IfNot(conditionExpression.Compile().Invoke(argument.Value), argument.Name, conditionDescription);
            return argument;
        }
    }
}