namespace Pellared.Infrastructure.Services.Modal
{
    public interface IModalViewModel
    {
        bool Closed { get; set; }
        string Title { get; }
    }
}