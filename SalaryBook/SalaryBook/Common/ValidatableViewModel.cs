using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

using Pellared.Common.Mvvm.ViewModel;
using Pellared.Utils.Mvvm.Validation;

namespace Pellared.SalaryBook.Common
{
    public abstract class ValidatableViewModel : ViewModelBase, IDataErrorInfo
    {
        public const string ObjectErrorPropertyName = "";

        protected ValidatableViewModel()
        {
            ErrorsContainer = new ErrorsContainer<ValidationError>();
            ErrorsContainer.ErrorsChanged += (_, arg) => OnErrorsChanged(arg.PropertyName);
            DataErrorInfoProvider = new DataErrorInfoProvider<ValidationError>(ErrorsContainer, ObjectErrorPropertyName, ArrayFormat.First);
            ValidationProvider = new ValidationProvider<ValidationError>(ErrorsContainer, Validation, error => error.PropertyName);
        }

        protected ValidatableViewModel(IMessenger messenger)
            : base(messenger)
        {
            ErrorsContainer = new ErrorsContainer<ValidationError>();
            ErrorsContainer.ErrorsChanged += (_, arg) => OnErrorsChanged(arg.PropertyName);
            DataErrorInfoProvider = new DataErrorInfoProvider<ValidationError>(ErrorsContainer, ObjectErrorPropertyName, ArrayFormat.First);
            ValidationProvider = new ValidationProvider<ValidationError>(ErrorsContainer, Validation, error => error.PropertyName);
        }

        protected ValidatableViewModel(IErrorsContainer<ValidationError> errorsContainer)
        {
            ErrorsContainer = errorsContainer;
            ErrorsContainer.ErrorsChanged += (_, arg) => OnErrorsChanged(arg.PropertyName);
            DataErrorInfoProvider = new DataErrorInfoProvider<ValidationError>(ErrorsContainer, ObjectErrorPropertyName, ArrayFormat.First);
            ValidationProvider = new ValidationProvider<ValidationError>(ErrorsContainer, Validation, error => error.PropertyName);
        }

        protected ValidatableViewModel(IDataErrorInfo dataErrorInfoProvider)
        {
            ErrorsContainer = new ErrorsContainer<ValidationError>();
            ErrorsContainer.ErrorsChanged += (_, arg) => OnErrorsChanged(arg.PropertyName);
            DataErrorInfoProvider = dataErrorInfoProvider;
            ValidationProvider = new ValidationProvider<ValidationError>(ErrorsContainer, Validation, error => error.PropertyName);
        }

        protected ValidatableViewModel(IErrorsContainer<ValidationError> errorsContainer, IDataErrorInfo dataErrorInfoProvider)
        {
            ErrorsContainer = errorsContainer;
            ErrorsContainer.ErrorsChanged += (_, arg) => OnErrorsChanged(arg.PropertyName);
            DataErrorInfoProvider = dataErrorInfoProvider;
            ValidationProvider = new ValidationProvider<ValidationError>(ErrorsContainer, Validation, error => error.PropertyName);
        }

        protected ValidatableViewModel(IErrorsContainer<ValidationError> errorsContainer, IValidationProvider validationProvider)
        {
            ErrorsContainer = errorsContainer;
            ErrorsContainer.ErrorsChanged += (_, arg) => OnErrorsChanged(arg.PropertyName);
            DataErrorInfoProvider = new DataErrorInfoProvider<ValidationError>(ErrorsContainer, ObjectErrorPropertyName, ArrayFormat.First);
            ValidationProvider = validationProvider;
        }

        protected ValidatableViewModel(IErrorsContainer<ValidationError> errorsContainer, IDataErrorInfo dataErrorInfoProvider, IValidationProvider validationProvider)
        {
            ErrorsContainer = errorsContainer;
            ErrorsContainer.ErrorsChanged += (_, arg) => OnErrorsChanged(arg.PropertyName);
            DataErrorInfoProvider = dataErrorInfoProvider;
            ValidationProvider = validationProvider;
        }

        public IValidationProvider ValidationProvider { get; private set; }

        public IErrorsContainer<ValidationError> ErrorsContainer { get; private set; }

        public IDataErrorInfo DataErrorInfoProvider { get; private set; }

        public virtual bool HasErrors
        {
            get { return ErrorsContainer.HasErrors; }
        }

        public virtual string Error
        {
            get 
            {
                return DataErrorInfoProvider.Error;
            }
        }

        public virtual string this[string columnName]
        {
            get
            {
                return DataErrorInfoProvider[columnName];
            }
        }

        protected override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            base.RaisePropertyChanged(propertyExpression);
            ValidationProvider.Validate();
        }

        protected override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression, T oldValue, T newValue, bool broadcast)
        {
            base.RaisePropertyChanged(propertyExpression, oldValue, newValue, broadcast);
            ValidationProvider.Validate();
        }

        protected override void RaisePropertyChanged<T>(string propertyName, T oldValue, T newValue, bool broadcast)
        {
            base.RaisePropertyChanged(propertyName, oldValue, newValue, broadcast);
            ValidationProvider.Validate(); ;
        }

        protected override void RaisePropertyChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);
            ValidationProvider.Validate();
        }

        protected abstract IEnumerable<ValidationError> Validation();

        protected virtual void OnErrorsChanged(string propertyName)
        {
            if (propertyName == ObjectErrorPropertyName)
            {
                // object error
                propertyName = "Error";
            }

            // notify for IDataErrorInfo
            base.RaisePropertyChanged(propertyName);
            base.RaisePropertyChanged("HasErrors");
        }
    }
}