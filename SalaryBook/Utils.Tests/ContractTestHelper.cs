using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.Utils.Tests
{
    public static class ContractTestHelper
    {
        const string ContractExceptionName = "System.Diagnostics.Contracts.__ContractsRuntime+ContractException";

        public static void ShouldThrowContractException(this Action action)
        {
            Contract.Requires(action != null);

            try
            {
                action();
                Assert.Fail("Expected contract failure");
            }
            catch (Exception e)
            {
                if (e.GetType().FullName != ContractExceptionName)
                {
                    throw;
                }

                // Correct exception was thrown. Fine
            }
        }
    }
}
