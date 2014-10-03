using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.Common.Mvvm.ViewModel
{
    public class CompositeErrorsContainer<TError> : IErrorsContainer<TError>
    {
        private readonly List<IErrorsContainer<TError>> errorsContainers;

        public event EventHandler<ErrorsChangedEventArgs> ErrorsChanged
        {
            add
            {
                foreach (var errorContainer in errorsContainers)
                {
                    errorContainer.ErrorsChanged += value;
                }
            }
            remove
            {
                foreach (var errorContainer in errorsContainers)
                {
                    errorContainer.ErrorsChanged -= value;
                }
            }
        }

        public CompositeErrorsContainer()
        {
            errorsContainers = new List<IErrorsContainer<TError>>();
        }

        public CompositeErrorsContainer(IEnumerable<IErrorsContainer<TError>> errorsContainers)
        {
            errorsContainers = errorsContainers.ToList();
        }

        public IEnumerable<IErrorsContainer<TError>> ErrorsContainers
        {
            get { return errorsContainers; }
        }

        public bool HasErrors
        {
            get { return ErrorsContainers.Any(x => x.HasErrors); }
        }

        public ILookup<string, IEnumerable<TError>> GetAllErrors()
        {
            return errorsContainers.SelectMany(x => x.GetAllErrors()).
                ToLookup(x => x.Key, y => y.SelectMany(z => z));
        }

        public IEnumerable<TError> GetErrors(string propertyName)
        {
            foreach (IErrorsContainer<TError> errorsContainer in errorsContainers)
            {
                IEnumerable<TError> localErrors = errorsContainer.GetErrors(propertyName);
                foreach (TError error in localErrors)
                {
                    yield return error;
                }
            }
        }

        public void ClearAllErrors()
        {
            foreach (IErrorsContainer<TError> errorsContainer in errorsContainers)
            {
                errorsContainer.ClearAllErrors();
            }
        }

        public void ClearErrors(string propertyName)
        {
            foreach (IErrorsContainer<TError> errorsContainer in errorsContainers)
            {
                errorsContainer.ClearErrors(propertyName);
            }
        }

        public void SetErrors(string propertyName, IEnumerable<TError> errors)
        {
            foreach (IErrorsContainer<TError> errorsContainer in errorsContainers)
            {
                errorsContainer.SetErrors(propertyName, errors);
            }
        }

        public CompositeErrorsContainer<TError> AddErrorsContainer(IErrorsContainer<TError> errorsContainer)
        {
            errorsContainers.Add(errorsContainer);
            return this;
        }
    }
}
