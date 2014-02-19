using System;

namespace Pellared.Common
{
    public sealed class DisposeAction : IDisposable
    {
        private readonly Action actionOnDispose;

        public DisposeAction(Action actionOnDispose)
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