using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Pellared.Utils.Mvvm.Dialog.Views
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    public partial class ClosableWindow : Window
    {
        public ClosableWindow()
        {
            InitializeComponent();

            Loaded += ClosableWindowLoaded;
        }

        private void ClosableWindowLoaded(object sender, RoutedEventArgs e)
        {
            var windowViewModel = DataContext as IWindowViewModel;
            if (windowViewModel != null)
            {
                windowViewModel.OnLoaded();
            }
        }

        public static ClosableWindow CreateClosableWindow(IDialogViewModel viewModel, bool canMinimize = false)
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

        public static ClosableWindow CreateClosableWindow(IDialogViewModel viewModel, Window owner, bool canMinimize = false)
        {
            ClosableWindow dialog = CreateClosableWindow(viewModel, canMinimize);
            if (owner != null)
            {
                dialog.Owner = owner;
                dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }

            return dialog;
        }

        public static ClosableWindow CreateClosableWindow(IDialogViewModel viewModel, Form owner, bool canMinimize = false)
        {
            ClosableWindow dialog = CreateClosableWindow(viewModel, canMinimize);
            if (owner != null)
            {
                WindowInteropHelper helper = new WindowInteropHelper(dialog);
                helper.Owner = owner.Handle;
                dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }

            return dialog;
        }

        public static void ShowDialog(IDialogViewModel viewModel, ClosableWindow dialog)
        {
            dialog.ShowDialog();
            viewModel.Closed = true;
        }

        public static void ShowWindow(IWindowViewModel viewModel, ClosableWindow dialog)
        {
            dialog.Closing += (sender, args) => viewModel.OnClosing(args);
            dialog.Closed += (sender, args) => viewModel.Closed = true;
            dialog.Show();
        }
    }
}