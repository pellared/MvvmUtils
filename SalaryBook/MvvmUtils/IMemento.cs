namespace Pellared.MvvmUtils
{
    public interface IMemento<T> 
    {
        void Restore(T originator);
    }
}