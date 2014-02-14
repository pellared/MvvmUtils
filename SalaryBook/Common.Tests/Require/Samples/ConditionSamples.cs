using CuttingEdge.Conditions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Pellared.Common.Tests.Contracts.Samples
{
    [TestClass]
    public class ConditionSamples
    {
        public void Sample(string argument)
        {
            Condition.Requires(argument).IsNotNull();
            Condition.WithExceptionOnFailure<OutOfMemoryException>().Requires(argument, "argument").IsNotNullOrWhiteSpace("argument caused a memory leak");
        }

        public void SampleEx(string argument)
        {
            ConditionEx.Requires(() => argument).IsNotNull();
            Condition<OutOfMemoryException>.Requires(() => argument).IsNotNullOrWhiteSpace("argument caused a memory leak");
        }

        [TestMethod]
        public void CuttingEdgeConditionsTest()
        {
            Action act = () => Sample(string.Empty);
            act.ShouldThrow<OutOfMemoryException>().WithMessage("*argument caused a memory leak*");
        }

        [TestMethod]
        public void CuttingEdgeConditionsTestEx()
        {
            Action act = () => SampleEx(string.Empty);
            act.ShouldThrow<OutOfMemoryException>().WithMessage("*argument caused a memory leak*");
        }
    }
}