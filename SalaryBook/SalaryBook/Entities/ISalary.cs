using System;

namespace Pellared.SalaryBook.Entities
{
    public interface ISalary {
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime? BirthDate { get; set; }
        double SalaryValue { get; set; }
    }
}