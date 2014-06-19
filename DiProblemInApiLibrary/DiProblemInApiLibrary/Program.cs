using ApiLibrary;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiProblemInApiLibrary
{
    class Program
    {
        class SuperSpeed : ISpeed
        {
            public int KilometeresPerHour
            {
                get
                {
                    return 1000;
                }
                set { }
            }
        }

        static void Main(string[] args)
        {
            BootstrapApiLibrary();

            var drive = new Drive();
            drive.MotorSpeed = 100;
            drive.StartMotor();

            ServiceLocator.Current.GetInstance<OwnedSample>();
        }

        private static void BootstrapApiLibrary()
        {
            var builder = new ContainerBuilder();

            // registering owned generic
            builder.RegisterGeneric(typeof(AutofacOwned<>)).As(typeof(IOwned<>));
            builder.RegisterGeneric(typeof(AutofacFactory<>)).As(typeof(IFactory<>));
            builder.RegisterGeneric(typeof(AutofacFactory<,>)).As(typeof(IFactory<,>));
            
            builder.RegisterType<Some>().As<ISome>();
            builder.RegisterType<WithArg>().As<IWithArg>();
            builder.RegisterType<OwnedSample>();
            
            // changing the ISpeed implementation provided by ApiLibrary
            builder.RegisterType<SuperSpeed>().As<ISpeed>();
            var container = builder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container)); // if using the Current Locator
            //Bootstrapper.Locator = new AutofacServiceLocator(container); // we could also make a collection of Locators if it would be neeeded
        }
    }
}
