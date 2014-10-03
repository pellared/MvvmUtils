using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace Pellared.Common.Mvvm.ViewModel
{
    public class ErrorsChangedEventArgs : EventArgs
    {
        public ErrorsChangedEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
    }

    /// <summary>
    /// Manages validation errors for an object, notifying when the error state changes.
    /// </summary>
    /// <typeparam name="TError">The type of the error object.</typeparam>
    public class ErrorsContainer<TError> : IErrorsContainer<TError>
    {
        private static readonly TError[] NoErrors = new TError[0];

        private Dictionary<string, List<TError>> propertyErrors = new Dictionary<string, List<TError>>();

        public event EventHandler<ErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get { return propertyErrors.Count != 0; }
        }

        public ILookup<string, IEnumerable<TError>> GetAllErrors()
        {
            return propertyErrors.ToLookup(x => x.Key, y => y.Value.AsEnumerable());
        }

        public IEnumerable<TError> GetErrors(string propertyName)
        {
            string localPropertyName = propertyName ?? string.Empty;
            List<TError> currentValidationResults;
            if (propertyErrors.TryGetValue(localPropertyName, out currentValidationResults))
            {
                return currentValidationResults;
            }
            else
            {
                return NoErrors;
            }
        }

        public void ClearAllErrors()
        {
            IEnumerable<string> propertyNames = propertyErrors.Select(x => x.Key);
            propertyErrors = new Dictionary<string, List<TError>>();
            foreach (string propertyName in propertyNames)
            {
                RaiseErrorsChanged(new ErrorsChangedEventArgs(propertyName));
            }
        }

        public void ClearErrors(string propertyName)
        {
            SetErrors(propertyName, new List<TError>());
        }

        public void SetErrors(string propertyName, IEnumerable<TError> errors)
        {
            if (errors == null)
            {
                throw new ArgumentNullException("newValidationResults");
            }

            string localPropertyName = propertyName ?? string.Empty;
            bool hasCurrentErrors = propertyErrors.ContainsKey(localPropertyName);
            bool hasNewErrors = errors != null && errors.Count() > 0;

            if (hasCurrentErrors || hasNewErrors)
            {
                if (hasNewErrors)
                {
                    propertyErrors[localPropertyName] = new List<TError>(errors);
                }
                else
                {
                    propertyErrors.Remove(localPropertyName);
                    
                }

                RaiseErrorsChanged(new ErrorsChangedEventArgs(localPropertyName));
            }
        }

        protected void RaiseErrorsChanged(ErrorsChangedEventArgs eventArgs)
        {
            EventHandler<ErrorsChangedEventArgs> handler = ErrorsChanged;
            if (handler != null)
            {
                handler(this, eventArgs);
            }
        }       
    }
}