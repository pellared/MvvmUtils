using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Pellared.Utils.Mvvm.Validation
{
    public class DelegateValidator<TObject> : IValidator<TObject> 
    {
        private readonly string propertyName;
        private readonly Func<TObject, string> validation;

        public DelegateValidator(string propertyName, Func<TObject, string> validation)
        {
            this.propertyName = propertyName;
            this.validation = validation;
        }

        public DelegateValidator(Expression<Func<TObject, object>> propertySelector, Func<TObject, string> validation)
            : this(ExpressionUtils.GetPropertyName(propertySelector), validation)
        { }

        public IEnumerable<ValidationError> Validate(TObject instance)
        {
            string errorMessage = validation(instance);
            return string.IsNullOrEmpty(errorMessage) ? null : new[] { new ValidationError(propertyName, errorMessage) };
        }
    }
         
}