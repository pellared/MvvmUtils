using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiProblemInApiLibrary
{
    public interface IOwned<out T> : IDisposable
    {
        T Value { get; }
    }

    public class Owned<T> : IOwned<T> 
    {
        public Owned(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }

        public void Dispose()
        {
            IDisposable disposable = Value as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }

    public class AutofacOwned<T> : IOwned<T>
    {
        private readonly Autofac.Features.OwnedInstances.Owned<T> owned;

        public AutofacOwned(Autofac.Features.OwnedInstances.Owned<T> owned)
        {
            this.owned = owned;
        }

        public T Value
        {
            get { return owned.Value; }
        }

        public void Dispose()
        {
            owned.Dispose();
        }
    }
}
