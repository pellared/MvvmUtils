using System;
using System.Collections.Generic;
using System.Linq;

using Pellared.SalaryBook.Views;

using Application = System.Windows.Forms.Application;

namespace Pellared.SalaryBook.WinForms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // create Wpf.Application.Current
            System.Windows.Application application = new System.Windows.Application
            {
                ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown
            };

            if (System.Windows.Application.Current != null)
            {
                // merge in your application resources
                System.Windows.Application.Current.Resources.MergedDictionaries.Add(
                    System.Windows.Application.LoadComponent(
                        new Uri("/SalaryBook;component/Resources/AppResources.xaml", UriKind.Relative))
                            as System.Windows.ResourceDictionary);
            }

            MainForm mainForm = new MainForm();
            MainView mainView = new MainView();
            mainForm.Element = mainView;
            
            Bootstrapper.WindowArgs windowArgs = new Bootstrapper.WindowArgs()
            {
                MainForm = mainForm
            };
            Bootstrapper bootstrapper = new Bootstrapper(windowArgs);
            try
            {
                mainView.DataContext = bootstrapper.MainViewModel;
                Application.Run(mainForm);
                application.Shutdown();
            } 
            finally
            {
                bootstrapper.Dispose();
                application.Shutdown();
            }
        }
    }
}