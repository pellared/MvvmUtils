using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.Common.Mvvm.ViewModel
{
    public static class ErrorContainerExtensions
    {
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static void ClearErrors<TError, TEntity, TProperty>(
            this IErrorsContainer<TError> errorContainer, Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            string propertyName = ExpressionUtils.GetPropertyName(propertyExpression);
            errorContainer.ClearErrors(propertyName);
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static void SetErrors<T, TEntity, TProperty>(
            this IErrorsContainer<T> errorContainer, Expression<Func<TEntity, TProperty>> propertyExpression, IEnumerable<T> propertyErrors)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }
            if (propertyErrors == null)
            {
                throw new ArgumentNullException("propertyErrors");
            }

            string propertyName = ExpressionUtils.GetPropertyName(propertyExpression);
            errorContainer.SetErrors(propertyName, propertyErrors);
        }
    }
}
