using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Pellared.Common.Mvvm.Validation
{
    public interface IValidationProvider
    {
        void Validate();

        void Disable();

        void Enable();
    }

    public class ValidationProvider<TError> : IValidationProvider
        where TError : ValidationError
    {
        private bool isEnabled;

        public ValidationProvider(IErrorsContainer<TError> errorsContainer, Func<IEnumerable<TError>> validation)
        {
            Contract.Requires<ArgumentNullException>(errorsContainer != null, "errorsContainer");
            Contract.Requires<ArgumentNullException>(validation != null, "validation");

            ErrorsContainer = errorsContainer;
            Validation = validation;
            isEnabled = true;
        }

        public IErrorsContainer<TError> ErrorsContainer { get; private set; }

        public Func<IEnumerable<TError>> Validation { get; set; }

        public void Validate()
        {
            if (isEnabled)
            {
                ErrorsContainer.ClearAllErrors();

                IEnumerable<TError> errors = Validation();
                if (!errors.IsNullOrEmpty())
                {
                    ErrorsContainer.SetErrors(errors);
                }
            }
        }

        public void Enable()
        {
            isEnabled = true;
        }

        public void Disable()
        {
            isEnabled = false;
        }
    }

    public class ValidationProvider : ValidationProvider<ValidationError>
    {
        public ValidationProvider(IErrorsContainer<ValidationError> errorsContainer, Func<IEnumerable<ValidationError>> validation)
            : base(errorsContainer, validation)
        {
        }
    }
}