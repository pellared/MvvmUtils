using System;

namespace Pellared.Utils
{
    public class DisposableObject : IDisposable
    {
        private bool hasUnmanagedResources;
        private bool baseDisposeManagedResourcesCalled;
        private bool baseDisposeUnmanagedResourcesCalled;

        protected bool IsDisposed { get; private set; }
        protected bool IsDisposing { get; private set; }

        public DisposableObject()
        {
            GC.SuppressFinalize(this);
        }

        ~DisposableObject()
        {
            CallDispose(false);
        }

        public void Dispose()
        {
            CallDispose(true);
            if (hasUnmanagedResources)
            {
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void DisposeManagedResources()
        {
            if (!IsDisposing)
            {
                throw new InvalidOperationException("In order to dipose an object call the Dispose method. Do not call DisposeManagedResources method directly.");
            }
            
            baseDisposeManagedResourcesCalled = true;
        }

        protected virtual void DisposeUnmanagedResources()
        {
            if (!IsDisposing)
            {
                throw new InvalidOperationException("In order to dipose an object call the Dispose method. Do not call DisposeUnmanagedResources method directly.");
            }
            
            baseDisposeUnmanagedResourcesCalled = true;
        }

        protected void HasUnmanagedResources()
        {
            if (!hasUnmanagedResources)
            {
                hasUnmanagedResources = true;
                GC.ReRegisterForFinalize(this);
            }
        }

        protected void ThrowIfDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException("Object is already disposed.");
            }

            if (IsDisposing)
            {
                throw new ObjectDisposedException("Object is beeing disposed.");
            }
        }

        private void CallDispose(bool disposeManagedResources)
        {
            if (IsDisposed)
            {
                throw new InvalidOperationException("Dispose called on an object that is already disposed.");
                return;
            }

            if (IsDisposing)
            {
                throw new InvalidOperationException("Dispose called on an object that is currently disposing.");
                return;
            }

            IsDisposing = true;

            if (disposeManagedResources)
            {
                DisposeManagedResources();
                VerifyBaseDisposeManagedResourcesCalled();
            }

            if (hasUnmanagedResources)
            {
                DisposeUnmanagedResources();
                VerifyBaseDisposeUnmanagedResourcesCalled();
            }

            IsDisposed = true;
            IsDisposing = false;
        }

        private void VerifyBaseDisposeManagedResourcesCalled()
        {
            if (!baseDisposeManagedResourcesCalled)
            {
                throw new InvalidOperationException("DisposeManagedResources of the base class was not called.\n" + GetTypeString());
            }
        }

        private void VerifyBaseDisposeUnmanagedResourcesCalled()
        {
            if (!baseDisposeUnmanagedResourcesCalled)
            {
                throw new InvalidOperationException("DisposeUnmanagedResources of the base class was not called.\n" + GetTypeString());
            }
        }

        private string GetTypeString()
        {
            string typeName = GetType().FullName;
            return "Type: " + typeName;
        }
    }
}