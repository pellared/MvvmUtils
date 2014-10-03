using GalaSoft.MvvmLight;

namespace Pellared.SalaryBook.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private object currentViewModel;

        public object CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                if (value != currentViewModel)
                {
                    currentViewModel = value;
                    RaisePropertyChanged(() => CurrentViewModel);
                }
            }
        }
    }
}