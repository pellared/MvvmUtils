using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using CuttingEdge.Conditions;
using Seterlund.CodeGuard;
using Pellared.Utils.Tests;
using EnsureThat;

namespace Pellared.Utils.Contracts.Tests.Tests.Performance
{
    [TestClass]
    public class PerformanceTests
    {
        private const int Operations = 1000000;
        private readonly Stopwatch stopwatch = new Stopwatch();

        [TestMethod]
        public void NotNull_EverthingGood_CheckPerformance()
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
        public void GratherThan_HundredthFailing_CheckPerformance()
        {
            MeasureGreater(ThrowSample, "My Throw");
            MeasureGreater(RequireSimple, "My Require Simple");
            MeasureGreater(RequireLambda, "My Require Lambda");
            MeasureGreater(ContractSample, "Code Contracts");
            MeasureGreater(ConditionSimple, "CuttingEdge.Conditions");
            MeasureGreater(ConditionLambda, "CuttingEdge.Conditions LambdaEx");
            MeasureGreater(GuardSimple, "CodeGuard Simple");
            MeasureGreater(GuardLambda, "CodeGuard Lambda");
            MeasureGreater(EnsureThatSample, "EnsureThat");
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
                    try
                    {
                        action(i);
                    }
                    catch (Exception)
                    {
                    }
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

        private void EnsureThatSample(string text)
        {
            Ensure.That(text, "text").IsNotNull();
        }


        private void RequireSimple(int value)
        {
            Require.That(value, "value").Is(x => x > Operations / 100, "must be greater");
        }

        private void RequireLambda(int value)
        {
            Require.That(() => value).Is(x => x > Operations / 100, "must be greater");
        }

        private void ThrowSample(int value)
        {
            Throw.IfNot(value > Operations / 100, "value", "must be greater");
        }

        private void ContractSample(int value)
        {
            Contract.Requires(value > Operations / 100, "must be greater");
        }

        private void ConditionSimple(int value)
        {
            Condition.Requires(value, "value").Evaluate(x => x > Operations / 100, "must be greater");
        }

        private void ConditionLambda(int value)
        {
            ConditionEx.Requires(() => value).Evaluate(x => x > Operations / 100, "must be greater");
        }

        private void GuardSimple(int value)
        {
            Guard.That(value, "value").IsTrue(x => x > Operations / 100, "must be greater");
        }

        private void GuardLambda(int value)
        {
            Guard.That(() => value).IsTrue(x => x > Operations / 100, "must be greater");
        }

        private void EnsureThatSample(int value)
        {
            Ensure.That(value, "value").IsGt(Operations / 100);
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
