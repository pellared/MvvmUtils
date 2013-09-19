using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pellared.Utils.Contracts;

using FluentAssertions;

namespace Pellared.Utils.Tests.Contracts
{
    [TestClass]
    public class Samples
    {
        public void NormalUsage(string argument)
        {
            Check.Argument(() => argument)
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

        public int ParsePostiveNumber(string number)
        {
            Argument<string> numberArgument = Check.Argument(() => number)
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