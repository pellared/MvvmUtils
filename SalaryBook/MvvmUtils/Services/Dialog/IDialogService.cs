using System;

namespace Pellared.MvvmUtils.Services.Dialog
{
    public interface IDialogService
    {
        string ShowOpenFileDialog(string filter);
        string ShowSaveFileDialog(string defaultExtension, string filter);
    }
}