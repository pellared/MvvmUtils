using System;

namespace Pellared.Common
{
    public class Disposer : IDisposable
    {
        private readonly Action actionOnDispose;

        public Disposer(Action actionOnDispose)
        {
            Ensure.NotNull(actionOnDispose, "actionOnDispose");
            this.actionOnDispose = actionOnDispose;
        }

        public void Dispose()
        {
            actionOnDispose();
        }
    }
}