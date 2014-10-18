using Pellared.MvvmUtils;
using Pellared.SalaryBook.Entities;
using System;

namespace Pellared.SalaryBook.ViewModels
{
    public class SalaryDialogViewModel : IWindowViewModel
    {
        private readonly Salary salary;
        private readonly IDialogService dialogService;

        public SalaryDialogViewModel(Salary salary, IDialogService dialogService)
        {
            this.salary = salary;
            this.dialogService = dialogService;
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