using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Pellared.Utils.Mvvm.ViewModel
{
    public static class ObservableUtils
    {
        public static PropertyChangedEventArgs CreateArgs<T>(Expression<Func<T, object>> propertyExpression)
        {
            return new PropertyChangedEventArgs(ExpressionUtils.ExtractPropertyName(propertyExpression));
        }

        #region Extension methods for INotifyPropertyChanged

        public static void RaisePropertyChanged<TEntity, TProperty>(
            this TEntity entity,
            PropertyChangedEventHandler propertyChanged,
            string propertyName)
            where TEntity : INotifyPropertyChanged
        {
            if (propertyChanged != null)
            {
                propertyChanged(entity, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static void RaisePropertyChanged<TEntity, TProperty>(
            this TEntity entity,
            PropertyChangedEventHandler propertyChanged,
            params string[] propertyNames)
            where TEntity : INotifyPropertyChanged
        {
            foreach (var propertyName in propertyNames)
            {
                entity.RaisePropertyChanged<TEntity, TProperty>(propertyChanged, propertyName);
            }
        }

        public static void RaisePropertyChanged<TEntity, TProperty>(
            this TEntity entity,
            PropertyChangedEventHandler propertyChanged,
            Expression<Func<TProperty>> propertySelector)
            where TEntity : INotifyPropertyChanged
        {
            if (propertyChanged != null)
            {
                var propertyName = ExpressionUtils.ExtractPropertyName(propertySelector);
                propertyChanged(entity, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static void RaisePropertyChanged<TEntity, TProperty>(
            this TEntity entity,
            PropertyChangedEventHandler propertyChanged,
            params Expression<Func<TProperty>>[] propertySelectors)
            where TEntity : INotifyPropertyChanged
        {
            foreach (var propertySelector in propertySelectors)
            {
                entity.RaisePropertyChanged(propertyChanged, propertySelector);
            }
        }

        #endregion

        #region Extension methods for PropertyChangedEventHandler

        public static void Raise<T>(this PropertyChangedEventHandler handler, Expression<Func<T>> propertyExpression)
        {
            if (handler != null)
            {
                var body = propertyExpression.Body as MemberExpression;
                var expression = body.Expression as ConstantExpression;
                handler(expression.Value, new PropertyChangedEventArgs(body.Member.Name));
            }
        }

        public static void Raise<T>(this PropertyChangedEventHandler handler, params Expression<Func<T>>[] propertyExpressions)
        {
            foreach (var propertyExpression in propertyExpressions)
            {
                handler.Raise<T>(propertyExpression);
            }
        }

        #endregion
    }
}