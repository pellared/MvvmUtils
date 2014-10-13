using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.MvvmUtils.Validation
{
    public class ValidationFacadeBuilder<TError>
        where TError : ValidationError
    {
        private IErrorsContainer<TError> errorsContainer;
        private IDataErrorInfo dataErrorInfoProvider;
        private INotifyDataErrorInfo notifyDataErrorInfoProvider;
        private IValidationProvider validationProvider;
        private Func<IEnumerable<TError>> validation;
        

        public ValidationFacade<TError> Build()
        {
            if (errorsContainer == null)
            {
                errorsContainer = new ErrorsContainer<TError>();
            }

            if (dataErrorInfoProvider == null && notifyDataErrorInfoProvider == null)
            {
                var errorProvider = new DataErrorInfoProvider<TError>(errorsContainer);
                dataErrorInfoProvider = errorProvider;
                notifyDataErrorInfoProvider = errorProvider;
            }

            if (validationProvider == null && validation != null)
            { 
                validationProvider = new ValidationProvider<TError>(errorsContainer, validation);
            }

            if (validationProvider == null)
            {
                throw new InvalidOperationException("IValidationProvider was not set");
            }

            return new ValidationFacade<TError>(errorsContainer, dataErrorInfoProvider, notifyDataErrorInfoProvider, validationProvider);
        }

        public ValidationFacadeBuilder<TError> With(IErrorsContainer<TError> errorsContainer)
        {
            this.errorsContainer = errorsContainer;
            return this;
        }

        public ValidationFacadeBuilder<TError> With(IDataErrorInfo dataErrorInfo)
        {
            dataErrorInfoProvider = dataErrorInfo;
            return this;
        }

        public ValidationFacadeBuilder<TError> With(INotifyDataErrorInfo notifyDataErrorInfo)
        {
            notifyDataErrorInfoProvider = notifyDataErrorInfo;
            return this;
        }

        public ValidationFacadeBuilder<TError> With(IValidationProvider validationProvider)
        {
            this.validationProvider = validationProvider;
            return this;
        }

        public ValidationFacadeBuilder<TError> With(Func<IEnumerable<TError>> validation)
        {
            this.validation = validation;
            return this;
        }
    }
}
