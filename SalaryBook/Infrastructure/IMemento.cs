namespace Pellared.Infrastructure
{
    public interface IMemento<T> {
        void Restore(T originator);
    }
}