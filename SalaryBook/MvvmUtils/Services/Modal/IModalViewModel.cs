namespace Pellared.MvvmUtils.Services.Modal
{
    public interface IModalViewModel
    {
        bool Closed { get; set; }
        string Title { get; }
    }
}