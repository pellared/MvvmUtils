using System;
using System.Collections.Generic;
using System.Linq;

using Pellared.Common;
using Pellared.Common.Mvvm.ViewModel;

namespace Pellared.Utils.Mvvm.Validation.Generic
{
    public class CompositeValidator<TObject, TError> : IValidator<TObject, TError>
        where TError : ValidationError
    {
        private readonly List<IValidator<TObject, TError>> validators;

        public CompositeValidator()
        {
            validators = new List<IValidator<TObject, TError>>();
        }

        public CompositeValidator(params IValidator<TObject, TError>[] validators)
        {
            this.validators = new List<IValidator<TObject, TError>>(validators);
        }

        public IEnumerable<IValidator<TObject, TError>> Validators
        {
            get { return validators; }
        }

        public void AddRule(Func<TObject, TError> validation)
        {
            var validator = new DelegateValidator<TObject, TError>(validation);
            validators.Add(validator);
        }

        public virtual IEnumerable<TError> Validate(TObject instance)
        {
            HashSet<TError> result = new HashSet<TError>();

            foreach (var validation in Validators)
            {
                var validationResults = validation.Validate(instance);
                if (validationResults != null)
                {
                    MergeErrors(validationResults, result);
                }
            }

            return result;
        }

        private static void MergeErrors(IEnumerable<TError> validationResults, HashSet<TError> result)
        {
            foreach (var validationResult in validationResults)
            {
                if (!validationResult.EqualsDefault())
                {
                    result.Add(validationResult);
                }
            }
        }
    }
}