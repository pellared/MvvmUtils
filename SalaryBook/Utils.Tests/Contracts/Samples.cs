using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pellared.Utils.Contracts;

namespace Pellared.Utils.Tests.Contracts
{
    [TestClass]
    public class Samples
    {
        public int ParseNaturalNumber(string number)
        {
            ArgumentValidator<string> numberArgument = Require.Argument(() => number)
                .IsNotNullOrWhiteSpace();
            
            int result;
            bool parsed = int.TryParse(number, out result);

            numberArgument
                .Is(x => parsed, "Is not a number")
                .Is(x => result < 0, "Is not a natural number");

            return result;
        }

        [TestMethod]
        public void MyTestMethod()
        {
            
        }

        
    }
}