using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Pellared.SalaryBook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if DEBUG
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
#endif

            MainWindow window = new MainWindow();
            Bootstrapper.WindowArgs windowArgs = new Bootstrapper.WindowArgs()
                                                     {
                                                             MainWindow = window
                                                     };
            using (Bootstrapper bootstrapper = new Bootstrapper(windowArgs))
            {
                window.DataContext = bootstrapper.MainViewModel;
                window.Show();
            }
        }

        #region UnhandledException handling

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();
            HandleUnhandledException(e.Exception.InnerException);
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandleUnhandledException(e.Exception);
            e.Handled = true;
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleUnhandledException((Exception)e.ExceptionObject);
        }

        private static void HandleUnhandledException(Exception ex)
        {
            MessageBox.Show(ex.Message, SalaryBook.Properties.Resources.ErrorText, MessageBoxButton.OK, MessageBoxImage.Error);
            Trace.WriteLine(string.Format("Error: {0}", ex.Message));
        }

        #endregion
    }
}