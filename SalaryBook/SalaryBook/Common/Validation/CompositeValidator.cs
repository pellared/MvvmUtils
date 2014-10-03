using Pellared.Common.Mvvm.ViewModel;
using Pellared.Utils.Mvvm.Validation.Generic;

namespace Pellared.Utils.Mvvm.Validation
{
    public class CompositeValidator<TObject> : CompositeValidator<TObject, ValidationError>, IValidator<TObject>
    {
        public CompositeValidator(params IValidator<TObject>[] validators)
            : base(validators)
        {
            
        }
    }
}
