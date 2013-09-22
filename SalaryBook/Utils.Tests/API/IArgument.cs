using System;
namespace Pellared.Utils
{
    public interface IArgument<T>
    {
        string Name { get; }
        T Value { get; }
    }
}
