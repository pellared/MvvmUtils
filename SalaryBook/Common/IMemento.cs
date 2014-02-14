namespace Pellared.Common
{
    public interface IMemento<T>
    {
        void Restore(T originator);
    }
}