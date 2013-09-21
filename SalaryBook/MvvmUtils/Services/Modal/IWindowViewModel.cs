using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.Utils.Mvvm.Services.Modal
{
    public interface IWindowViewModel : IDialogViewModel
    {
        void OnLoaded();
        void OnClosing(CancelEventArgs args);
    }
}
