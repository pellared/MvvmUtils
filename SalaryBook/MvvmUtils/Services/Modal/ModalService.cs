using Pellared.Utils.Mvvm.Services.Modal.Views;
using System.Windows;
using System.Windows.Forms;

namespace Pellared.Utils.Mvvm.Services.Modal
{
    public class ModalService : IModalService
    {
        private readonly Window ownerWindow;
        private readonly Form ownerForm;

        public ModalService()
        {
        }

        public ModalService(Window ownerWindow)
        {
            this.ownerWindow = ownerWindow;
        }

        public ModalService(Form ownerForm)
        {
            this.ownerForm = ownerForm;
        }

        public void Open(IWindowViewModel viewModel, bool canMinimize = false)
        {
            var window = CreateWindow(viewModel, canMinimize);
            ClosableWindow.ShowWindow(viewModel, window);
        }

        public void OpenModal(IDialogViewModel viewModel, bool canMinimize = false)
        {
            var window = CreateWindow(viewModel, canMinimize);
            ClosableWindow.ShowDialog(viewModel, window);
        }

        private ClosableWindow CreateWindow(IDialogViewModel viewModel, bool canMinimize)
        {
            viewModel.Closed = false;

            ClosableWindow window;
            if (ownerWindow != null)
            {
                window = ClosableWindow.CreateClosableWindow(viewModel, ownerWindow, canMinimize);
            }
            else if (ownerForm != null)
            {
                window = ClosableWindow.CreateClosableWindow(viewModel, ownerForm, canMinimize);
            }
            else
            {
                window = ClosableWindow.CreateClosableWindow(viewModel, canMinimize);
            }

            return window;
        }
    }
}