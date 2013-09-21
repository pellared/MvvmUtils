using System.ComponentModel;
using System.Windows.Input;

using GalaSoft.MvvmLight.Command;

using Pellared.SalaryBook.Services;
using System;

namespace Pellared.SalaryBook.ViewModels
{
    public class EditSalaryViewModel
    {
        private readonly INavigationService navigationService;
        private readonly EditableSalaryViewModel editableSalaryViewModel;

#if DEBUG
        /// <summary>
        /// Design-time constructor
        /// </summary>
        public EditSalaryViewModel()
        {
            editableSalaryViewModel = new EditableSalaryViewModel();
        }
#endif

        public EditSalaryViewModel(INavigationService navigationService,
                                   EditableSalaryViewModel editableSalary)
        {
            this.navigationService = navigationService;
            editableSalaryViewModel = editableSalary;

            editableSalary.BeginEdit();

            saveCommand = new RelayCommand(Save, CanSave);
            _cancelCommand = new RelayCommand(Cancel);

            EditableSalaryViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RefreshCommands();
        }

        private void RefreshCommands()
        {
            _cancelCommand.RaiseCanExecuteChanged();
        }

        public EditableSalaryViewModel EditableSalaryViewModel
        {
            get { return editableSalaryViewModel; }
        }

        #region Save command

        private readonly RelayCommand saveCommand;

        public ICommand SaveCommand
        {
            get { return saveCommand; }
        }

        private bool CanSave()
        {
            return !EditableSalaryViewModel.HasErrors;
        }

        private void Save()
        {
            editableSalaryViewModel.EndEdit();
            navigationService.NavigateToMainSalary();
        }

        #endregion

        #region Cancel command

        private readonly RelayCommand _cancelCommand;

        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
        }

        private void Cancel()
        {
            editableSalaryViewModel.CancelEdit();
            navigationService.NavigateToMainSalary();
        }

        #endregion
    }
}