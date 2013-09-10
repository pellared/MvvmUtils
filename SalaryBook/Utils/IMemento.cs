namespace Pellared.Utils
{
    public interface IMemento<T> 
    {
        void Restore(T originator);
    }
}