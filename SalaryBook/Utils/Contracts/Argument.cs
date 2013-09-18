namespace Pellared.Utils.Contracts
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
    }
}