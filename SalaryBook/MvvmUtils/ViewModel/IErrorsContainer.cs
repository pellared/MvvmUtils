using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Pellared.Utils.Mvvm.ViewModel
{
    public interface IErrorsContainer<T>
    {
        /// <summary>
        ///     Gets a value indicating whether the object has validation errors.
        /// </summary>
        bool HasErrors { get; }

        /// <summary>
        ///     Gets the validation errors for a specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>
        ///     The validation errors of type <typeparamref name="T" /> for the property.
        /// </returns>
        IEnumerable<T> GetErrors(string propertyName);

        /// <summary>
        ///     Clears the errors for all properties.
        /// </summary>
        void ClearAllErrors();

        /// <summary>
        ///     Clears the errors for the property indicated by the property expression.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <typeparam name="TProperty">The property type.</typeparam>
        /// <param name="propertyExpression">The expression indicating a property.</param>
        /// <example>
        ///     container.ClearErrors(()=>SomeProperty);
        /// </example>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        void ClearErrors<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression);

        /// <summary>
        ///     Clears the errors for a property.
        /// </summary>
        /// <param name="propertyName">The name of th property for which to clear errors.</param>
        /// <example>
        ///     container.ClearErrors("SomeProperty");
        /// </example>
        void ClearErrors(string propertyName);

        /// <summary>
        ///     Sets the validation errors for the specified property.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <typeparam name="TProperty">The property type for which to set errors.</typeparam>
        /// <param name="propertyExpression">
        ///     The <see cref="Expression" /> indicating the property.
        /// </param>
        /// <param name="propertyErrors">The list of errors to set for the property.</param>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        void SetErrors<TEntity, TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression, IEnumerable<T> propertyErrors);

        /// <summary>
        ///     Sets the validation errors for the specified property.
        /// </summary>
        /// <remarks>
        ///     If a change is detected then the errors changed event is raised.
        /// </remarks>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="newValidationResults">The new validation errors.</param>
        void SetErrors(string propertyName, IEnumerable<T> newValidationResults);
    }
}