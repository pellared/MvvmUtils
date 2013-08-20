using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32;

namespace Pellared.Infrastructure.Services.Dialog
{
    public class DialogService : IDialogService
    {
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
    }
}