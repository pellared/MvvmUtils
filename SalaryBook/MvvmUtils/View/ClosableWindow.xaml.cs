﻿using System.Windows;

using Pellared.Utils.Mvvm.Dialog;

namespace Pellared.Utils.Mvvm.View
{
    /// <summary>
    ///     Interaction logic for ModalWindow.xaml
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