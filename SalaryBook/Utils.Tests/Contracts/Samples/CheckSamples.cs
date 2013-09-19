using CuttingEdge.Conditions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pellared.Utils.Contracts;
using System;
using System.Diagnostics.Contracts;

namespace Pellared.Utils.Tests.Contracts
{
    [TestClass]
    public class CheckSamples
    {
        public void Sample(string argument)
        {
            Check.If(() => argument)
                .IsNotNull()
                .IsNotNullOrWhiteSpace(arg => new OutOfMemoryException(arg.Name + " caused a memory leak"));
        }

        [TestMethod]
        public void CheckTest()
        {
            Action act = () => Sample(string.Empty);
            act.ShouldThrow<OutOfMemoryException>().WithMessage("*argument caused a memory leak*");
        }

        private void NormalUsage(string argument)
        {
            Check.If(() => argument)
                .IsNotNullOrWhiteSpace()
                .Is(x => x.Length >= 5);

            // some logic
        }

        [TestMethod]
        public void NormalUsage_GoodParameter_NoException()
        {
            const string parameter = "abcde";
            Action act = () => NormalUsage(parameter);
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void NormalUsage_ToShortParameter_ThrowsArgumentException()
        {
            const string parameter = "abc";
            Action act = () => NormalUsage(parameter);
            act.ShouldThrow<ArgumentException>();
        }

        private int ParsePostiveNumber(string number)
        {
            Argument<string> numberArgument = Check.If(() => number)
                .IsNotNullOrWhiteSpace();

            int result;
            bool parsed = int.TryParse(number, out result);

            numberArgument
                .Is(x => parsed, "Is not a number")
                .Is(x => result > 0, "Is not a positive number");

            return result;
        }

        [TestMethod]
        public void PostiveNumber_NoException()
        {
            string parameter = "5";
            Action act = () => ParsePostiveNumber(parameter);
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void NegativeNumber_ThrowsException()
        {
            string parameter = "-5";
            Action act = () => ParsePostiveNumber(parameter);
            act.ShouldThrow<ArgumentException>().WithMessage("Is not a positive number", ComparisonMode.EquivalentSubstring);
        }

        [TestMethod]
        public void StringEmpty_ThrowsException()
        {
            string parameter = string.Empty;
            Action act = () => ParsePostiveNumber(parameter);
            act.ShouldThrow<ArgumentException>();
        }
    }
}