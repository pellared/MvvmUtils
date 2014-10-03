using Pellared.Common.Mvvm.Validation;
using System.Collections.Generic;

namespace Pellared.Utils.Mvvm.Validation.Generic
{
    public interface IValidator<in TObject, out TError>
        where TError : ValidationError
    {
        IEnumerable<TError> Validate(TObject instance);
    }
}