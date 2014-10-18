using Pellared.Common;
using Pellared.MvvmUtils.View;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace Pellared.MvvmUtils
{
    

    public enum DialogButtons
    {
        OK,
        OKCancel,
        YesNo,
        YesNoCancel
    }

    public enum DialogIcon
    {
        None,
        Information,
        Question,
        Exclamation,
        Stop,
        Warning
    }

    public enum DialogResult
    {
        None,
        OK,
        Cancel,
        Yes,
        No
    }

    public interface IDialogService
    {
        string ShowOpenFileDialog(string filter);

        string ShowSaveFileDialog(string defaultExtension, string filter);

        DialogResult ShowMessage(string message, string caption, DialogIcon icon, DialogButtons buttons);
    }

    public interface IWindowViewModel
    {
        bool Closed { get; set; }

        string Title { get; }
    }

    public class DialogService : IDialogService
    {
        private readonly Window ownerWindow;

        public DialogService()
        {
        }

        public DialogService(Window ownerWindow)
        {
            Ensure.NotNull(ownerWindow, "ownerWindow");
            this.ownerWindow = ownerWindow;
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

        public DialogResult ShowMessage(string message, string caption, DialogIcon icon, DialogButtons buttons)
        {
            MessageBoxImage dialogImage = GetImage(icon);
            MessageBoxButton dialogButtons = GetButtons(buttons);
            MessageBoxResult result = (ownerWindow != null)
                ? MessageBox.Show(ownerWindow, message, caption, dialogButtons, dialogImage)
                : MessageBox.Show(message, caption, dialogButtons, dialogImage);
            return GetResult(result);
        }

        /// <summary>
        ///     Translates a CustomDialogIcons into a standard WPF System.Windows.MessageBox MessageBoxImage.
        ///     This abstraction allows for different frameworks to use the same ViewModels but supply
        ///     alternative implementations of core service interfaces
        /// </summary>
        /// <param name="icon">The icon to be displayed.</param>
        /// <returns>A standard WPF System.Windows.MessageBox MessageBoxImage</returns>
        private MessageBoxImage GetImage(DialogIcon icon)
        {
            switch (icon)
            {
                default:
                    return MessageBoxImage.None;

                case DialogIcon.Information:
                    return MessageBoxImage.Information;

                case DialogIcon.Question:
                    return MessageBoxImage.Question;

                case DialogIcon.Exclamation:
                    return MessageBoxImage.Exclamation;

                case DialogIcon.Stop:
                    return MessageBoxImage.Stop;

                case DialogIcon.Warning:
                    return MessageBoxImage.Warning;
            }
        }

        /// <summary>
        ///     Translates a CustomDialogButtons into a standard WPF System.Windows.MessageBox MessageBoxButton.
        ///     This abstraction allows for different frameworks to use the same ViewModels but supply
        ///     alternative implementations of core service interfaces
        /// </summary>
        /// <param name="btn">The buttons type to be displayed.</param>
        /// <returns>A standard WPF System.Windows.MessageBox MessageBoxButton</returns>
        private MessageBoxButton GetButtons(DialogButtons btn)
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
        private DialogResult GetResult(MessageBoxResult result)
        {
            switch (result)
            {
                default:
                    return DialogResult.None;

                case MessageBoxResult.Cancel:
                    return DialogResult.Cancel;

                case MessageBoxResult.No:
                    return DialogResult.No;

                case MessageBoxResult.None:
                    return DialogResult.None;

                case MessageBoxResult.OK:
                    return DialogResult.OK;

                case MessageBoxResult.Yes:
                    return DialogResult.Yes;
            }
        }
    }

    public static class DialogServiceExtensions
    {
        public static void ShowMessage(this IDialogService dialogService, string message, string caption, DialogIcon icon)
        {
            dialogService.ShowMessage(message, caption, icon, DialogButtons.OK);
        }
    }
}