using FluentValidation;

namespace Pellared.SalaryBook.Common
{
    public class FluentInlineValidator<TObject> : FluentValidator<TObject>
    {
        public FluentInlineValidator()
            : base(new InlineValidator<TObject>())
        {
        }

        public InlineValidator<TObject> FluentValidator
        {
            get { return Validator as InlineValidator<TObject>; }
        }
    }
}