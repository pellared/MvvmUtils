using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace Pellared.MvvmUtils.ViewModel
{
    /// <summary>
    /// Manages validation errors for an object, notifying when the error state changes.
    /// </summary>
    /// <typeparam name="T">The type of the error object.</typeparam>
    public class ErrorsContainer<T> : IErrorsContainer<T>
    {
        private static readonly T[] NoErrors = new T[0];
        private readonly Action<string> raiseErrorsChanged;
        private Dictionary<string, List<T>> validationResults;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorsContainer{T}"/> class.
        /// </summary>
        /// <param name="raiseErrorsChanged">The action that invoked if when errors are added for an object./>
        /// event.</param>
        public ErrorsContainer(Action<string> raiseErrorsChanged)
        {
            if (raiseErrorsChanged == null)
                throw new ArgumentNullException("raiseErrorsChanged");

            this.raiseErrorsChanged = raiseErrorsChanged;
            validationResults = new Dictionary<string, List<T>>();
        }

        /// <summary>
        /// Gets a value indicating whether the object has validation errors. 
        /// </summary>
        public bool HasErrors
        {
            get { return validationResults.Count != 0; }
        }

        /// <summary>
        /// Gets the validation errors for a specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The validation errors of type <typeparamref name="T"/> for the property.</returns>
        public IEnumerable<T> GetErrors(string propertyName)
        {
            var localPropertyName = propertyName ?? string.Empty;
            List<T> currentValidationResults;
            if (validationResults.TryGetValue(localPropertyName, out currentValidationResults))
                return currentValidationResults;
            else
                return NoErrors;
        }

        /// <summary>
        /// Clears the errors for all properties.
        /// </summary>
        public void ClearAllErrors()
        {
            IEnumerable<string> propertyNames = validationResults.Select(x => x.Key);
            validationResults = new Dictionary<string, List<T>>();
            foreach (string propertyName in propertyNames)
            {
                raiseErrorsChanged(propertyName);
            }
        }

        /// <summary>
        /// Clears the errors for the property indicated by the property expression.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <param name="propertyExpression">The expression indicating a property.</param>
        /// <example>
        ///     container.ClearErrors(()=>SomeProperty);
        /// </example>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public void ClearErrors<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            var propertyName = ReflectionUtils.ExtractPropertyName(propertyExpression);
            ClearErrors(propertyName);
        }

        /// <summary>
        /// Clears the errors for a property.
        /// </summary>
        /// <param name="propertyName">The name of th property for which to clear errors.</param>
        /// <example>
        ///     container.ClearErrors("SomeProperty");
        /// </example>
        public void ClearErrors(string propertyName)
        {
            SetErrors(propertyName, new List<T>());
        }

        /// <summary>
        /// Sets the validation errors for the specified property.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <typeparam name="TProperty">The property type for which to set errors.</typeparam>
        /// <param name="propertyExpression">The <see cref="Expression"/> indicating the property.</param>
        /// <param name="propertyErrors">The list of errors to set for the property.</param>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public void SetErrors<TEntity, TProperty>(
                Expression<Func<TEntity, TProperty>> propertyExpression, IEnumerable<T> propertyErrors)
        {
            var propertyName = ReflectionUtils.ExtractPropertyName(propertyExpression);
            SetErrors(propertyName, propertyErrors);
        }

        /// <summary>
        /// Sets the validation errors for the specified property.
        /// </summary>
        /// <remarks>
        /// If a change is detected then the errors changed event is raised.
        /// </remarks>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="newValidationResults">The new validation errors.</param>
        public void SetErrors(string propertyName, IEnumerable<T> newValidationResults)
        {
            var localPropertyName = propertyName ?? string.Empty;
            var hasCurrentValidationResults = validationResults.ContainsKey(localPropertyName);
            var hasNewValidationResults = newValidationResults != null && newValidationResults.Count() > 0;

            if (hasCurrentValidationResults || hasNewValidationResults)
                if (hasNewValidationResults)
                {
                    validationResults[localPropertyName] = new List<T>(newValidationResults);
                    raiseErrorsChanged(localPropertyName);
                }
                else
                {
                    validationResults.Remove(localPropertyName);
                    raiseErrorsChanged(localPropertyName);
                }
        }
    }
}