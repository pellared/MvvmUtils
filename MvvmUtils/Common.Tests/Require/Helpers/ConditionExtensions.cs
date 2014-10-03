using CuttingEdge.Conditions;
using System;
using System.Linq.Expressions;

namespace Pellared.Common.Tests
{
    public static class ConditionExtensions
    {
        public static ConditionValidator<T> Validate<T>(this ConditionValidator<T> validator, Func<T, bool> condition, string conditionDescription = "")
        {
            return validator.Evaluate(condition(validator.Value), conditionDescription);
        }
    }
}