using Pellared.MvvmUtils;

namespace MvvmLightSample.ViewModel
{
    public class AddedViewModel : IWindowViewModel
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