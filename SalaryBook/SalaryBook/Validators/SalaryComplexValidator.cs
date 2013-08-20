using System;
using System.Collections.Generic;
using System.Threading;

using Pellared.Infrastructure.Validation;
using Pellared.SalaryBook.Entities;
using Pellared.SalaryBook.Properties;

namespace Pellared.SalaryBook.Validators
{
    public class SalaryComplexValidator : DelegateValidator<ISalary>
    {
        public SalaryComplexValidator()
            : base(x => x.BirthDate, BirthDateValidation)
        {
        }

        private static string BirthDateValidation(ISalary salary)
        {
            Thread.Sleep(3000);
            if (salary.BirthDate > DateTime.Today.AddYears(-18))
                return Resources.ComplexBirthDateValidatationErrorText;

            return null;
        }
    }
}