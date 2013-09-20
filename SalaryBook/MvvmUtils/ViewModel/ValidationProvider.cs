using System;
using System.Collections.Generic;
using System.Linq;

namespace Pellared.Utils.Mvvm.ViewModel
{
    public class ValidationProvider<TError>
    {
        private readonly IErrorsContainer<TError> errorsContainer;
        private readonly Func<IEnumerable<TError>> validation;
        private readonly Func<TError, string> propertyNameSelector;

        public ValidationProvider(IErrorsContainer<TError> errorsContainer, Func<IEnumerable<TError>> validation, Func<TError, string> propertyNameSelector)
        {
            this.errorsContainer = errorsContainer;
            this.validation = validation;
            this.propertyNameSelector = propertyNameSelector;
        }

        public void Validate()
        {
            errorsContainer.ClearAllErrors();

            IEnumerable<TError> errors = validation();
            if (errors != null)
            {
                var propertyErrors = errors.GroupBy(propertyNameSelector);
                foreach (IGrouping<string, TError> validationErrors in propertyErrors)
                {
                    errorsContainer.SetErrors(validationErrors.Key, validationErrors);
                }
            }
        }
    }
}