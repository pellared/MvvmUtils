using System.Collections.Generic;

namespace Pellared.Infrastructure.Validation.Generic
{
    public interface IValidator<in TObject, out TError>
    {
        IEnumerable<TError> Validate(TObject instance);
    }
}