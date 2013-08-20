using Pellared.SalaryBook.ViewModels;

namespace Pellared.SalaryBook.Services
{
    public class NavigationService : INavigationService
    {
        private readonly MainViewModel mainViewModel;
        private MainSalaryViewModel mainSalaryViewModel;

        public NavigationService(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
        }

        public void RegisterMainSalary(MainSalaryViewModel mainSalaryViewModel)
        {
            this.mainSalaryViewModel = mainSalaryViewModel;
        }

        public void Navigate(object viewModel)
        {
            mainViewModel.CurrentViewModel = viewModel;
        }

        public void NavigateToMainSalary()
        {
            Navigate(mainSalaryViewModel);
        }
    }
}