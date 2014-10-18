using Pellared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

namespace Pellared.MvvmUtils
{
    /// <summary>
    /// Monitors the PropertyChanged event of an object that implements INotifyPropertyChanged,
    /// and executes callback methods (i.e. handlers) registered for properties of that object.
    /// </summary>
    /// <typeparam name="TPropertySource">The type of object to monitor for property changes.</typeparam>
    /// <remarks>Usage: http://joshsmithonwpf.wordpress.com/2009/07/11/one-way-to-avoid-messy-propertychanged-event-handling/ </remarks>
    public sealed class PropertyObserver<TPropertySource> : IWeakEventListener
        where TPropertySource : class, INotifyPropertyChanged
    {
        private readonly Dictionary<string, Action<TPropertySource>> propertyNameToHandlerMap;
        private readonly WeakReference propertySourceRef;

        /// <summary>
        /// Initializes a new instance of PropertyObserver, which
        /// observes the 'propertySource' object for property changes.
        /// </summary>
        /// <param name="propertySource">The object to monitor for property changes.</param>
        public PropertyObserver(TPropertySource propertySource)
        {
            Ensure.NotNull(propertySource, "propertySource");

            propertySourceRef = new WeakReference(propertySource);
            propertyNameToHandlerMap = new Dictionary<string, Action<TPropertySource>>();
        }


        /// <summary>
        /// Registers a callback to be invoked when the PropertyChanged event has been raised for the specified property.
        /// </summary>
        /// <param name="expression">A lambda expression like 'n => n.PropertyName'.</param>
        /// <param name="handler">The callback to invoke when the property has changed.</param>
        /// <returns>The object on which this method was invoked, to allow for multiple invocations chained together.</returns>
        public PropertyObserver<TPropertySource> RegisterHandler(
            Expression<Func<TPropertySource, object>> expression,
            Action<TPropertySource> handler)
        {
            Ensure.NotNull(expression, "expression");
            Ensure.NotNull(handler, "handler");

            string propertyName = ExpressionUtils.GetPropertyName(expression);
            TPropertySource propertySource = GetPropertySource();
            if (propertySource != null)
            {
                propertyNameToHandlerMap[propertyName] = handler;
                PropertyChangedEventManager.AddListener(propertySource, this, propertyName);
            }

            return this;
        }

        /// <summary>
        /// Removes the callback associated with the specified property.
        /// </summary>
        /// <param name="expression">A lambda expression like 'n => n.PropertyName'.</param>
        /// <returns>The object on which this method was invoked, to allow for multiple invocations chained together.</returns>
        public PropertyObserver<TPropertySource> UnregisterHandler(Expression<Func<TPropertySource, object>> expression)
        {
            Ensure.NotNull(expression, "expression");

            string propertyName = ExpressionUtils.GetPropertyName(expression);
            TPropertySource propertySource = GetPropertySource();
            if (propertySource != null)
            {
                if (propertyNameToHandlerMap.ContainsKey(propertyName))
                {
                    propertyNameToHandlerMap.Remove(propertyName);
                    PropertyChangedEventManager.RemoveListener(propertySource, this, propertyName);
                }
            }

            return this;
        }

        private TPropertySource GetPropertySource()
        {
            try
            {
                return (TPropertySource)propertySourceRef.Target;
            }
            catch
            {
                return default(TPropertySource);
            }
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(PropertyChangedEventManager))
            {
                string propertyName = ((PropertyChangedEventArgs)e).PropertyName;
                var propertySource = (TPropertySource)sender;

                if (propertyName.IsNullOrEmpty())
                {
                    // When the property name is empty, all properties are considered to be invalidated.
                    // Iterate over a copy of the list of handlers, in case a handler is registered by a callback.
                    foreach (Action<TPropertySource> handler in propertyNameToHandlerMap.Values.ToArray())
                    {
                        handler(propertySource);
                    }

                    return true;
                }
                else
                {
                    Action<TPropertySource> handler;
                    if (propertyNameToHandlerMap.TryGetValue(propertyName, out handler))
                    {
                        handler(propertySource);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}