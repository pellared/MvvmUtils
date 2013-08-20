using System;

namespace Pellared.Infrastructure.Services.Dialog
{
    public interface IDialogService
    {
        string ShowOpenFileDialog(string filter);
        string ShowSaveFileDialog(string defaultExtension, string filter);
    }
}