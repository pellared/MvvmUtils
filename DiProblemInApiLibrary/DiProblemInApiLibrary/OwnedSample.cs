using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiProblemInApiLibrary
{
    interface ISome
    {
        void Do();
    }

    interface IWithArg
    {
        void Do();
    }

    public class Some : ISome, IDisposable
    {
        public void Do()
        {
            Console.WriteLine("Do");
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose");
        }
    }

    public class WithArg : IWithArg, IDisposable
    {
        private readonly string text;

        public WithArg(string text)
        {
            this.text = text;
        }

        public void Do()
        {
            Console.WriteLine(text);
        }

        public void Dispose()
        {
            Console.WriteLine(text + " Dispose");
        }
    }

    class OwnedSample
    {
        public OwnedSample(IOwned<ISome> some, IFactory<ISome> someFactory, IFactory<string, IWithArg> argFactory)
        {
            using (some)
            {
                some.Value.Do();
            }

            using (IOwned<ISome> created = someFactory.Create())
            {
                created.Value.Do();
            }

            using (var nestedCreated = argFactory.Create("Test Arg Factory"))
            {
                nestedCreated.Value.Do();
            }
        }
    }
}
