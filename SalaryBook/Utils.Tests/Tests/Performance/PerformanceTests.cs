using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using CuttingEdge.Conditions;
using Seterlund.CodeGuard;
using Pellared.Utils.Tests;

namespace Pellared.Utils.Contracts.Tests.Tests.Performance
{
    [TestClass]
    public class PerformanceTests
    {
        private readonly Stopwatch stopwatch = new Stopwatch();

        [TestMethod]
        public void PerformanceTest()
        {
            Measure(ThrowSample, "My Throw");
            Measure(RequireSimple, "My Require Simple");
            Measure(RequireLambda, "My Require Lambda");
            Measure(ContractSample, "Code Contracts");
            Measure(ConditionSimple, "CuttingEdge.Conditions");
            Measure(ConditionLambda, "CuttingEdge.Conditions LambdaEx");
            Measure(GuardSimple, "CodeGuard Simple");
            Measure(GuardLambda, "CodeGuard Lambda");
        }

        private void Measure(Action<string> action, string name)
        {
            using (stopwatch.Measurement())
            {
                for (int i = 0; i < 100000; i++)
                {
                    action("something");
                }
            }

            Console.WriteLine(stopwatch.ElapsedMilliseconds + "\t" + name);
        }

        private void RequireSimple(string text)
        {
            Require.That(text, "text").IsNotNull();
        }

        private void RequireLambda(string text)
        {
            Require.That(() => text).IsNotNull();
        }

        private void ThrowSample(string text)
        {
            Throw.IfNull(text, "text");
        }

        private void ContractSample(string text)
        {
            Contract.Requires(text != null);
        }

        private void ConditionSimple(string text)
        {
            Condition.Requires(text, "text").IsNotNull();
        }

        private void ConditionLambda(string text)
        {
            ConditionEx.Requires(() => text).IsNotNull();
        }

        private void GuardSimple(string text)
        {
            Guard.That(text, "text").IsNotNull();
        }

        private void GuardLambda(string text)
        {
            Guard.That(() => text).IsNotNull();
        }
    }

    public static class StopwatchEx
    {
        public static IDisposable Measurement(this Stopwatch stopwatch)
        {
            stopwatch.Restart();
            var stopper = new DisposeAction(stopwatch.Stop);
            return stopper;
        }
    }
}
