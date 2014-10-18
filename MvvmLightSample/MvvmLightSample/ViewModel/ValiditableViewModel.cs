using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Pellared.MvvmUtils.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace MvvmLightSample.ViewModel
{
    public abstract class ValidatableViewModel : ViewModelBase, IDataErrorInfo
    {
        private DataErrorInfoProvider dataErrorInfoProvider;
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
            ErrorsContainer = new ErrorsContainer();
            ErrorsContainer.ErrorsChanged += OnErrorsChanged;

            dataErrorInfoProvider = new DataErrorInfoProvider(ErrorsContainer);

            validationOnPropertyChangedEnabled = true;
        }

        public ErrorsContainer ErrorsContainer { get; private set; }

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
                    ErrorsContainer.ClearAllErrors();
                }
            }
        }

        public virtual bool HasErrors
        {
            get { return dataErrorInfoProvider.HasErrors; }
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

        public virtual void Validate()
        {
            IEnumerable<ValidationError> errors = Validation();
            ErrorsContainer.ClearAndSetErrors(errors);
        }

        protected abstract IEnumerable<ValidationError> Validation();

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

        protected void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnErrorsChanged(e.PropertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            if (propertyName == dataErrorInfoProvider.ObjectPropertyName)
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