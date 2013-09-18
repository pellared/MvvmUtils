using System;

namespace Pellared.Utils.Contracts
{
    public static partial class ArgumentValidationExtensions
    {
        public static ArgumentValidation<T> IsNotNull<T>(this ArgumentValidation<T> validation)
            where T : class
        {
            if (validation.Argument.Value == null)
            {
                throw new ArgumentNullException(validation.Argument.Name);
            }

            return validation;
        }
    }
}