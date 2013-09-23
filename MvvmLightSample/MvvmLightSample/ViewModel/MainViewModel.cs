using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmLightSample.Model;
using Pellared.Utils.Mvvm.Dialog;
using Pellared.Utils.Mvvm.ViewModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MvvmLightSample.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IDialogService _dialogService;
        private readonly PropertyObserver<PersonViewModel> _personObserver;
        private readonly ViewModelLocator _vmLocator;

        public MainViewModel(IDataService dataService, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _dataService = dataService;

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
            
            _vmLocator = new ViewModelLocator();
            _personObserver = new PropertyObserver<PersonViewModel>(_vmLocator.Person)
                .RegisterHandler(p => p.Name, GreetJon);

            GetData();
        }

        private string _welcomeTitle;
        public string WelcomeTitle
        {
            get { return _welcomeTitle; }
            set { Set(() => WelcomeTitle, ref _welcomeTitle, value); }
        }

        public ObservableCollection<PersonViewModel> People { get; set; }

        private RelayCommand _addPerson;
        public RelayCommand AddPerson
        {
            get
            {
                return _addPerson ?? (_addPerson = new RelayCommand(ExecuteAddPerson));
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

        private void ExecuteAddPerson()
        {
            PersonViewModel person = _vmLocator.Person;
            if (person.HasErrors)
            {
                _dialogService.ShowMessage("Please, enter a name", "Error", DialogIcons.Stop);
            }
            else
            {
                Task.Run(() => 
                    {
                        var personToAdd = new PersonViewModel() { Name = person.Name.Trim() };
                        People.Add(personToAdd);
                        person.Name = string.Empty;
                    });
            }
        }

        private void GreetJon(PersonViewModel person)
        {
            if (person.Name == "Jon")
            {
                _dialogService.ShowMessage("Hi Jon, I missed you!", "Tony the Pony greetings", DialogIcons.Information);
            }
        }
    }
}