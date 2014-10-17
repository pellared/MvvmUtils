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

        private readonly DataErrorInfoProvider dataErrorInfoProvider;

        protected ValidatableViewModel(ArrayFormat errorFormat = ArrayFormat.First)
        {
            ErrorsContainer = new ErrorsContainer();
            ErrorsContainer.ErrorsChanged += OnErrorsChanged;

            dataErrorInfoProvider = new DataErrorInfoProvider(ErrorsContainer, errorFormat, ObjectErrorPropertyName);
        }

        protected ValidatableViewModel(IMessenger messenger, ArrayFormat errorFormat = ArrayFormat.First)
            : base(messenger)
        {
            ErrorsContainer = new ErrorsContainer();
            ErrorsContainer.ErrorsChanged += OnErrorsChanged;

            dataErrorInfoProvider = new DataErrorInfoProvider(ErrorsContainer, errorFormat, ObjectErrorPropertyName);
        }

        protected ValidatableViewModel(IErrorsContainer<ValidationError> errorsContainer, ArrayFormat errorFormat = ArrayFormat.First)
        {
            ErrorsContainer = errorsContainer;
            ErrorsContainer.ErrorsChanged += OnErrorsChanged;

            dataErrorInfoProvider = new DataErrorInfoProvider(ErrorsContainer, errorFormat, ObjectErrorPropertyName);
        }

        public IErrorsContainer<ValidationError> ErrorsContainer { get; private set; }

        public virtual bool HasErrors
        {
            get { return ErrorsContainer.HasErrors; }
        }

        public virtual string Error
        {
            get
            {
                return dataErrorInfoProvider.Error;
            }
        }

        public virtual string this[string columnName]
        {
            get
            {
                return dataErrorInfoProvider[columnName];
            }
        }

        public void Validate()
        {
            IEnumerable<ValidationError> errors = Validation();
            ErrorsContainer.ClearAndSetErrors(errors);
        }

        protected abstract IEnumerable<ValidationError> Validation();

        protected override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            base.RaisePropertyChanged(propertyExpression);
            Validate();
        }

        protected override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression, T oldValue, T newValue, bool broadcast)
        {
            base.RaisePropertyChanged(propertyExpression, oldValue, newValue, broadcast);
            Validate();
        }

        protected override void RaisePropertyChanged<T>(string propertyName, T oldValue, T newValue, bool broadcast)
        {
            base.RaisePropertyChanged(propertyName, oldValue, newValue, broadcast);
            Validate();
        }

        protected override void RaisePropertyChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);
            Validate();
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