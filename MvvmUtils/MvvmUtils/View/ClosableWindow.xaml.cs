using System.Windows;

namespace Pellared.MvvmUtils.View
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

        public ClosableWindow(IWindowViewModel viewModel, System.Windows.ResizeMode resizeMode)
            : this()
        {
            Content = viewModel;
            DataContext = viewModel;
            ResizeMode = resizeMode;
        }

        public void Open()
        {
            var viewModel = DataContext as IWindowViewModel;
            if (viewModel != null)
            {
                Show();
            }
        }

        public void OpenDialog()
        {
            var viewModel = DataContext as IWindowViewModel;
            if (viewModel != null)
            {
                ShowDialog();
                viewModel.Closed = true;
            }
        }
    }
}