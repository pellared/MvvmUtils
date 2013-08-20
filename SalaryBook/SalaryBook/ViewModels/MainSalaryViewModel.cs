namespace Pellared.SalaryBook.ViewModels
{
    public class MainSalaryViewModel
    {
        private readonly SalaryTableViewModel salaryTableViewModel;
        private readonly AddSalaryViewModel addSalaryViewModel;

        public MainSalaryViewModel(SalaryTableViewModel salaryTableViewModel, AddSalaryViewModel addSalaryViewModel)
        {
            this.salaryTableViewModel = salaryTableViewModel;
            this.addSalaryViewModel = addSalaryViewModel;
        }

        public SalaryTableViewModel SalaryTableViewModel
        {
            get { return salaryTableViewModel; }
        }

        public AddSalaryViewModel AddSalaryViewModel
        {
            get { return addSalaryViewModel; }
        }
    }
}