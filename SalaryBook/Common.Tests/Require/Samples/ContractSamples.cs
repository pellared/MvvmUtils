using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.Contracts;

namespace Pellared.Common.Tests.Contracts.Samples
{
    [TestClass]
    public class ContractSamples
    {
        public void Sample(string argument)
        {
            Contract.Requires(argument != null);
            Contract.Requires<OutOfMemoryException>(!string.IsNullOrWhiteSpace(argument), "argument caused a memory leak");
        }

        [TestMethod]
        public void CodeContractsTest()
        {
            Action act = () => Sample(string.Empty);
            act.ShouldThrow<OutOfMemoryException>().WithMessage("*argument caused a memory leak*");
        }
    }
}