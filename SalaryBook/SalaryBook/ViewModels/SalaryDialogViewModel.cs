using System;

using Pellared.SalaryBook.Entities;
using Pellared.Utils.Mvvm.Services.Modal;
using System.ComponentModel;
using Pellared.Utils.Mvvm.Services.Dialog;

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

        public void OnLoaded()
        {
        }

        public void OnClosing(CancelEventArgs args)
        {
            CustomDialogResults result = dialogService.ShowMessage("Czy na pewno zamknąć okno?", "Pytanie", CustomDialogIcons.Question, CustomDialogButtons.YesNo);
            if (result != CustomDialogResults.Yes)
            {
                args.Cancel = true;
            }
        }
    }
}