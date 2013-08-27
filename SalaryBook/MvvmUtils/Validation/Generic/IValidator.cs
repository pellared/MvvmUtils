using System.Collections.Generic;

namespace Pellared.MvvmUtils.Validation.Generic
{
    public interface IValidator<in TObject, out TError>
    {
        IEnumerable<TError> Validate(TObject instance);
    }
}