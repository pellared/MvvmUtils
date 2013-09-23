using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Pellared.Utils.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace MvvmLightSample.ViewModel
{
    public abstract class ValidatableViewModel : ViewModelBase, IDataErrorInfo
    {
        private DataErrorInfoProvider<PropertyError> dataErrorInfoProvider;
        private ErrorsContainer<PropertyError> errorsContainer;
        private ValidationProvider<PropertyError> validationProvider;

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
            errorsContainer = new ErrorsContainer<PropertyError>(OnErrorsChanged);
            dataErrorInfoProvider = new DataErrorInfoProvider<PropertyError>(ArrayFormat.First, errorsContainer);
            validationProvider = new ValidationProvider<PropertyError>(errorsContainer, Validation, error => error.PropertyName);

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

        protected DataErrorInfoProvider<PropertyError> DataErrorInfoProvider
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

        protected abstract IEnumerable<PropertyError> Validation();

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