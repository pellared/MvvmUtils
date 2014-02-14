using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Pellared.Common.Conditions
{
    public class Argument<T>
    {
        public Argument(T value, string name)
        {
            Value = value;
            Name = name;
        }

        public T Value { get; private set; }
        public string Name { get; private set; }

        public static implicit operator T(Argument<T> arg)
        {
            return arg.Value;
        }
    }
}