namespace Pellared.SalaryBook.Services
{
    public interface INavigationService
    {
        void Navigate(object viewModel);
        void NavigateToMainSalary();
    }
}