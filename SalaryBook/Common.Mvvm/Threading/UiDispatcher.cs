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
        /// Executes an action on the UI thread. If this method is called
        /// from the UI thread, the action is executed immendiately. If the
        /// method is called from another thread, the action will be enqueued
        /// on the UI thread's dispatcher and executed asynchronously.
        /// </summary>
        /// <param name="action">
        /// The action that will be executed on the UI
        /// thread.
        /// </param>
        public void InvokeAsync(Action action)
        {
            if (synchronizationContext == SynchronizationContext.Current)
            {
                action();
            }
            else
            {
                synchronizationContext.Post(x => action(), null);
            }
        }

        /// <summary>
        /// Executes an action on the UI thread. If this method is called
        /// from the UI thread, the action is executed immendiately. If the
        /// method is called from another thread, the action will be
        /// executed synchronously.
        /// </summary>
        /// <param name="action">
        /// The action that will be executed on the UI
        /// thread.
        /// </param>
        public void InvokeSync(Action action)
        {
            if (synchronizationContext == SynchronizationContext.Current)
            {
                action();
            }
            else
            {
                synchronizationContext.Send(x => action(), null);
            }
        }

    }
}