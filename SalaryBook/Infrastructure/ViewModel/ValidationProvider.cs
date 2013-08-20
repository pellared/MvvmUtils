using System;
using System.Collections.Generic;
using System.Linq;

using Pellared.Infrastructure.Validation;

namespace Pellared.Infrastructure.ViewModel
{
    public class ValidationProvider
    {
        private readonly IErrorsContainer<ValidationError> errorsContainer;
        private readonly Func<IEnumerable<ValidationError>> validation;

        public ValidationProvider(IErrorsContainer<ValidationError> errorsContainer, Func<IEnumerable<ValidationError>> validation)
        {
            this.errorsContainer = errorsContainer;
            this.validation = validation;
        }

        public void Validate()
        {
            errorsContainer.ClearAllErrors();

            IEnumerable<ValidationError> errors = validation();
            if (errors != null)
            {
                var propertyErrors = errors.GroupBy(x => x.PropertyName);
                foreach (IGrouping<string, ValidationError> validationErrors in propertyErrors)
                {
                    errorsContainer.SetErrors(validationErrors.Key, validationErrors);
                }
            }
        }
    }
}