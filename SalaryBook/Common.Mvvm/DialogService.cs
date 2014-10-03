using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

using Pellared.Common.Mvvm.View;

using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace Pellared.Common.Mvvm
{
    public enum ResizeMode
    {
        NoResize,
        CanMinimize,
        CanResize
    }

    public enum DialogButtons
    {
        OK,
        OKCancel,
        YesNo,
        YesNoCancel
    }

    public enum DialogIcons
    {
        None,
        Information,
        Question,
        Exclamation,
        Stop,
        Warning
    }

    public enum DialogResults
    {
        None,
        OK,
        Cancel,
        Yes,
        No
    }

    public interface IWindowService
    {
        void Show(IWindowViewModel viewModel, ResizeMode resizeMode);
    }

    public interface IDialogService
    {
        void ShowDialog(IDialogViewModel viewModel, ResizeMode resizeMode);

        string ShowOpenFileDialog(string filter);

        string ShowSaveFileDialog(string defaultExtension, string filter);

        void ShowMessage(string message, string caption, DialogIcons icon);

        DialogResults ShowMessage(string message, string caption, DialogIcons icon, DialogButtons buttons);
    }

    public interface IWindowViewModel
    {
        bool Closed { get; set; }
        string Title { get; }
    }

    public interface IDialogViewModel : IWindowViewModel
    {
    }

    public class DialogService : IDialogService, IWindowService
    {
        private readonly Window ownerWindow;
        private readonly Form ownerForm;

        public DialogService()
        {
        }

        public DialogService(Window ownerWindow)
        {
            this.ownerWindow = ownerWindow;
        }

        public DialogService(Form ownerForm)
        {
            this.ownerForm = ownerForm;
        }

        public void Show(IWindowViewModel viewModel, ResizeMode resizeMode)
        {
            viewModel.Closed = false;
            ClosableWindow window = CreateWindow(viewModel, resizeMode);
            window.Open();
        }

        public void ShowDialog(IDialogViewModel viewModel, ResizeMode resizeMode)
        {
            viewModel.Closed = false;
            ClosableWindow window = CreateWindow(viewModel, resizeMode);
            window.OpenDialog();
        }

        private ClosableWindow CreateWindow(IWindowViewModel viewModel, ResizeMode resizeMode)
        {
            System.Windows.ResizeMode mode = GetMode(resizeMode);
            var window = new ClosableWindow(viewModel, mode);
            if (ownerWindow != null)
            {
                window.Owner = ownerWindow;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else if (ownerForm != null)
            {
                var helper = new WindowInteropHelper(window);
                helper.Owner = ownerForm.Handle;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }

            return window;
        }

        private System.Windows.ResizeMode GetMode(ResizeMode resizeMode)
        {
            switch (resizeMode)
            {
                case ResizeMode.NoResize:
                    return System.Windows.ResizeMode.NoResize;
                case ResizeMode.CanMinimize:
                    return System.Windows.ResizeMode.CanMinimize;
                case ResizeMode.CanResize:
                    return System.Windows.ResizeMode.CanResize;
                default:
                    return System.Windows.ResizeMode.NoResize;
            }
        }

        public string ShowOpenFileDialog(string filter)
        {
            var dialog = new OpenFileDialog
            {
                Filter = filter,
                Multiselect = false
            };

            // Show open file dialog box
            bool? result = (ownerWindow != null) ? dialog.ShowDialog(ownerWindow) : dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                return dialog.FileName;
            }
            return null;
        }

        public string ShowSaveFileDialog(string defaultExtension, string filter)
        {
            var dialog = new SaveFileDialog
            {
                DefaultExt = defaultExtension,
                Filter = filter
            };

            // Show open file dialog box
            bool? result = (ownerWindow != null) ? dialog.ShowDialog(ownerWindow) : dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                return dialog.FileName;
            }
            return null;
        }

        /// <summary>
        ///     Shows a standard System.Windows.MessageBox using the parameters requested
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="caption">The heading to be displayed</param>
        /// <param name="icon">The icon to be displayed.</param>
        public void ShowMessage(string message, string caption, DialogIcons icon)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, GetImage(icon));
        }

        /// <summary>
        ///     Shows a standard System.Windows.MessageBox using the parameters requested
        ///     but will return a translated result to enable adhere to the IMessageBoxService
        ///     implementation required.
        ///     This abstraction allows for different frameworks to use the same ViewModels but supply
        ///     alternative implementations of core service interfaces
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="caption">The caption of the message box window</param>
        /// <param name="icon">The icon to be displayed.</param>
        /// <param name="button"></param>
        /// <returns>CustomDialogResults results to use</returns>
        public DialogResults ShowMessage(string message, string caption, DialogIcons icon, DialogButtons button)
        {
            MessageBoxResult result = MessageBox.Show(message, caption, GetButton(button), GetImage(icon));
            return GetResult(result);
        }

        /// <summary>
        ///     Translates a CustomDialogIcons into a standard WPF System.Windows.MessageBox MessageBoxImage.
        ///     This abstraction allows for different frameworks to use the same ViewModels but supply
        ///     alternative implementations of core service interfaces
        /// </summary>
        /// <param name="icon">The icon to be displayed.</param>
        /// <returns>A standard WPF System.Windows.MessageBox MessageBoxImage</returns>
        private MessageBoxImage GetImage(DialogIcons icon)
        {
            switch (icon)
            {
                default:
                    return MessageBoxImage.None;
                case DialogIcons.Information:
                    return MessageBoxImage.Information;
                case DialogIcons.Question:
                    return MessageBoxImage.Question;
                case DialogIcons.Exclamation:
                    return MessageBoxImage.Exclamation;
                case DialogIcons.Stop:
                    return MessageBoxImage.Stop;
                case DialogIcons.Warning:
                    return MessageBoxImage.Warning;
            }
        }

        /// <summary>
        ///     Translates a CustomDialogButtons into a standard WPF System.Windows.MessageBox MessageBoxButton.
        ///     This abstraction allows for different frameworks to use the same ViewModels but supply
        ///     alternative implementations of core service interfaces
        /// </summary>
        /// <param name="btn">The button type to be displayed.</param>
        /// <returns>A standard WPF System.Windows.MessageBox MessageBoxButton</returns>
        private MessageBoxButton GetButton(DialogButtons btn)
        {
            switch (btn)
            {
                default:
                    return MessageBoxButton.OK;
                case DialogButtons.OK:
                    return MessageBoxButton.OK;
                case DialogButtons.OKCancel:
                    return MessageBoxButton.OKCancel;
                case DialogButtons.YesNo:
                    return MessageBoxButton.YesNo;
                case DialogButtons.YesNoCancel:
                    return MessageBoxButton.YesNoCancel;
            }
        }

        /// <summary>
        ///     Translates a standard WPF System.Windows.MessageBox MessageBoxResult into a
        ///     CustomDialogIcons.
        ///     This abstraction allows for different frameworks to use the same ViewModels but supply
        ///     alternative implementations of core service interfaces
        /// </summary>
        /// <param name="result">The standard WPF System.Windows.MessageBox MessageBoxResult</param>
        /// <returns>CustomDialogResults results to use</returns>
        private DialogResults GetResult(MessageBoxResult result)
        {
            switch (result)
            {
                default:
                    return DialogResults.None;
                case MessageBoxResult.Cancel:
                    return DialogResults.Cancel;
                case MessageBoxResult.No:
                    return DialogResults.No;
                case MessageBoxResult.None:
                    return DialogResults.None;
                case MessageBoxResult.OK:
                    return DialogResults.OK;
                case MessageBoxResult.Yes:
                    return DialogResults.Yes;
            }
        }
    }
}