using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace Pellared.MvvmUtils.Validation
{
    public class DataErrorInfoProvider<TError> : IDataErrorInfo, INotifyDataErrorInfo
        where TError : ValidationError
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add
            {
                ErrorsContainer.ErrorsChanged += value;
            }
            remove
            {
                ErrorsContainer.ErrorsChanged -= value;
            }
        }

        public DataErrorInfoProvider(IErrorsContainer<TError> errorsContainer, Func<IEnumerable<TError>, string> errorsFormatter, string objectPropertyName = "")
        {
            Contract.Requires<ArgumentNullException>(errorsContainer != null, "errorsContainer");
            Contract.Requires<ArgumentNullException>(objectPropertyName != null, "objectPropertyName");
            Contract.Requires<ArgumentNullException>(errorsFormatter != null, "errorsFormatter");

            ObjectPropertyName = objectPropertyName;
            ErrorsContainer = errorsContainer;
            ErrorsFormatter = errorsFormatter;
        }

        public DataErrorInfoProvider(IErrorsContainer<TError> errorsContainer, ArrayFormat arrayFormat = ArrayFormat.First, string objectPropertyName = "")
            : this(errorsContainer, ArrayFormatter.GetErrorFormatter(arrayFormat) as Func<IEnumerable<TError>, string>, objectPropertyName)
        {
        }

        public string ObjectPropertyName { get; private set; }

        public IErrorsContainer<TError> ErrorsContainer { get; private set; }

        public Func<IEnumerable<TError>, string> ErrorsFormatter { get; private set; }

        public virtual string Error
        {
            get { return GetErrorsAndFormat(ObjectPropertyName); }
        }

        public virtual string this[string columnName]
        {
            get { return GetErrorsAndFormat(columnName); }
        }

        private string GetErrorsAndFormat(string propertyName)
        {
            IEnumerable<TError> allErrors = ErrorsContainer.GetErrors(propertyName);
            return ErrorsFormatter(allErrors);
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return ErrorsContainer.GetErrors(propertyName);
        }

        public bool HasErrors
        {
            get { return ErrorsContainer.HasErrors; }
        }
    }

    public class DataErrorInfoProvider : DataErrorInfoProvider<ValidationError>
    {
        public DataErrorInfoProvider(IErrorsContainer<ValidationError> errorsContainer, Func<IEnumerable<ValidationError>, string> errorsFormatter, string objectPropertyName = "")
            : base(errorsContainer, errorsFormatter, objectPropertyName)
        {
        }

        public DataErrorInfoProvider(IErrorsContainer<ValidationError> errorsContainer, ArrayFormat arrayFormat = ArrayFormat.First, string objectPropertyName = "")
            : base(errorsContainer, arrayFormat, objectPropertyName)
        {
        }
    }
}