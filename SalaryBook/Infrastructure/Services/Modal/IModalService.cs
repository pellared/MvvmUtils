namespace Pellared.Infrastructure.Services.Modal
{
    public interface IModalService
    {
        void Open(IModalViewModel viewModel, bool canMinimize = false);
    }
}