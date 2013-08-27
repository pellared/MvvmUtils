using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;

using Autofac;

using Pellared.MvvmUtils.Services.Dialog;
using Pellared.MvvmUtils.Services.Modal;
using Pellared.MvvmUtils.Threading;
using Pellared.MvvmUtils.Validation;
using Pellared.SalaryBook.Entities;
using Pellared.SalaryBook.IO;
using Pellared.SalaryBook.Services;
using Pellared.SalaryBook.Validators;
using Pellared.SalaryBook.ViewModels;

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
            RegisterCommonServices();
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

        private void RegisterCommonServices()
        {
            containerBuilder.RegisterInstance<IDialogService>(new DialogService());
            containerBuilder.RegisterInstance<IUiDispatcher>(UiDispatcher.Current);
        }

        private void RegisterDialogService()
        {
            if (windowArgs.MainWindow != null)
                containerBuilder.RegisterInstance<IModalService>(new ModalService(windowArgs.MainWindow));
            else if (windowArgs.MainForm != null)
                containerBuilder.RegisterInstance<IModalService>(new ModalService(windowArgs.MainForm));
            else
                containerBuilder.RegisterInstance<IModalService>(new ModalService());
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
            navigationService.RegisterMainSalary(container.Resolve<MainSalaryViewModel>());
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