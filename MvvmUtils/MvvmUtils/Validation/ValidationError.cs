using Pellared.Common;
using System;
using System.Linq.Expressions;
namespace Pellared.MvvmUtils.Validation
{
    public class ValidationError
    {
        public ValidationError(string propertyName, string errorMessage)
        {
            Ensure.NotNull(propertyName);
            Ensure.NotEmpty(errorMessage);

            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public static ValidationError Create<TProperty>(string propertyName, string errorMessage)
        {
            Ensure.NotNull(propertyName);
            Ensure.NotEmpty(errorMessage);

            return new ValidationError(propertyName, errorMessage);
        }

        public static ValidationError Create<TProperty>(Expression<Func<TProperty>> propertySelector, string errorMessage)
        {
            Ensure.NotNull(propertySelector);
            Ensure.NotEmpty(errorMessage);

            string propertyName = ExpressionUtils.GetPropertyName(propertySelector);
            return new ValidationError(propertyName, errorMessage);
        }

        public string PropertyName { get; private set; }

        public string ErrorMessage { get; private set; }

        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}