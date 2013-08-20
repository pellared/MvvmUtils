using Pellared.Infrastructure.Validation.Generic;

namespace Pellared.Infrastructure.Validation
{
    public class CompositeValidator<TObject> : CompositeValidator<TObject, ValidationError>, IValidator<TObject>
    {
        public CompositeValidator(params IValidator<TObject, ValidationError>[] validators)
            : base(validators)
        {
            
        }
    }
}
