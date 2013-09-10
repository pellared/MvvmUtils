using System;

namespace Pellared.Utils.Mvvm.Threading
{
    public interface IUiDispatcher 
    {
        /// <summary>
        /// Invokes an action asynchronously on the UI thread.
        /// </summary>
        /// <param name="action">The action that must be executed.</param>
        void InvokeAsync(Action action);

        /// <summary>
        /// Invokes an action asynchronously on the UI thread.
        /// </summary>
        /// <param name="action">The action that must be executed.</param>
        /// <param name="argument">Argument passed to the function.</param>
        void InvokeAsync<T>(Action<T> action, T argument);

        /// <summary>
        /// Executes an action on the UI thread. If this method is called
        /// from the UI thread, the action is executed immendiately. If the
        /// method is called from another thread, the action will be enqueued
        /// on the UI thread's dispatcher and executed asynchronously.
        /// </summary>
        /// <param name="action">The action that will be executed on the UI
        /// thread.</param>
        void Invoke(Action action);

        /// <summary>
        /// Executes an action on the UI thread. If this method is called
        /// from the UI thread, the action is executed immendiately. If the
        /// method is called from another thread, the action will be enqueued
        /// on the UI thread's dispatcher and executed asynchronously.
        /// </summary>
        /// <param name="action">The action that will be executed on the UI
        /// thread.</param>
        /// <param name="argument">Argument passed to the function.</param>
        void Invoke<T>(Action<T> action, T argument);
    }
}