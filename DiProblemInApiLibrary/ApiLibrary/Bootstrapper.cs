using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyIoC;

namespace ApiLibrary
{
    public class Bootstrapper
    {
        private static readonly TinyIoCContainer Container;

        static Bootstrapper()
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                Locator = ServiceLocator.Current;
            }

            // using TinyIoC as internal in order that the clients could have any library they want
            Container = TinyIoCContainer.Current;
            Container.Register<IMotor, Motor>();
            Container.Register<ISpeed, Speed>();
        }

        public static IServiceLocator Locator { get; set; }

        internal static T Get<T>() where T : class
        {
            // client could also provide custom registration using Common Service Locator
            // alternatives: assembly scanning, MEF (MAF)
            if (Locator != null)
            {
                try
                {
                    return Locator.GetInstance<T>();
                }
                catch (ActivationException)
                {
                    // if not registered by the client
                }
            }

            return Container.Resolve<T>();
        }
    }
}
