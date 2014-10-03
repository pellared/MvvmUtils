using FluentValidation;
using Pellared.SalaryBook.Common;
using Pellared.SalaryBook.Entities;
using Pellared.SalaryBook.Properties;
using System;
using System.Threading;

namespace Pellared.SalaryBook.Validators
{
    public class SalaryComplexValidator : FluentInlineValidator<ISalary>
    {
        public SalaryComplexValidator()
        {
            FluentValidator.RuleFor(x => x.BirthDate)
                .Must(OlderThan18)
                .WithMessage(Resources.ComplexBirthDateValidatationErrorText);
        }

        private static bool OlderThan18(DateTime? birthDate)
        {
            Thread.Sleep(1000);
            return birthDate < DateTime.Today.AddYears(-18);
        }
    }
}