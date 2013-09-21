using CuttingEdge.Conditions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Pellared.Utils.Tests.Contracts.Samples
{
    [TestClass]
    public class ConditionSamples
    {
        public void Sample(string argument)
        {
            Condition.Requires(argument).IsNotNull();
            Condition.WithExceptionOnFailure<OutOfMemoryException>().Requires(argument, "argument").IsNotNullOrWhiteSpace("argument caused a memory leak");
        }

        [TestMethod]
        public void CuttingEdgeConditionsTest()
        {
            Action act = () => Sample(string.Empty);
            act.ShouldThrow<OutOfMemoryException>().WithMessage("*argument caused a memory leak*");
        }
    }
}