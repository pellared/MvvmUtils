using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

using AutoMapper;

using Pellared.SalaryBook.Common;
using Pellared.SalaryBook.Entities;
using Pellared.SalaryBook.Validators;
using Pellared.Utils;
using Pellared.Utils.Mvvm.Validation;
using Pellared.Utils.Mvvm.ViewModel;

namespace Pellared.SalaryBook.ViewModels
{
    public class EditableSalaryViewModel : ValidatableViewModel, IEditableObject, ISalary
    {
        private readonly IValidator<ISalary> salaryValidator;
        private Memento<EditableSalaryViewModel> memento;

        private readonly ErrorsContainer<ValidationError> secondPhaseErrorsContainer;
        private readonly ValidationProvider secondPhaseValidationProvider;

#if DEBUG
        /// <summary>
        /// Design-time contructor
        /// </summary>
        public EditableSalaryViewModel()
        {
            if (IsInDesignMode)
            {
                FirstName = "Jan";
                LastName = "Kowalski";
                BirthDate = DateTime.Today.AddYears(-20);
                SalaryValue = 1000;
            }
        }
#endif
        public EditableSalaryViewModel(IValidator<ISalary> salaryValidator)
        {
            this.salaryValidator = salaryValidator;

            secondPhaseErrorsContainer = new ErrorsContainer<ValidationError>(OnErrorsChanged);
            secondPhaseValidationProvider = new ValidationProvider(secondPhaseErrorsContainer, SecondPhaseValidation);
            DataErrorInfoProvider.AddErrorsContainer(secondPhaseErrorsContainer);
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    RaisePropertyChanged(() => FirstName);
                }
            }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value != lastName)
                {
                    lastName = value;
                    RaisePropertyChanged(() => LastName);
                }
            }
        }

        private DateTime? birthDate;

        public DateTime? BirthDate
        {
            get { return birthDate; }
            set
            {
                if (value != birthDate)
                {
                    birthDate = value;

                    if (birthDate > DateTime.Today)
                        birthDate = DateTime.Today;

                    RaisePropertyChanged(() => BirthDate);
                }
            }
        }

        private double salaryValue;

        public double SalaryValue
        {
            get { return salaryValue; }
            set
            {
                if (Math.Abs(value - salaryValue) > double.Epsilon)
                {
                    salaryValue = Math.Round(value, 2);
                    RaisePropertyChanged(() => SalaryValue);
                }
            }
        }

        public void BeginEdit()
        {
            memento = new Memento<EditableSalaryViewModel>(this);
        }

        public void CancelEdit()
        {
            memento.Restore(this);
            memento = null;
        }

        public void EndEdit()
        {
            memento = null;
        }

        public void Clear()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            BirthDate = null;
            SalaryValue = 0;
        }

        public void LoadEntity(Salary salary)
        {
            Mapper.DynamicMap(salary, this);
        }

        public Salary CreateEntity()
        {
            Salary result = Mapper.DynamicMap<Salary>(this);
            return result;
        }

        public void ValidateAll()
        {
            base.Validate();
            ValidateSecondPhase();
        }

        public Task ValidateAllAsync()
        {
            return Task.Run(() => ValidateAll());
        }

        public override void Validate()
        {
            Task.Run(() => base.Validate());
        }

        public void ValidateSecondPhase()
        {
            secondPhaseValidationProvider.Validate();
        }

        protected override IEnumerable<ValidationError> Validation()
        {
            return salaryValidator.Validate(this);
        }

        private IEnumerable<ValidationError> SecondPhaseValidation()
        {
            SalaryComplexValidator complexValidator = new SalaryComplexValidator();
            return complexValidator.Validate(this);
        }
    }
}