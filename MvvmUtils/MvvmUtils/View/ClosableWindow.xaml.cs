using Pellared.Common;
using System.Windows;

namespace Pellared.MvvmUtils.View
{
    internal partial class ClosableWindow : Window
    {
        protected ClosableWindow()
        {
            InitializeComponent();
        }

        public ClosableWindow(IWindowViewModel viewModel, System.Windows.ResizeMode resizeMode)
            : this()
        {
            Ensure.NotNull(viewModel, "viewModel");

            ViewModel = viewModel;
            ResizeMode = resizeMode;
        }

        public IWindowViewModel ViewModel
        {
            get { return DataContext as IWindowViewModel; }
            private set 
            {
                Content = value;
                DataContext = value; 
            }
        }

        public void Open()
        {
            if (ViewModel != null)
            {
                Show();
            }
        }

        public void OpenDialog()
        {
            if (ViewModel != null)
            {
                ShowDialog();
                ViewModel.Closed = true;
            }
        }
    }
}