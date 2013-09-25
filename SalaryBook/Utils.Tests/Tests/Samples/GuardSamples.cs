using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seterlund.CodeGuard;

namespace Pellared.Utils.Tests.Contracts.Samples
{
    [TestClass]
    public class GuardSamples
    {
        public void Sample(string argument)
        {
            Guard.That(() => argument).IsNotNull();
            Guard.That(() => argument).IsTrue(x => !string.IsNullOrWhiteSpace(x), "argument caused a memory leak");
        }

        [TestMethod]
        public void RequireTest()
        {
            Action act = () => Sample(string.Empty);
            act.ShouldThrow<ArgumentException>().WithMessage("*argument caused a memory leak*");
        }
    }
}