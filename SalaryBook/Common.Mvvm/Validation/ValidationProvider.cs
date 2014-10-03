using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Pellared.Common.Mvvm.ViewModel
{
    public interface IValidationProvider
    {
        void Validate();

        void Disable();

        void Enable();
    }

    public class ValidationProvider<TError> : IValidationProvider
    {
        private readonly IErrorsContainer<TError> errorsContainer;
        private readonly Func<IEnumerable<TError>> validation;
        private readonly Func<TError, string> propertyNameSelector;

        private bool isEnabled;

        public ValidationProvider(IErrorsContainer<TError> errorsContainer, Func<IEnumerable<TError>> validation,
            Func<TError, string> propertyNameSelector)
        {
            Contract.Requires<ArgumentNullException>(errorsContainer != null, "errorsContainer");
            Contract.Requires<ArgumentNullException>(validation != null, "validation");
            Contract.Requires<ArgumentNullException>(propertyNameSelector != null, "propertyNameSelector");

            this.errorsContainer = errorsContainer;
            this.validation = validation;
            this.propertyNameSelector = propertyNameSelector;
            isEnabled = true;
        }

        public void Validate()
        {
            if (isEnabled)
            {
                errorsContainer.ClearAllErrors();

                IEnumerable<TError> errors = validation();
                if (errors != null)
                {
                    IEnumerable<IGrouping<string, TError>> propertyErrors = errors.GroupBy(propertyNameSelector);
                    foreach (IGrouping<string, TError> validationErrors in propertyErrors)
                    {
                        errorsContainer.SetErrors(validationErrors.Key, validationErrors);

                    }
                }
            }
        }

        public void Enable()
        {
            Validate();
            isEnabled = true;
        }

        public void Disable()
        {
            errorsContainer.ClearAllErrors();
            isEnabled = false;
        }
    }
}