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

        protected override IEnumerable<PropertyError> Validation()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return PropertyError.Create(() => Name, "Name cannot be empty");
            }

            return null;
        }
    }
}