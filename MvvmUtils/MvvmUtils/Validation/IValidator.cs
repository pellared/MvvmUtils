using System.Collections.Generic;

namespace Pellared.MvvmUtils.Validation
{
    public interface IValidator<in TObject>
    {
        IEnumerable<ValidationError> Validate(TObject instance);
    }
}