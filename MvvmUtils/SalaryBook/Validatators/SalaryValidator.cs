using Infrastructure.Validation;

using SalaryBook.Entities;
using SalaryBook.Properties;

using FluentValidation;
using FluentValidation.Results;

namespace SalaryBook.Validatators
{
    public class SalaryValidator : AbstractFluentValidator<ISalary>
    {
        public SalaryValidator()
        {
            FluentValidator.RuleFor(x => x.FirstName).NotEmpty().WithName(Resources.FirstNameText);
            FluentValidator.RuleFor(x => x.LastName).NotEmpty().WithName(Resources.LastNameText);
            FluentValidator.RuleFor(x => x.BirthDate).NotEmpty().WithName(Resources.BirthDateText);
            FluentValidator.Custom(delegate(ISalary x)
            {
                if (string.IsNullOrEmpty(x.FirstName)) return new ValidationFailure(string.Empty, "IMIĘ NIE MOŻE BY PUSTE");
                return null;
            });
        }
    }
}