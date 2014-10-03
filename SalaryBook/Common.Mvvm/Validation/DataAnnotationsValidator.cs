using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Pellared.Common.Mvvm.Validation
{
    public class DataAnnotationsValidator<TObject> : IValidator<TObject>
    {
        public virtual IEnumerable<ValidationError> Validate(TObject instance)
        {
            var context = new ValidationContext(instance, null, null);
            var validationResult = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(instance, context, validationResult);

            if (valid) return null;

            IEnumerable<ValidationError> errors = ConvertResult(validationResult);
            return errors;
        }

        private static IEnumerable<ValidationError> ConvertResult(List<ValidationResult> validationResult)
        {
            IEnumerable<ValidationError> errors =
                    from result in validationResult
                    from propertyName in result.MemberNames
                    select new ValidationError(propertyName, result.ErrorMessage);

            return errors;
        }
    }
}