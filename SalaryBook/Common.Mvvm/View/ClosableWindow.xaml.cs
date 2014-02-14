using System.Windows;

using Pellared.Common.Mvvm.Dialog;

namespace Pellared.Common.Mvvm.View
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

        public ClosableWindow(IDialogViewModel viewModel, System.Windows.ResizeMode resizeMode)
            : this()
        {
            Content = viewModel;
            DataContext = viewModel;
            ResizeMode = resizeMode;
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