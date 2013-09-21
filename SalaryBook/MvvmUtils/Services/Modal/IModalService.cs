namespace Pellared.Utils.Mvvm.Services.Modal
{
    public interface IModalService
    {
        void Open(IWindowViewModel viewModel, bool canMinimize = false);
        void OpenModal(IDialogViewModel viewModel, bool canMinimize = false);
    }
}