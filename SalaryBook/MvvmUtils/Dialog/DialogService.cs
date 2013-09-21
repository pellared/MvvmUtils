using Microsoft.Win32;
using Pellared.Utils.Mvvm.Dialog.Views;
using System;
using System.Diagnostics.Contracts;
using System.Windows;

namespace Pellared.Utils.Mvvm.Dialog
{
    public class DialogService : IDialogService
    {
        private readonly Window ownerWindow;
        private readonly System.Windows.Forms.Form ownerForm;

        public DialogService()
        {
        }

        public DialogService(Window ownerWindow)
        {
            this.ownerWindow = ownerWindow;
        }

        public DialogService(System.Windows.Forms.Form ownerForm)
        {
            this.ownerForm = ownerForm;
        }

        public void Open(IWindowViewModel viewModel, bool canMinimize = false)
        {
            var window = CreateWindow(viewModel, canMinimize);
            ClosableWindow.ShowWindow(viewModel, window);
        }

        public void OpenModal(IDialogViewModel viewModel, bool canMinimize = false)
        {
            var window = CreateWindow(viewModel, canMinimize);
            ClosableWindow.ShowDialog(viewModel, window);
        }

        private ClosableWindow CreateWindow(IDialogViewModel viewModel, bool canMinimize)
        {
            viewModel.Closed = false;

            ClosableWindow window;
            if (ownerWindow != null)
            {
                window = ClosableWindow.CreateClosableWindow(viewModel, ownerWindow, canMinimize);
            }
            else if (ownerForm != null)
            {
                window = ClosableWindow.CreateClosableWindow(viewModel, ownerForm, canMinimize);
            }
            else
            {
                window = ClosableWindow.CreateClosableWindow(viewModel, canMinimize);
            }

            return window;
        }

        public string ShowOpenFileDialog(string filter)
        {
            var dlg = new OpenFileDialog
                          {
                              Filter = filter,
                              Multiselect = false
                          };

            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
                return dlg.FileName;
            return null;
        }

        public string ShowSaveFileDialog(string defaultExtension, string filter)
        {
            var dlg = new SaveFileDialog
                          {
                              DefaultExt = defaultExtension,
                              Filter = filter
                          };

            // Show open file dialog box
            bool? result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
                // Open document
                return dlg.FileName;
            return null;
        }

        /// <summary>
        /// Shows a standard System.Windows.MessageBox using the parameters requested
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="caption">The heading to be displayed</param>
        /// <param name="icon">The icon to be displayed.</param>
        public void ShowMessage(string message, string caption, CustomDialogIcons icon)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, GetImage(icon));
        }

        /// <summary>
        /// Shows a standard System.Windows.MessageBox using the parameters requested
        /// but will return a translated result to enable adhere to the IMessageBoxService
        /// implementation required.
        ///
        /// This abstraction allows for different frameworks to use the same ViewModels but supply
        /// alternative implementations of core service interfaces
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        /// <param name="caption">The caption of the message box window</param>
        /// <param name="icon">The icon to be displayed.</param>
        /// <param name="button"></param>
        /// <returns>CustomDialogResults results to use</returns>
        public CustomDialogResults ShowMessage(string message, string caption, CustomDialogIcons icon, CustomDialogButtons button)
        {
            MessageBoxResult result = MessageBox.Show(message, caption, GetButton(button), GetImage(icon));
            return GetResult(result);
        }

        /// <summary>
        /// Translates a CustomDialogIcons into a standard WPF System.Windows.MessageBox MessageBoxImage.
        /// This abstraction allows for different frameworks to use the same ViewModels but supply
        /// alternative implementations of core service interfaces
        /// </summary>
        /// <param name="icon">The icon to be displayed.</param>
        /// <returns>A standard WPF System.Windows.MessageBox MessageBoxImage</returns>
        private MessageBoxImage GetImage(CustomDialogIcons icon)
        {
            switch (icon)
            {
                default:
                    return MessageBoxImage.None;
                case CustomDialogIcons.Information:
                    return MessageBoxImage.Information;
                case CustomDialogIcons.Question:
                    return MessageBoxImage.Question;
                case CustomDialogIcons.Exclamation:
                    return MessageBoxImage.Exclamation;
                case CustomDialogIcons.Stop:
                    return MessageBoxImage.Stop;
                case CustomDialogIcons.Warning:
                    return MessageBoxImage.Warning;
            }
        }

        /// <summary>
        /// Translates a CustomDialogButtons into a standard WPF System.Windows.MessageBox MessageBoxButton.
        /// This abstraction allows for different frameworks to use the same ViewModels but supply
        /// alternative implementations of core service interfaces
        /// </summary>
        /// <param name="btn">The button type to be displayed.</param>
        /// <returns>A standard WPF System.Windows.MessageBox MessageBoxButton</returns>
        private MessageBoxButton GetButton(CustomDialogButtons btn)
        {
            switch (btn)
            {
                default:
                    return MessageBoxButton.OK;
                case CustomDialogButtons.OK:
                    return MessageBoxButton.OK;
                case CustomDialogButtons.OKCancel:
                    return MessageBoxButton.OKCancel;
                case CustomDialogButtons.YesNo:
                    return MessageBoxButton.YesNo;
                case CustomDialogButtons.YesNoCancel:
                    return MessageBoxButton.YesNoCancel;
            }
        }

        /// <summary>
        /// Translates a standard WPF System.Windows.MessageBox MessageBoxResult into a
        /// CustomDialogIcons.
        /// This abstraction allows for different frameworks to use the same ViewModels but supply
        /// alternative implementations of core service interfaces
        /// </summary>
        /// <param name="result">The standard WPF System.Windows.MessageBox MessageBoxResult</param>
        /// <returns>CustomDialogResults results to use</returns>
        private CustomDialogResults GetResult(MessageBoxResult result)
        {
            switch (result)
            {
                default:
                    return CustomDialogResults.None;
                case MessageBoxResult.Cancel:
                    return CustomDialogResults.Cancel;
                case MessageBoxResult.No:
                    return CustomDialogResults.No;
                case MessageBoxResult.None:
                    return CustomDialogResults.None;
                case MessageBoxResult.OK:
                    return CustomDialogResults.OK;
                case MessageBoxResult.Yes:
                    return CustomDialogResults.Yes;
            }
        }
    }
}