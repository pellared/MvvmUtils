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
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            if (hasUnmanagedResources)
            {
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void DisposeManagedResources()
        {
            if (!IsDisposing)
            {
                throw new InvalidOperationException("In order to dispose an object call the Dispose method. Do not call DisposeManagedResources method directly." + GetTypeString());
            }
            
            baseDisposeManagedResourcesCalled = true;
        }

        protected virtual void DisposeUnmanagedResources()
        {
            if (!IsDisposing)
            {
                throw new InvalidOperationException("In order to dispose an object call the Dispose method. Do not call DisposeUnmanagedResources method directly." + GetTypeString());
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
                throw new ObjectDisposedException("Object is already disposed." + GetTypeString());
            }

            if (IsDisposing)
            {
                throw new ObjectDisposedException("Object is being disposed." + GetTypeString());
            }
        }

        private void Dispose(bool disposeManagedResources)
        {
            if (IsDisposed)
            {
                throw new InvalidOperationException("Dispose called on an object that is already disposed." + GetTypeString());
            }

            if (IsDisposing)
            {
                throw new InvalidOperationException("Dispose called on an object that is currently disposing." + GetTypeString());
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
                throw new InvalidOperationException("DisposeManagedResources of the base class was not called." + GetTypeString());
            }
        }

        private void VerifyBaseDisposeUnmanagedResourcesCalled()
        {
            if (!baseDisposeUnmanagedResourcesCalled)
            {
                throw new InvalidOperationException("DisposeUnmanagedResources of the base class was not called." + GetTypeString());
            }
        }

        private string GetTypeString()
        {
            string typeName = GetType().FullName;
            return " Type: " + typeName;
        }
    }
}