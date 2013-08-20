using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Pellared.Infrastructure.Services.Dialog;
using Pellared.Infrastructure.Services.Modal;
using Pellared.Infrastructure.Threading;
using Pellared.Infrastructure.Validation;
using Pellared.SalaryBook.Entities;
using Pellared.SalaryBook.IO;
using Pellared.SalaryBook.Messages;
using Pellared.SalaryBook.Properties;
using Pellared.SalaryBook.Services;

namespace Pellared.SalaryBook.ViewModels
{
    public enum FileType
    {
        Csv,
        Xml
    }

    public class SalaryTableViewModel : ViewModelBase
    {
        private const string CsvDescription = "csv";
        private const string CsvExtension = "txt";
        private const string XmlDescription = "xml";
        private const string XmlExtension = "xml";

        private readonly INavigationService navigationService;
        private readonly IDialogService dialogService;
        private readonly IUiDispatcher uiDispatcher;
        private readonly IModalService modalService;
        private readonly ImportExportManager importExportManager;
        private readonly IValidator<ISalary> salaryValidator;

        public SalaryTableViewModel(
                INavigationService navigationService,
                IDialogService dialogService,
                IUiDispatcher uiDispatcher,
                IModalService modalService,
                ImportExportManager importExportManager,
                IValidator<ISalary> salaryValidator)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.uiDispatcher = uiDispatcher;
            this.modalService = modalService;
            this.importExportManager = importExportManager;
            this.salaryValidator = salaryValidator;

            showSalaryCommand = new RelayCommand(ShowSalary, CanShowSalary);
            editSalaryCommand = new RelayCommand(EditSalary, CanEditSalary);
            deleteSalaryCommand = new RelayCommand(DeleteSalary, CanDeleteSalary);
            exportCommand = new RelayCommand<FileType>(Export, CanExport);
            importCommand = new RelayCommand<FileType>(Import, CanImport);

            Salary[] salaries = new[]
                                {
                                        new Salary
                                        {
                                                FirstName = "Jan",
                                                LastName = "Kowalski",
                                                BirthDate = new DateTime(1979, 5, 3),
                                                SalaryValue = 4300
                                        },
                                        new Salary
                                        {
                                                FirstName = "Adam",
                                                LastName = "Nowak",
                                                BirthDate = new DateTime(1972, 11, 4),
                                                SalaryValue = 5200
                                        }
                                };

            LoadSalaries(salaries);

            MessengerInstance.Register<SalaryAddedMessage>(this, OnSalaryAdded);
        }

        public ObservableCollection<EditableSalaryViewModel> Salaries { get; private set; }

        private EditableSalaryViewModel selectedSalary;

