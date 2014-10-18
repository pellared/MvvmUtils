using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmLightSample.Model;
using Pellared.Common;
using Pellared.MvvmUtils;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MvvmLightSample.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IDialogService _dialogService;
        private readonly PersonViewModel _personelViewModel;
        private readonly PropertyObserver<PersonViewModel> _personObserver;
        private readonly IWindowService _windowService;
        private RelayCommand _addPerson;
        private string _welcomeTitle;

        public MainViewModel(PersonViewModel personelViewModel, IDataService dataService, IDialogService dialogService, IWindowService windowService)
        {
            Ensure.NotNull(personelViewModel, "personelViewModel");
            Ensure.NotNull(dataService, "dataService");
            Ensure.NotNull(dialogService, "dialogService");
            Ensure.NotNull(windowService, "windowService");

            _personelViewModel = personelViewModel;
            _dataService = dataService;
            _dialogService = dialogService;
            _windowService = windowService;

            if (IsInDesignModeStatic)
            {
                People = new ObservableCollection<PersonViewModel>()
                {
                    new PersonViewModel() { Name = "Jon" },
                    new PersonViewModel() { Name = "Robert" }
                };
            }
            else
            {
                People = new ConcurrentObservableCollection<PersonViewModel>();
            }

            //_vmLocator = new ViewModelLocator();
            _personObserver = new PropertyObserver<PersonViewModel>(_personelViewModel)
                .RegisterHandler(p => p.Name, GreetJon);

            GetData();
        }

        public RelayCommand AddPerson
        {
            get
            {
                return _addPerson ?? (_addPerson = new RelayCommand(ExecuteAddPerson));
            }
        }

        public ObservableCollection<PersonViewModel> People { get; set; }

        public string WelcomeTitle
        {
            get { return _welcomeTitle; }
            set { Set(() => WelcomeTitle, ref _welcomeTitle, value); }
        }

        private void ExecuteAddPerson()
        {
            if (_personelViewModel.HasErrors)
            {
                _dialogService.ShowMessage("Please, enter a name", "Error", DialogIcon.Stop);
            }
            else
            {
                Task.Run(() =>
                    {
                        var personToAdd = new PersonViewModel() { Name = _personelViewModel.Name.Trim() };
                        People.Add(personToAdd);
                        _personelViewModel.Name = string.Empty;
                    })
                    .ContinueWith(task =>
                    {
                        var addedVM = new AddedViewModel();
                        _windowService.ShowDialog(addedVM, ResizeMode.CanMinimize);
                    }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void GetData()
        {
            _dataService.GetData(
                (item, error) =>
                {
                    if (error != null)
                    {
                        return;
                    }

                    WelcomeTitle = item.Title;
                });
        }

        private void GreetJon(PersonViewModel person)
        {
            if (person.Name == "Jon")
            {
                _dialogService.ShowMessage("Hi Jon, I missed you!", "Tony the Pony greetings", DialogIcon.Information);
            }
        }
    }
}