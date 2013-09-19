using CuttingEdge.Conditions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pellared.Utils.Tests.Contracts.Samples
{
    [TestClass]
    public class RequireSamples
    {
        public void Sample(string argument)
        {
            Require.That(() => argument).IsNotNull();
            Require<OutOfMemoryException>.That(() => argument).IsNotNullOrWhiteSpace("argument caused a memory leak");
        }

        [TestMethod]
        public void RequireTest()
        {
            Action act = () => Sample(string.Empty);
            act.ShouldThrow<OutOfMemoryException>().WithMessage("*argument caused a memory leak*");
        }
    }
}
