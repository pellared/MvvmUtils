using FluentAssertions;
using EnsureThat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.Utils.Contracts.Tests.Tests.Samples
{
    [TestClass]
    public class EnsureSamples
    {
        [TestMethod]
        public void Sample(string argument)
        {
            Ensure.That(argument, "argument").IsNotNull();
            Ensure.That(argument, "argument").IsNotNullOrWhiteSpace().WithExtraMessageOf(() => "argument caused a memory leak");
        }

        [TestMethod]
        public void RequireTest()
        {
            Action act = () => Sample(string.Empty);
            act.ShouldThrow<ArgumentException>().WithMessage("*argument caused a memory leak*");
        }
    }
}
