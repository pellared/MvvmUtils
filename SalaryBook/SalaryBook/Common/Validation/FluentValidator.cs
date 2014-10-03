using System.Collections.Generic;
using System.Linq;

using FluentValidation.Results;
using Pellared.Common.Mvvm.ViewModel;

namespace Pellared.Utils.Mvvm.Validation
{
    public class FluentValidator<TObject> : IValidator<TObject>
    {
        public FluentValidator(FluentValidation.IValidator<TObject> validator)
        {
            Validator = validator;
        }

        protected FluentValidation.IValidator<TObject> Validator { get; set; }

        public virtual IEnumerable<ValidationError> Validate(TObject instance)
        {
            ValidationResult results = Validator.Validate(instance);
            return ConvertResult(results);
        }

        private static IEnumerable<ValidationError> ConvertResult(ValidationResult results)
        {
            if (results != null && results.Errors.Any())
            {
                return results.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage));
            }

            return null;
        }
    }
}