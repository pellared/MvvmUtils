using System;
using System.Collections.Generic;

using FluentValidation;
using FluentValidation.Results;

namespace Pellared.Infrastructure.Validation
{
    public static class FluentValidatorExtensions
    {
        public static void ObjectRule<T>(this AbstractValidator<T> validator, Func<T, string> customValidation)
        {
            validator.Custom(
                x =>
                {
                    string error = customValidation(x);
                    if (string.IsNullOrEmpty(error)) return null;
                    return new ValidationFailure(string.Empty, error);
                });
        }
    }
}