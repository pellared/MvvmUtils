using Pellared.Infrastructure.Validation.Generic;

namespace Pellared.Infrastructure.Validation
{
    public interface IValidator<in TObject> : IValidator<TObject, ValidationError> { }
}
