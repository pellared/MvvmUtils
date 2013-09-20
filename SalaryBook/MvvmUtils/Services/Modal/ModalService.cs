using System.Windows;
using System.Windows.Forms;

using Pellared.Utils.Mvvm.Services.Modal.Views;

namespace Pellared.Utils.Mvvm.Services.Modal
{
    public class ModalService : IModalService
    {
        private readonly Window ownerWindow;
        private readonly Form ownerForm;

        public ModalService() {}

        public ModalService(Window ownerWindow)
        {
            this.ownerWindow = ownerWindow;
        }

        public ModalService(Form ownerForm)
        {
            this.ownerForm = ownerForm;
        }

        public void Open(IDialogViewModel viewModel, bool canMinimize = false)
        {
            viewModel.Closed = false;
            if (ownerWindow != null)
            {
                ClosableWindow.Open(viewModel, ownerWindow, false, canMinimize);
            }
            else if (ownerForm != null)
            {
                ClosableWindow.Open(viewModel, ownerForm, false, canMinimize);
            }
            else
            {
                ClosableWindow.Open(viewModel, false, canMinimize);
            }
        }

        public void OpenModal(IDialogViewModel viewModel, bool canMinimize = false)
        {
            viewModel.Closed = false;
            if (ownerWindow != null)
            {
                ClosableWindow.Open(viewModel, ownerWindow, true, canMinimize);
            }
            else if (ownerForm != null)
            {
                ClosableWindow.Open(viewModel, ownerForm, true, canMinimize);
            }
            else
            {
                ClosableWindow.Open(viewModel, true, canMinimize);
            }
        }
    }
}