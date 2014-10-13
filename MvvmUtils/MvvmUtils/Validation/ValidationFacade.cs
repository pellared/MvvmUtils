using Pellared.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.MvvmUtils.Validation
{
    public class ValidationFacade<TError> : IErrorsContainer<TError>, IDataErrorInfo, INotifyDataErrorInfo, IValidationProvider
        where TError : ValidationError
    {
        private readonly IErrorsContainer<TError> errorsContainer;
        private readonly IDataErrorInfo dataErrorInfoProvider;
        private readonly INotifyDataErrorInfo notifyDataErrorInfoProvider;
        private readonly IValidationProvider validationProvider;

        public ValidationFacade(IErrorsContainer<TError> errorsContainer, IDataErrorInfo dataErrorInfoProvider, INotifyDataErrorInfo notifyDataErrorInfoProvider, IValidationProvider validationProvider)
        {
            Ensure.NotNull(errorsContainer, "errorsContainer");
            Ensure.NotNull(dataErrorInfoProvider, "dataErrorInfoProvider");
            Ensure.NotNull(notifyDataErrorInfoProvider, "notifyDataErrorInfoProvider");
            Ensure.NotNull(validationProvider, "validationProvider");

            this.errorsContainer = errorsContainer;
            this.dataErrorInfoProvider = dataErrorInfoProvider;
            this.notifyDataErrorInfoProvider = notifyDataErrorInfoProvider;
            this.validationProvider = validationProvider;
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add
            {
                errorsContainer.ErrorsChanged += value;
            }
            remove
            {
                errorsContainer.ErrorsChanged -= value;
            }
        }

        public bool HasErrors
        {
            get { return errorsContainer.HasErrors; }
        }

        public ILookup<string, IEnumerable<TError>> GetAllErrors()
        {
            return errorsContainer.GetAllErrors();
        }

        public IEnumerable<TError> GetErrors(string propertyName)
        {
            return errorsContainer.GetErrors(propertyName);
        }

        public void ClearAllErrors()
        {
            errorsContainer.ClearAllErrors();
        }

        public void ClearErrors(string propertyName)
        {
            errorsContainer.ClearErrors(propertyName);
        }

        public void SetErrors(IEnumerable<TError> errors)
        {
            errorsContainer.SetErrors(errors);
        }

        string IDataErrorInfo.Error
        {
            get { return dataErrorInfoProvider.Error; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get { return dataErrorInfoProvider[columnName]; }
        }

        event EventHandler<DataErrorsChangedEventArgs> INotifyDataErrorInfo.ErrorsChanged
        {
            add { notifyDataErrorInfoProvider.ErrorsChanged += value; }
            remove { notifyDataErrorInfoProvider.ErrorsChanged -= value; }
        }

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            return notifyDataErrorInfoProvider.GetErrors(propertyName);
        }

        bool INotifyDataErrorInfo.HasErrors
        {
            get { return notifyDataErrorInfoProvider.HasErrors; }
        }

        public void Validate()
        {
            validationProvider.Validate();
        }

        public void Disable()
        {
            validationProvider.Disable();
        }

        public void Enable()
        {
            validationProvider.Enable();
        }
    }
}
