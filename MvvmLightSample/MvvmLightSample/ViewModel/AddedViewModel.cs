using Pellared.Utils.Mvvm.Dialog;

namespace MvvmLightSample.ViewModel
{
    public class AddedViewModel : IDialogViewModel
    {
        public bool Closed { get; set; }

        public string Title
        {
            get { return "Added!"; }
        }

        public string Message
        {
            get { return "A person has been added to the table."; }
        }
    }
}