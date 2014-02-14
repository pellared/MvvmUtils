using System;
using System.Threading;

namespace Pellared.Common.Mvvm.Threading
{
    public class UiDispatcher : IUiDispatcher
    {
        private readonly static UiDispatcher current = new UiDispatcher();

        public static UiDispatcher Current
        {
            get { return current; }
        }

        private readonly SynchronizationContext synchronizationContext;

        public UiDispatcher()
        {
            synchronizationContext = SynchronizationContext.Current;
        }

        /// <summary>
        ///     Invokes an action asynchronously on the UI thread.
        /// </summary>
        /// <param name="action">The action that must be executed.</param>
        public void InvokeAsync(Action action)
        {
            synchronizationContext.Post(x => action(), null);
        }

        /// <summary>
        ///     Invokes an action asynchronously on the UI thread.
        /// </summary>
        /// <param name="action">The action that must be executed.</param>
        /// <param name="argument">Argument passed to the function.</param>
        public void InvokeAsync<T>(Action<T> action, T argument)
        {
            synchronizationContext.Post(x => action((T)x), argument);
        }

        /// <summary>
        ///     Executes an action on the UI thread. If this method is called
        ///     from the UI thread, the action is executed immendiately. If the
        ///     method is called from another thread, the action will be enqueued
        ///     on the UI thread's dispatcher and executed asynchronously.
        /// </summary>
        /// <param name="action">
        ///     The action that will be executed on the UI
        ///     thread.
        /// </param>
        public void Invoke(Action action)
        {
            if (synchronizationContext == SynchronizationContext.Current)
            {
                action();
            }
            else
            {
                InvokeAsync(action);
            }
        }

        /// <summary>
        ///     Executes an action on the UI thread. If this method is called
        ///     from the UI thread, the action is executed immendiately. If the
        ///     method is called from another thread, the action will be enqueued
        ///     on the UI thread's dispatcher and executed asynchronously.
        /// </summary>
        /// <param name="action">
        ///     The action that will be executed on the UI
        ///     thread.
        /// </param>
        /// <param name="argument">Argument passed to the function.</param>
        public void Invoke<T>(Action<T> action, T argument)
        {
            if (synchronizationContext == SynchronizationContext.Current)
            {
                action(argument);
            }
            else
            {
                InvokeAsync(action, argument);
            }
        }
    }
}