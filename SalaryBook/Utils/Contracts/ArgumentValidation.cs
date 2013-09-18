using System;
using System.Collections.Generic;
using System.Linq;

namespace Pellared.Utils.Contracts
{
    public class ArgumentValidation<T>
    {
        public Argument<T> Argument { get; private set; }

        public ArgumentValidation(Argument<T> argument)
        {
            Argument = argument;
        }

        public ArgumentValidation<T> Is<TException>(Predicate<T> predicate, params object[] parameters)
            where TException : ArgumentException
        {
            if (!predicate(Argument.Value))
            {
                var types = new List<Type>();
                var args = new List<object>();
                foreach (object p in parameters ?? Enumerable.Empty<object>())
                {
                    types.Add(p.GetType());
                    args.Add(p);
                }

                Type exeptionType = typeof(TException);
                var constructor = exeptionType.GetConstructor(types.ToArray());
                if (constructor == null)
                {
                    throw new ArgumentException("There is no constructor for " + exeptionType.FullName + " that has given parameters.", "parameters");
                }
                
                var exception = constructor.Invoke(args.ToArray()) as TException;
                throw exception;
            }

            return this;
        }

        public ArgumentValidation<T> Is(Predicate<T> predicate, string exceptionMessage = "")
        {
            if (!predicate(Argument.Value))
            {
                throw new ArgumentException(exceptionMessage, Argument.Name);
            }

            return this;
        }
    }
}