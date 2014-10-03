using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace Pellared.Common.Mvvm.ViewModel
{
    public interface IErrorsContainer<T>
    {
        event EventHandler<ErrorsChangedEventArgs> ErrorsChanged; 

        bool HasErrors { get; }

        ILookup<string, IEnumerable<T>> GetAllErrors();

        IEnumerable<T> GetErrors(string propertyName);

        void ClearAllErrors();

        void ClearErrors(string propertyName);

        void SetErrors(string propertyName, IEnumerable<T> errors);
    }
}