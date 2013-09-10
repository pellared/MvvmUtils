using System;
using System.Collections.Generic;
using System.Linq;

namespace Pellared.Utils.Mvvm.Validation.Generic
{
    public class DelegateValidator<TObject, TError> : IValidator<TObject, TError>
    {
        private readonly Func<TObject, TError> validation;

        public DelegateValidator(Func<TObject, TError> validation)
        {
            this.validation = validation;
        }

        public virtual IEnumerable<TError> Validate(TObject instance)
        {
            TError validationResult = validation(instance);
            return validationResult.EqualsDefault() ? null : new[] { validationResult };
        }
    }
}