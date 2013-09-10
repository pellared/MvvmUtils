using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Pellared.Utils.Mvvm.ViewModel
{
    public class DataErrorInfoProvider<TError> : IDataErrorInfo
    {
        private readonly List<IErrorsContainer<TError>> errorsContainers;
        private readonly Func<IEnumerable<TError>, string> errorsFormatter;

        public DataErrorInfoProvider(Func<IEnumerable<TError>, string> errorsFormatter, params IErrorsContainer<TError>[] errorsContainers)
        {
            this.errorsContainers = errorsContainers.ToList();
            this.errorsFormatter = errorsFormatter;
        }

        public DataErrorInfoProvider(ArrayFormat arrayFormat, params IErrorsContainer<TError>[] errorsContainers)
            : this(ArrayFormatter.GetErrorFormatter(arrayFormat) as Func<IEnumerable<TError>, string>, errorsContainers)
        { }

        public virtual string ObjectPropertyName { get { return string.Empty; } }

        public virtual string Error
        {
            get
            {
                return GetErrorsAndFormat(ObjectPropertyName);
            }
        }

        public virtual string this[string columnName]
        {
            get
            {
                return GetErrorsAndFormat(columnName);
            }
        }

        public bool HasErrors
        {
            get { return errorsContainers.Any(x => x.HasErrors); }
        }

        public void AddErrorsContainer(IErrorsContainer<TError> errorsContainer)
        {
            errorsContainers.Add(errorsContainer);
        }

        private string GetErrorsAndFormat(string propertyName)
        {
            List<TError> allErrors = new List<TError>();

            foreach (IErrorsContainer<TError> errorsContainer in errorsContainers)
            {
                IEnumerable<TError> localErrors = errorsContainer.GetErrors(propertyName);
                allErrors.AddRange(localErrors);
            }

            return errorsFormatter(allErrors);
        }
    }
}