using Pellared.Common.Conditions;
using System;

namespace Pellared.Common
{
    public sealed class DisposeAction : IDisposable
    {
        private readonly Action actionOnDispose;

        public DisposeAction(Action actionOnDispose)
        {
            Throw.IfNull(actionOnDispose, "actionOnDispose");
            this.actionOnDispose = actionOnDispose;
        }

        public void Dispose()
        {
            actionOnDispose();
        }
    }
}