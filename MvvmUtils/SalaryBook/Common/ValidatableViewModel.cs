using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Pellared.MvvmUtils.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Pellared.SalaryBook.Common
{
    public abstract class ValidatableViewModel : ViewModelBase, IDataErrorInfo
    {
        public const string ObjectErrorPropertyName = "";

        protected ValidatableViewModel()
        {
            ValidationProvider = new ValidationFacadeBuilder<ValidationError>()
                .With(Validation)
                .Build();

            ValidationProvider.ErrorsChanged += OnErrorsChanged;
        }

        protected ValidatableViewModel(IMessenger messenger)
            : base(messenger)
        {
            ValidationProvider = new ValidationFacadeBuilder<ValidationError>()
                .With(Validation)
                .Build();

            ValidationProvider.ErrorsChanged += OnErrorsChanged;
        }

        protected ValidatableViewModel(IErrorsContainer<ValidationError> errorsContainer)
        {
            ValidationProvider = new ValidationFacadeBuilder<ValidationError>()
                .With(errorsContainer)
                .With(Validation)
                .Build();

            ValidationProvider.ErrorsChanged += OnErrorsChanged;
        }

        public ValidationFacade<ValidationError> ValidationProvider { get; private set; }


        public virtual bool HasErrors
        {
            get { return ValidationProvider.HasErrors; }
        }

        public virtual string Error
        {
            get
            {
                return ((IDataErrorInfo)ValidationProvider).Error;
            }
        }

        public virtual string this[string columnName]
        {
            get
            {
                return ((IDataErrorInfo)ValidationProvider)[columnName];
            }
        }

        protected abstract IEnumerable<ValidationError> Validation();

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
            ValidationProvider.Validate();
        }

        protected override void RaisePropertyChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);
            ValidationProvider.Validate();
        }

        private void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnErrorsChanged(e.PropertyName);
        }

        private void OnErrorsChanged(string propertyName)
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