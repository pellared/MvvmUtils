using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Pellared.Common.Mvvm.ViewModel
{
    public class DataErrorInfoProvider<TError> : IDataErrorInfo
    {
        public DataErrorInfoProvider(IErrorsContainer<TError> errorsContainer, string objectPropertyName, Func<IEnumerable<TError>, string> errorsFormatter)
        {
            Contract.Requires<ArgumentNullException>(errorsContainer != null, "errorsContainer");
            Contract.Requires<ArgumentNullException>(objectPropertyName != null, "objectPropertyName");
            Contract.Requires<ArgumentNullException>(errorsFormatter != null, "errorsFormatter");

            ObjectPropertyName = objectPropertyName;
            this.ErrorsContainer = errorsContainer;
            this.ErrorsFormatter = errorsFormatter;
        }

        public DataErrorInfoProvider(IErrorsContainer<TError> errorsContainer, string objectPropertyName, ArrayFormat arrayFormat)
            : this(errorsContainer, objectPropertyName, ArrayFormatter.GetErrorFormatter(arrayFormat) as Func<IEnumerable<TError>, string>)
        {
            Contract.Requires<ArgumentNullException>(errorsContainer != null, "errorsContainer");
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
    }
}