using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Pellared.Common
{
    public class Disposable : IDisposable
    {
        private const int DisposedFlag = 1;

        private readonly StackTrace creationStackTrace;

        private int _isDisposed;

        public Disposable(bool withFinalizer = false)
        {
#if DEBUG
            creationStackTrace = new StackTrace();
#else
            if (!withFinalizer)
            {
                GC.SuppressFinalize(this);
            }
#endif
        }

        ~Disposable()
        {
            DisposeUnmanaged();
#if DEBUG
            Debug.Fail(GetType() + " in not disposed" + Environment.NewLine + creationStackTrace);
#endif
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Dispose is implemented correctly")]
        public void Dispose()
        {
            int wasDisposed = Interlocked.Exchange(ref _isDisposed, DisposedFlag);
            if (wasDisposed != DisposedFlag)
            {
                DisposeResources();
            }
        }

        protected virtual void DisposeManaged()
        {
        }

        protected virtual void DisposeUnmanaged()
        {
        }

        public bool IsDisposed
        {
            get
            {
                Thread.MemoryBarrier();
                return _isDisposed == DisposedFlag;
            }
        }

        protected void EnsureNotDisposed()
        {
            if (IsDisposed)
            {
                string typeName = GetType().FullName;
                throw new ObjectDisposedException(typeName);
            }
        }

        private void DisposeResources()
        {
            DisposeManaged();
            DisposeUnmanaged();
            GC.SuppressFinalize(this);
        }
    }
}