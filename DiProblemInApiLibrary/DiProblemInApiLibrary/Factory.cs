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

    public class FuncFactory<T> : IFactory<T>
    {
        private readonly Func<IOwned<T>> creator;

        public FuncFactory(Func<IOwned<T>> creator)
        {
            this.creator = creator;
        }

        public IOwned<T> Create()
        {
            return creator();
        }
    }

    public class AutofacFactory<T> : IFactory<T>
    {
        private readonly Func<Autofac.Features.OwnedInstances.Owned<T>, IOwned<T>> ownedFactory;
        private readonly Func<Autofac.Features.OwnedInstances.Owned<T>> creator;

        public AutofacFactory(Func<Autofac.Features.OwnedInstances.Owned<T>, IOwned<T>> ownedFactory, Func<Autofac.Features.OwnedInstances.Owned<T>> creator)
        {
            this.ownedFactory = ownedFactory;
            this.creator = creator;
        }

        public IOwned<T> Create()
        {
            Autofac.Features.OwnedInstances.Owned<T> ownedInstance = creator();
            IOwned<T> result = ownedFactory(ownedInstance);
            return result;
        }
    }

    public class AutofacFactory<T, TOut> : IFactory<T, TOut>
    {
        private readonly Func<Autofac.Features.OwnedInstances.Owned<TOut>, IOwned<TOut>> ownedFactory;
        private readonly Func<T, Autofac.Features.OwnedInstances.Owned<TOut>> creator;

        public AutofacFactory(Func<Autofac.Features.OwnedInstances.Owned<TOut>, IOwned<TOut>> ownedFactory, Func<T, Autofac.Features.OwnedInstances.Owned<TOut>> creator)
        {
            this.ownedFactory = ownedFactory;
            this.creator = creator;
        }

        public IOwned<TOut> Create(T input)
        {
            Autofac.Features.OwnedInstances.Owned<TOut> ownedInstance = creator(input);
            IOwned<TOut> result = ownedFactory(ownedInstance);
            return result;
        }
    }
}
