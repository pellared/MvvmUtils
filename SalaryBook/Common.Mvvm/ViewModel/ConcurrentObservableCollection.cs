using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Windows.Threading;

namespace Pellared.Common.Mvvm.ViewModel
{
    public class ConcurrentObservableCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        ///     This private variable holds the flag to
        ///     turn on and off the collection changed notification.
        /// </summary>
        private bool suspendCollectionChangeNotification;

        /// <summary>
        ///     Initializes a new instance of the FastObservableCollection class.
        /// </summary>
        public ConcurrentObservableCollection()
            : base()
        {
            suspendCollectionChangeNotification = false;
        }

        /// <summary>
        ///     This event is overridden CollectionChanged event of the observable collection.
        /// </summary>
        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        ///     This method adds the given generic list of items
        ///     as a range into current collection by casting them as type T.
        ///     It then notifies once after all items are added.
        /// </summary>
        /// <param name="items">The source collection.</param>
        public void Add(IEnumerable<T> items)
        {
            Contract.Requires<ArgumentNullException>(items != null, "items");

            SuspendCollectionChangeNotification();
            try
            {
                foreach (T item in items)
                {
                    Add(item);
                }
            }
            finally
            {
                NotifyChanges();
            }
        }

        /// <summary>
        ///     This method removes the given generic list of items as a range
        ///     into current collection by casting them as type T.
        ///     It then notifies once after all items are removed.
        /// </summary>
        /// <param name="items">The source collection.</param>
        public void Remove(IEnumerable<T> items)
        {
            Contract.Requires<ArgumentNullException>(items != null, "items");

            SuspendCollectionChangeNotification();
            try
            {
                foreach (T item in items)
                {
                    Remove(item);
                }
            }
            finally
            {
                NotifyChanges();
            }
        }

        /// <summary>
        ///     Resumes collection changed notification.
        /// </summary>
        public void ResumeCollectionChangeNotification()
        {
            suspendCollectionChangeNotification = false;
        }

        /// <summary>
        ///     Suspends collection changed notification.
        /// </summary>
        public void SuspendCollectionChangeNotification()
        {
            suspendCollectionChangeNotification = true;
        }

        /// <summary>
        ///     Raises collection change event.
        /// </summary>
        public void NotifyChanges()
        {
            ResumeCollectionChangeNotification();
            var arg = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(arg);
        }

        /// <summary>
        ///     This collection changed event performs thread safe event raising.
        /// </summary>
        /// <param name="e">The event argument.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            // Recommended is to avoid reentry
            // in collection changed event while collection
            // is getting changed on other thread.
            using (BlockReentrancy())
            {
                if (!suspendCollectionChangeNotification)
                {
                    NotifyCollectionChangedEventHandler eventHandler = CollectionChanged;
                    if (eventHandler != null)
                    {
                        OnCollectionChanged(eventHandler, e);
                    }
                }
            }
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventHandler eventHandler, NotifyCollectionChangedEventArgs e)
        {
            // Walk through invocation list.
            Delegate[] delegates = eventHandler.GetInvocationList();
            foreach (NotifyCollectionChangedEventHandler handler in delegates)
            {
                // If the subscriber is a DispatcherObject and different thread.
                var dispatcherObject = handler.Target as DispatcherObject;
                if (dispatcherObject != null && !dispatcherObject.CheckAccess())
                {
                    // Invoke handler in the target dispatcher's thread...
                    // asynchronously for better responsiveness.
                    dispatcherObject.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, handler, this, e);
                }
                else
                {
                    // Execute handler as is.
                    handler(this, e);
                }
            }
        }
    }
}