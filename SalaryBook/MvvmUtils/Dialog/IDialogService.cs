namespace Pellared.Utils.Mvvm.Dialog
{
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

    public interface IDialogService
    {
        void ShowDialog(IDialogViewModel viewModel);

        string ShowOpenFileDialog(string filter);

        string ShowSaveFileDialog(string defaultExtension, string filter);

        void ShowMessage(string message, string caption, DialogIcons icon);

        DialogResults ShowMessage(string message, string caption, DialogIcons icon, DialogButtons buttons);
    }
}