using System;

namespace Pellared.Utils.Mvvm.Services.Dialog
{
    public interface IDialogService
    {
        string ShowOpenFileDialog(string filter);
        string ShowSaveFileDialog(string defaultExtension, string filter);
    }
}