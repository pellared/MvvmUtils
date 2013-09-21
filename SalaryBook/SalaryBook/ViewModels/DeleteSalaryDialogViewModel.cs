using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Pellared.SalaryBook.Entities;
using Pellared.SalaryBook.Properties;
using Pellared.Utils.Mvvm.Services.Dialog;

namespace Pellared.SalaryBook.ViewModels
{
    public class DeleteSalaryDialogViewModel : ViewModelBase, IDialogViewModel
    {
        private readonly Salary salary;

        public DeleteSalaryDialogViewModel(Salary salary)
        {
            this.salary = salary;

            deleteCommand = new RelayCommand(Delete);
            cancelCommand = new RelayCommand(Cancel);
        }

        public bool Result { get; private set; }

        public string Title
        {
            get { return Resources.DeleteSalaryDialogTitle; }
        }

        public string Message
        {
            get { return string.Format(Resources.DeleteSalaryQuestion, salary.FirstName, salary.LastName); }
        }

        private bool closed;

        public bool Closed
        {
            get { return closed; }
            set
            {
                if (value != closed)
                {
                    closed = value;
                    RaisePropertyChanged(() => Closed);
                }
            }
        }

        #region Delete command

        private readonly RelayCommand deleteCommand;

        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
        }

        private void Delete()
        {
            Result = true;
            Closed = true;
        }

        #endregion

        #region Cancel command

        private readonly RelayCommand cancelCommand;

        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }

        private void Cancel()
        {
            Result = false;
            Closed = true;
        }

        #endregion
    }
}