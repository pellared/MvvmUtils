using System.Collections.Generic;

namespace Pellared.Common.Mvvm.Validation
{
    public interface IValidator<in TObject>
    {
        IEnumerable<ValidationError> Validate(TObject instance);
    }
}