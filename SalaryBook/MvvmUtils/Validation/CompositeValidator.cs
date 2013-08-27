using Pellared.MvvmUtils.Validation.Generic;

namespace Pellared.MvvmUtils.Validation
{
    public class CompositeValidator<TObject> : CompositeValidator<TObject, ValidationError>, IValidator<TObject>
    {
        public CompositeValidator(params IValidator<TObject, ValidationError>[] validators)
            : base(validators)
        {
            
        }
    }
}
