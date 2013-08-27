using System;

using Pellared.MvvmUtils.Services.Modal;
using Pellared.SalaryBook.Entities;

namespace Pellared.SalaryBook.ViewModels
{
    public class SalaryDialogViewModel : IModalViewModel
    {
        private readonly Salary salary;

        public SalaryDialogViewModel(Salary salary)
        {
            this.salary = salary;
        }

        public bool Closed { get; set; }

        public string Title
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public string FirstName
        {
            get { return salary.FirstName; }
        }

        public string LastName
        {
            get { return salary.LastName; }
        }

        public DateTime BirthDate
        {
            get { return salary.BirthDate; }
        }

        public double SalaryValue
        {
            get { return salary.SalaryValue; }
        }
    }
}