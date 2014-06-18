using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiProblemInApiLibrary
{
    interface IFactory<out T>
    {
        IOwned<T> Create();
    }

    interface IFactory<in T, out TOut>
    {
        IOwned<TOut> Create(T input);
    }

    public class Factory<T> : IFactory<T>
    {
        private readonly Func<IOwned<T>> creator;

        public Factory(Func<IOwned<T>> creator)
        {
            this.creator = creator;
        }

        public IOwned<T> Create()
        {
            return creator();
        }
    }

    public class Factory<T, TOut> : IFactory<T, TOut>
    {
        private readonly Func<T, IOwned<TOut>> creator;

        public Factory(Func<T, IOwned<TOut>> creator)
        {
            this.creator = creator;
        }

        public IOwned<TOut> Create(T input)
        {
            return creator(input);
        }
    }
}
