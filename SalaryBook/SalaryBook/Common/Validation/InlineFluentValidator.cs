using FluentValidation;

namespace Pellared.Utils.Mvvm.Validation
{
    public class InlineFluentValidator<TObject> : FluentValidator<TObject>
    {
        public InlineFluentValidator()
            : base(new InlineValidator<TObject>())
        {
        }

        protected InlineValidator<TObject> FluentValidator 
        {
            get { return Validator as InlineValidator<TObject>; }
        }
    }
}