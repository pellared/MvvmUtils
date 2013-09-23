using Pellared.Utils;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MvvmLightSample.ViewModel
{
    public class PropertyError
    {
        public string PropertyName { get; set; }

        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return ErrorMessage;
        }

        public static IEnumerable<PropertyError> Create(Expression<Func<object>> propertySelector, string errorMessage)
        {
            return new[] { new PropertyError() { PropertyName = ExpressionUtils.ExtractPropertyName(propertySelector), ErrorMessage = errorMessage } };
        }
    }
}