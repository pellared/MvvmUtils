using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using CuttingEdge.Conditions;
using Seterlund.CodeGuard;
using Pellared.Common.Tests;
using EnsureThat;

namespace Pellared.Common.Contracts.Tests.Tests.Performance
{
    [TestClass]
    public class PerformanceTests
    {
        private const int Operations = 10000000;
        private readonly Stopwatch stopwatch = new Stopwatch();

        [TestMethod]
        public void NotNull_CheckPerformance()
        {
            Measure(EnsureSample, "My Ensure");
            Measure(ContractSample, "Code Contracts");
            Measure(ConditionSample, "CuttingEdge.Conditions");
            Measure(GuardSimple, "CodeGuard Simple");
            Measure(GuardLambda, "CodeGuard Lambda");
            Measure(EnsureThatSample, "EnsureThat");
        }

        [TestMethod]
        public void GratherThan_CheckPerformance()
        {
            MeasureGreater(EnsureSample, "My Ensure");
            MeasureGreater(ContractSample, "Code Contracts");
            MeasureGreater(ConditionEvaluateSample, "CuttingEdge.Conditions");
            MeasureGreater(ConditionExIsSample, "My CuttingEdge.Conditions Validate");
            MeasureGreater(GuardSimple, "CodeGuard Simple");
            MeasureGreater(GuardLambda, "CodeGuard Lambda");
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

        private void EnsureSample(string text)
        {
            Ensure.NotNull(text, "text");
        }

        private void ContractSample(string text)
        {
            Contract.Requires(text != null);
        }

        private void ConditionSample(string text)
        {
            Condition.Requires(text, "text").IsNotNull();
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
            EnsureThat.Ensure.That(text, "text").IsNotNull();
        }

        private void EnsureSample(int value)
        {
            Ensure.That(value > 100, "must be greater", "value");
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
            Condition.Requires(value, "value").Validate(x => x > 100, "must be greater");
        }

        private void GuardSimple(int value)
        {
            Guard.That(value, "value").IsTrue(x => x > 100, "must be greater");
        }

        private void GuardLambda(int value)
        {
            Guard.That(() => value).IsTrue(x => x > 100, "must be greater");
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
