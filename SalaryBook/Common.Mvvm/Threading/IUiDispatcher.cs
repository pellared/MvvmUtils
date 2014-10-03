using System;

namespace Pellared.Common.Mvvm.Threading
{
    public interface IUiDispatcher
    {
        void InvokeAsync(Action action);

        void InvokeSync(Action action);
    }
}