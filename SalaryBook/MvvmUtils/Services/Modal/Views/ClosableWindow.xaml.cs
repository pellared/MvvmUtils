using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Pellared.Utils.Mvvm.Services.Modal.Views
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ClosableWindow : Window
    {
        public ClosableWindow()
        {
            InitializeComponent();
        }

        public static void Open(IDialogViewModel viewModel, bool modal, bool canMinimize = false)
        {
            ClosableWindow dialog = CreateModalWindow(viewModel, canMinimize);
            ShowWindow(viewModel, dialog, modal);
        }

        public static void Open(IDialogViewModel viewModel, Window owner, bool modal, bool canMinimize = false)
        {
            ClosableWindow dialog = CreateModalWindow(viewModel, canMinimize);
            if (owner != null)
            {
                dialog.Owner = owner;
                dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }

            ShowWindow(viewModel, dialog, modal);
        }

        public static void Open(IDialogViewModel viewModel, Form owner, bool modal, bool canMinimize = false)
        {
            ClosableWindow dialog = CreateModalWindow(viewModel, canMinimize);
            if (owner != null)
            {
                WindowInteropHelper helper = new WindowInteropHelper(dialog);
                helper.Owner = owner.Handle;
                dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }

            ShowWindow(viewModel, dialog, modal);
        }

        private static ClosableWindow CreateModalWindow(IDialogViewModel viewModel, bool canMinimize)
        {
            ClosableWindow dialog = new ClosableWindow
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

        private static void ShowWindow(IDialogViewModel viewModel, ClosableWindow dialog, bool modal)
        {
            if (modal)
            {
                dialog.ShowDialog();
                viewModel.Closed = true;
            }
            else
            {
                dialog.Closed += (sender, args) => viewModel.Closed = true;
                dialog.Show();
            }
        }
    }
}