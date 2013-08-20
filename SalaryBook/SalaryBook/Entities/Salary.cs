using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pellared.SalaryBook.Entities
{
    public class Salary
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public double SalaryValue { get; set; }
    }
}