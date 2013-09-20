using System.Collections.Generic;

namespace Pellared.Utils.Mvvm.Validation.Generic
{
    public interface IValidator<in TObject, out TError>
    {
        IEnumerable<TError> Validate(TObject instance);
    }
}