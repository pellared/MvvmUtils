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

        public void Open(IModalViewModel viewModel, bool canMinimize = false)
        {
            if (ownerWindow != null)
            {
                ModalWindow.OpenModal(viewModel, ownerWindow, canMinimize);
            }
            else if (ownerForm != null)
            {
                ModalWindow.OpenModal(viewModel, ownerForm, canMinimize);
            }
            else
            {
                ModalWindow.OpenModal(viewModel, canMinimize);
            }
        }
    }
}