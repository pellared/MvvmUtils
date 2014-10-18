using Pellared.MvvmUtils.Validation;
using System.Collections.Generic;

namespace MvvmLightSample.ViewModel
{
    public sealed class PersonViewModel : ValidatableViewModel
    {
        public PersonViewModel()
        {
            if (IsInDesignModeStatic)
            {
                Name = "Jon";
            }

            Validate();
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(() => Name, ref _name, value); }
        }

        protected override IEnumerable<ValidationError> Validation()
        {
            var errors = new List<ValidationError>();

            if (string.IsNullOrWhiteSpace(Name))
            {
                errors.Add(ValidationError.Create(() => Name, "Name cannot be empty"));
            }

            return errors;
        }
    }
}