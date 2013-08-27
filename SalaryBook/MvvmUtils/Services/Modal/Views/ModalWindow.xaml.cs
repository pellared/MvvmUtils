using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Pellared.MvvmUtils.Services.Modal.Views
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        public ModalWindow()
        {
            InitializeComponent();
        }

        public static void OpenModal(IModalViewModel viewModel, bool canMinimize = false)
        {
            ModalWindow dialog = CreateModalWindow(viewModel, canMinimize);
            ShowModalWindow(viewModel, dialog);
        }

        public static void OpenModal(IModalViewModel viewModel, Window owner, bool canMinimize = false)
        {
            ModalWindow dialog = CreateModalWindow(viewModel, canMinimize);
            if (owner != null)
            {
                dialog.Owner = owner;
                dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }

            ShowModalWindow(viewModel, dialog);
        }

        public static void OpenModal(IModalViewModel viewModel, Form owner, bool canMinimize = false)
        {
            ModalWindow dialog = CreateModalWindow(viewModel, canMinimize);
            if (owner != null)
            {
                WindowInteropHelper helper = new WindowInteropHelper(dialog);
                helper.Owner = owner.Handle;
                dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }

            ShowModalWindow(viewModel, dialog);
        }

        private static ModalWindow CreateModalWindow(IModalViewModel viewModel, bool canMinimize)
        {
            ModalWindow dialog = new ModalWindow
            {
                    Content = viewModel,
                    DataContext = viewModel,
            };

            if (canMinimize)
            {
                dialog.ResizeMode = ResizeMode.CanMinimize;
            }

            return dialog;
        }

        private static void ShowModalWindow(IModalViewModel viewModel, ModalWindow dialog)
        {
            dialog.ShowDialog();
            viewModel.Closed = true;
        }
    }
}