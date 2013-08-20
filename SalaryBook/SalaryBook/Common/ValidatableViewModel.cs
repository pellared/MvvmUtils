using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

using Pellared.Infrastructure;
using Pellared.Infrastructure.Validation;
using Pellared.Infrastructure.ViewModel;

namespace Pellared.SalaryBook.Common
{
    public abstract class ValidatableViewModel : ViewModelBase, IDataErrorInfo
    {
        private DataErrorInfoProvider<ValidationError> dataErrorInfoProvider;

        private ErrorsContainer<ValidationError> errorsContainer;
        private ValidationProvider validationProvider;

        private bool validationOnPropertyChangedEnabled;

        protected ValidatableViewModel()
        {
            Initialize();
        }

        protected ValidatableViewModel(IMessenger messenger)
            : base(messenger)
        {
            Initialize();
        }

        private void Initialize()
        {
            errorsContainer = new ErrorsContainer<ValidationError>(OnErrorsChanged);
            dataErrorInfoProvider = new DataErrorInfoProvider<ValidationError>(ArrayFormat.First, errorsContainer);
            validationProvider = new ValidationProvider(errorsContainer, Validation);
            
            validationOnPropertyChangedEnabled = true;
        }

        public bool ValidationOnPropertyChangedEnabled
        {
            get { return validationOnPropertyChangedEnabled; }
            set
            {
                validationOnPropertyChangedEnabled = value;

                if (validationOnPropertyChangedEnabled)
                {
                    Validate();
                }
                else
                {
                    errorsContainer.ClearAllErrors();
                }
            }
        }

        public virtual bool HasErrors
        {
            get { return DataErrorInfoProvider.HasErrors; }
        }

        public virtual string Error
        {
            get 
            {
                return DataErrorInfoProvider.Error;
            }
        }

        protected DataErrorInfoProvider<ValidationError> DataErrorInfoProvider
        {
            get { return dataErrorInfoProvider; }
        }

        public virtual string this[string columnName]
        {
            get
            {
                return DataErrorInfoProvider[columnName];
            }
        }

        public virtual void Validate()
        {
            validationProvider.Validate();
        }

        protected override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            base.RaisePropertyChanged(propertyExpression);
            ValidateOnPropertyChanged();
        }

        protected override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression, T oldValue, T newValue, bool broadcast)
        {
            base.RaisePropertyChanged(propertyExpression, oldValue, newValue, broadcast);
            ValidateOnPropertyChanged();
        }

        protected override void RaisePropertyChanged<T>(string propertyName, T oldValue, T newValue, bool broadcast)
        {
            base.RaisePropertyChanged(propertyName, oldValue, newValue, broadcast);
            ValidateOnPropertyChanged();
        }

        protected override void RaisePropertyChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);
            ValidateOnPropertyChanged();
        }

        protected abstract IEnumerable<ValidationError> Validation();

        protected void OnErrorsChanged(string propertyName)
        {
            if (propertyName == DataErrorInfoProvider.ObjectPropertyName)
            {
                // object error
                propertyName = "Error";
            }

            // notify for IDataErrorInfo
            base.RaisePropertyChanged(propertyName);
            base.RaisePropertyChanged("HasErrors");
        }

        private void ValidateOnPropertyChanged()
        {
            if (ValidationOnPropertyChangedEnabled)
            {
                Validate();
            }
        }
    }
}