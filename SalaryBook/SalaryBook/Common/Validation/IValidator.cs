using Pellared.Common.Mvvm.ViewModel;
using Pellared.Utils.Mvvm.Validation.Generic;

namespace Pellared.Utils.Mvvm.Validation
{
    public interface IValidator<in TObject> : IValidator<TObject, ValidationError> { }
}
