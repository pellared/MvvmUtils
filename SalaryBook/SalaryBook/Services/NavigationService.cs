using Autofac;
using Pellared.SalaryBook.ViewModels;

namespace Pellared.SalaryBook.Services
{
    public class NavigationService : INavigationService
    {
        private MainViewModel mainViewModel;
        private MainSalaryViewModel mainSalaryViewModel;

        public void Navigate(object viewModel)
        {
            mainViewModel.CurrentViewModel = viewModel;
        }

        public void NavigateToMainSalary()
        {
            Navigate(mainSalaryViewModel);
        }

        internal void Initialize(IContainer container)
        {
            mainViewModel = container.Resolve<MainViewModel>();
            mainSalaryViewModel = container.Resolve<MainSalaryViewModel>();

            NavigateToMainSalary();
        }
    }
}