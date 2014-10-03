using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace Pellared.MvvmUtils.Validation
{
    public interface IDataErrorInfoProvider : IDataErrorInfo, INotifyDataErrorInfo
    {
    }

    public class DataErrorInfoProvider<TError> : IDataErrorInfoProvider
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

        public DataErrorInfoProvider(IErrorsContainer<TError> errorsContainer, string objectPropertyName, Func<IEnumerable<TError>, string> errorsFormatter)
        {
            Contract.Requires<ArgumentNullException>(errorsContainer != null, "errorsContainer");
            Contract.Requires<ArgumentNullException>(objectPropertyName != null, "objectPropertyName");
            Contract.Requires<ArgumentNullException>(errorsFormatter != null, "errorsFormatter");

            ObjectPropertyName = objectPropertyName;
            ErrorsContainer = errorsContainer;
            ErrorsFormatter = errorsFormatter;
        }

        public DataErrorInfoProvider(IErrorsContainer<TError> errorsContainer, string objectPropertyName, ArrayFormat arrayFormat = ArrayFormat.First)
            : this(errorsContainer, objectPropertyName, ArrayFormatter.GetErrorFormatter(arrayFormat) as Func<IEnumerable<TError>, string>)
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
        public DataErrorInfoProvider(IErrorsContainer<ValidationError> errorsContainer, string objectPropertyName, Func<IEnumerable<ValidationError>, string> errorsFormatter)
            : base(errorsContainer, objectPropertyName, errorsFormatter)
        {
        }

        public DataErrorInfoProvider(IErrorsContainer<ValidationError> errorsContainer, string objectPropertyName, ArrayFormat arrayFormat = ArrayFormat.First)
            : base(errorsContainer, objectPropertyName, arrayFormat)
        {
        }
    }
}