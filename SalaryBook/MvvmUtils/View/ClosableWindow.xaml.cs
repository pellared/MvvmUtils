using Pellared.Utils.Mvvm.Dialog;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Pellared.Utils.Mvvm.View
{
    /// <summary>
    /// Interaction logic for ModalWindow.xaml
    /// </summary>
    internal partial class ClosableWindow : Window
    {
        protected ClosableWindow()
        {
            InitializeComponent();
        }

        public ClosableWindow(IDialogViewModel viewModel)
            : this()
        {
            Content = viewModel;
            DataContext = viewModel;
        }

        public void OpenDialog()
        {
            var viewModel = DataContext as IDialogViewModel;
            if (viewModel != null)
            {
                ShowDialog();
                viewModel.Closed = true;
            }
        }
    }
}