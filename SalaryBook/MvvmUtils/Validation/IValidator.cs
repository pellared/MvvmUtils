using Pellared.MvvmUtils.Validation.Generic;

namespace Pellared.MvvmUtils.Validation
{
    public interface IValidator<in TObject> : IValidator<TObject, ValidationError> { }
}