        public EditableSalaryViewModel SelectedSalary
        {
            get { return selectedSalary; }
            set
            {
                if (value != selectedSalary)
                {
                    selectedSalary = value;
                    RaisePropertyChanged(() => SelectedSalary);

                    showSalaryCommand.RaiseCanExecuteChanged();
                    editSalaryCommand.RaiseCanExecuteChanged();
                    deleteSalaryCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public bool IsSalarySelected
        {
            get { return SelectedSalary != null; }
        }

        #region ShowSalary command

        private readonly RelayCommand showSalaryCommand;

        public ICommand ShowSalaryCommand
        {
            get { return showSalaryCommand; }
        }

        private bool CanShowSalary()
        {
            return IsSalarySelected;
        }

        private void ShowSalary()
        {
            Salary salary = SelectedSalary.CreateEntity();
            SalaryDialogViewModel salaryDialogViewModel = new SalaryDialogViewModel(salary);
            modalService.Open(salaryDialogViewModel);
        }

        #endregion

        #region EditSalary command

        private readonly RelayCommand editSalaryCommand;

        public ICommand EditSalaryCommand
        {
            get { return editSalaryCommand; }
        }

        private bool CanEditSalary()
        {
            return IsSalarySelected;
        }

        private void EditSalary()
        {
            EditSalaryViewModel editSalaryViewModel = new EditSalaryViewModel(navigationService, SelectedSalary);
            navigationService.Navigate(editSalaryViewModel);
        }

        #endregion

        #region DeleteSalary command

        private readonly RelayCommand deleteSalaryCommand;

        public ICommand DeleteSalaryCommand
        {
            get { return deleteSalaryCommand; }
        }

        private bool CanDeleteSalary()
        {
            return IsSalarySelected;
        }

        private void DeleteSalary()
        {
            Salary salary = SelectedSalary.CreateEntity();
            DeleteSalaryDialogViewModel deleteSalaryDialogViewModel = new DeleteSalaryDialogViewModel(salary);
            modalService.Open(deleteSalaryDialogViewModel);
            if (deleteSalaryDialogViewModel.Result)
                Salaries.Remove(SelectedSalary);
        }

        #endregion

        #region Export command

        private readonly RelayCommand<FileType> exportCommand;

        public ICommand ExportCommand
        {
            get { return exportCommand; }
        }

        private bool CanExport(FileType fileType)
        {
            return Salaries.Any();
        }

        private void Export(FileType fileType)
        {
            string description;
            string extension;

            switch (fileType)
            {
                case FileType.Csv:
                    description = CsvDescription;
                    extension = CsvExtension;
                    break;
                case FileType.Xml:
                    description = XmlDescription;
                    extension = XmlExtension;
                    break;
                default:
                    return;
            }

            string filePath = dialogService.ShowSaveFileDialog(
                                                               extension,
                                                               string.Format(
                                                                             Resources.OpenFileDialogText, description,
                                                                             extension));
            if (string.IsNullOrEmpty(filePath))
                return;

            Task.Factory.StartNew(
                                  () =>
                                  {
                                      using (var csvFileWriter = new StreamWriter(filePath))
                                          switch (fileType)
                                          {
                                              case FileType.Csv:
                                                  importExportManager.CsvSalariesExporter.Export(GetSalaries(), csvFileWriter);
                                                  break;
                                              case FileType.Xml:
                                                  importExportManager.XmlSalariesExporter.Export(GetSalaries(), csvFileWriter);
                                                  break;
                                          }
                                  });
        }

        #endregion

        #region Import command

        private readonly RelayCommand<FileType> importCommand;

        public ICommand ImportCommand
        {
            get { return importCommand; }
        }

        private bool CanImport(FileType fileType)
        {
            return true;
        }

        private void Import(FileType fileType)
        {
            string description;
            string extension;

            switch (fileType)
            {
                case FileType.Csv:
                    description = CsvDescription;
                    extension = CsvExtension;
                    break;
                case FileType.Xml:
                    description = XmlDescription;
                    extension = XmlExtension;
                    break;
                default:
                    return;
            }

            string filePath =
                    dialogService.ShowOpenFileDialog(string.Format(Resources.OpenFileDialogText, description, extension));
            if (string.IsNullOrEmpty(filePath))
                return;

            Task.Factory.StartNew(
                                  () =>
                                  {
                                      IEnumerable<Salary> importedCollection = null;

                                      using (var csvFileReader = new StreamReader(filePath))
                                          switch (fileType)
                                          {
                                              case FileType.Csv:
                                                  importedCollection =
                                                          importExportManager.CsvSalariesImporter.Import(csvFileReader);
                                                  break;
                                              case FileType.Xml:
                                                  importedCollection =
                                                          importExportManager.XmlSalariesImporter.Import(csvFileReader);
                                                  break;
                                          }

                                      if (importedCollection != null)
                                          uiDispatcher.Invoke(ImportSalaries, importedCollection);
                                  });
        }

        #endregion

        private void OnSalaryAdded(SalaryAddedMessage message)
        {
            EditableSalaryViewModel salaryViewModel = new EditableSalaryViewModel(salaryValidator);
            salaryViewModel.LoadEntity(message.Content);
            Salaries.Add(salaryViewModel);
            exportCommand.RaiseCanExecuteChanged();
        }

        private IEnumerable<Salary> GetSalaries()
        {
            return Salaries.Select(x => x.CreateEntity());
        }

        private void LoadSalaries(IEnumerable<Salary> salaries)
        {
            Salaries = new ObservableCollection<EditableSalaryViewModel>();
            ImportSalaries(salaries);
        }

        private void ImportSalaries(IEnumerable<Salary> importedCollection)
        {
            foreach (var item in importedCollection)
            {
                EditableSalaryViewModel itemViewModel = new EditableSalaryViewModel(salaryValidator);
                itemViewModel.LoadEntity(item);
                Salaries.Add(itemViewModel);
            }

            exportCommand.RaiseCanExecuteChanged();
        }
    }
}