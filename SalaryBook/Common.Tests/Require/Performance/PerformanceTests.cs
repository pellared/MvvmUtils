using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using CuttingEdge.Conditions;
using Seterlund.CodeGuard;
using Pellared.Common.Tests;
using EnsureThat;
using Pellared.Common.Conditions;

namespace Pellared.Common.Contracts.Tests.Tests.Performance
{
    [TestClass]
    public class PerformanceTests
    {
        private const int Operations = 1000000;
        private readonly Stopwatch stopwatch = new Stopwatch();

        [TestMethod]
        public void NotNull_CheckPerformance()
        {
            Measure(ThrowSample, "My Throw");
            Measure(RequireSimple, "My Require Simple");
            Measure(RequireLambda, "My Require Lambda");
            Measure(ContractSample, "Code Contracts");
            Measure(ConditionSimple, "CuttingEdge.Conditions");
            Measure(ConditionLambda, "CuttingEdge.Conditions LambdaEx");
            Measure(GuardSimple, "CodeGuard Simple");
            Measure(GuardLambda, "CodeGuard Lambda");
            Measure(EnsureThatSample, "EnsureThat");
        }

        [TestMethod]
        public void GratherThan_CheckPerformance()
        {
            MeasureGreater(ThrowSample, "My Throw");
            MeasureGreater(RequireSample, "My Require");
            MeasureGreater(ContractSample, "Code Contracts");
            MeasureGreater(ConditionEvaluateSample, "CuttingEdge.Conditions Evaluate");
            MeasureGreater(ConditionExIsSample, "CuttingEdge.Conditions Is extensions");
            MeasureGreater(GuardSample, "CodeGuard Simple");
        }

        private void Measure(Action<string> action, string name)
        {
            using (stopwatch.Measurement())
            {
                for (int i = 0; i < Operations; i++)
                {
                    action("something");
                }
            }

            Console.WriteLine(stopwatch.ElapsedMilliseconds + "\t" + name);
        }

        private void MeasureGreater(Action<int> action, string name)
        {
            using (stopwatch.Measurement())
            {
                for (int i = 0; i < Operations; i++)
                {
                    action(200);
                }
            }

            Console.WriteLine(stopwatch.ElapsedMilliseconds + "\t" + name);
        }

        private void ThrowSample(string text)
        {
            Throw.IfNull(text, "text");
        }

        private void RequireSimple(string text)
        {
            Require.That(text, "text").IsNotNull();
        }

        private void RequireLambda(string text)
        {
            Require.That(() => text).IsNotNull();
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

        private void EnsureThatSample(string text)
        {
            Ensure.That(text, "text").IsNotNull();
        }

        private void ThrowSample(int value)
        {
            Throw.IfNot(value > 100, "value", "must be greater");
        }

        private void RequireSample(int value)
        {
            Require.That(value, "value").Is(x => x > 100, "must be greater");
        }

        private void ContractSample(int value)
        {
            Contract.Requires(value > 100, "must be greater");
        }

        private void ConditionEvaluateSample(int value)
        {
            Condition.Requires(value, "value").Evaluate(value > 100, "must be greater");
        }

        private void ConditionExIsSample(int value)
        {
            Condition.Requires(value, "value").Is(x => x > 100, "must be greater");
        }

        private void GuardSample(int value)
        {
            Guard.That(value, "value").IsTrue(x => x > 100, "must be greater");
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
