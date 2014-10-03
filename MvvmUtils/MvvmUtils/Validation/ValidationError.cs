namespace Pellared.MvvmUtils.Validation
{
    public class ValidationError
    {
        public ValidationError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public string PropertyName { get; private set; }

        public string ErrorMessage { get; private set; }

        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}