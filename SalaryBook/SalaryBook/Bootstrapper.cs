using Autofac;
using Pellared.Common.Mvvm;
using Pellared.Common.Mvvm.Validation;
using Pellared.SalaryBook.Entities;
using Pellared.SalaryBook.IO;
using Pellared.SalaryBook.Services;
using Pellared.SalaryBook.Validators;
using Pellared.SalaryBook.ViewModels;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;

namespace Pellared.SalaryBook
{
    public sealed class Bootstrapper : IDisposable
    {
        public class WindowArgs
        {
            public Window MainWindow { get; set; }

            public Form MainForm { get; set; }
        }

        private readonly WindowArgs windowArgs;
        private readonly MainViewModel mainViewModel;
        private readonly ContainerBuilder containerBuilder;
        private IContainer container;

        public Bootstrapper(WindowArgs windowArgs)
        {
            if (windowArgs.MainForm != null && windowArgs.MainWindow != null) throw new ArgumentException("windowArgs contains more than one main view");

            this.windowArgs = windowArgs;
            mainViewModel = new MainViewModel();
            containerBuilder = new ContainerBuilder();

            Thread.CurrentThread.CurrentUICulture = CultureInfo.CurrentCulture;
            FixLocalizedBindings();

            ConfigureContainer();
            ConfigureNavigationService();
        }

        public void Dispose()
        {
            container.Dispose();
        }

        public MainViewModel MainViewModel
        {
            get { return mainViewModel; }
        }

        private void ConfigureContainer()
        {
            RegisterViewModels();
            RegisterDialogService();
            RegisterNavigationService();
            RegisterOtherServices();

            container = containerBuilder.Build();
        }

        private void RegisterViewModels()
        {
            var assembly = Assembly.GetExecutingAssembly();
            containerBuilder.RegisterAssemblyTypes(assembly)
                            .Where(t => t.Name.EndsWith("ViewModel", StringComparison.OrdinalIgnoreCase))
                            .AsSelf();

            containerBuilder.RegisterInstance(mainViewModel);
        }

        private void RegisterDialogService()
        {
            if (windowArgs.MainWindow != null)
                containerBuilder.RegisterInstance<IDialogService>(new DialogService(windowArgs.MainWindow));
            else if (windowArgs.MainForm != null)
                containerBuilder.RegisterInstance<IDialogService>(new DialogService(windowArgs.MainForm));
            else
                containerBuilder.RegisterInstance<IDialogService>(new DialogService());
        }

        private void RegisterNavigationService()
        {
            containerBuilder.RegisterType<NavigationService>().SingleInstance().AsSelf().As<INavigationService>();
        }

        private void RegisterOtherServices()
        {
            containerBuilder.RegisterInstance(new ImportExportManager(
                                               new CsvSalariesImporter(),
                                               new CsvSalariesExporter(),
                                               new XmlSalariesImporter(),
                                               new XmlSalariesExporter()));

            containerBuilder.RegisterType<SalaryValidator>().As<IValidator<ISalary>>();
        }

        private void ConfigureNavigationService()
        {
            NavigationService navigationService = container.Resolve<NavigationService>();
            navigationService.Initialize(container);
            navigationService.NavigateToMainSalary();
        }

        /// <summary>
        /// Ensure the current culture passed into bindings
        /// is the OS culture. By default, WPF uses en-US
        /// as the culture, regardless of the system settings
        /// </summary>
        private static void FixLocalizedBindings()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                                                               typeof(FrameworkElement),
                                                               new FrameworkPropertyMetadata(
                                                                       XmlLanguage.GetLanguage(
                                                                                               CultureInfo.CurrentCulture
                                                                                                          .IetfLanguageTag)));
        }
    }
}