using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pellared.Utils.Contracts;

using FluentAssertions;

namespace Pellared.Utils.Tests.Contracts
{
    [TestClass]
    public class ArgumentValidationsTests
    {
        [TestMethod]
        public void Is_WhenConditionPasses_NoException()
        {
            Argument<int> validation = new Argument<int>(1, "argumentName");
            Action act = () => validation.Is(x => x > 0);
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void Is_WhenConditionFails_ThrowsArgumentException()
        {
            Argument<int> validation = new Argument<int>(1, "argumentName");
            Action act = () => validation.Is(x => x < 0);
            act.ShouldThrow<ArgumentException>().WithMessage("*x < 0*", ComparisonMode.Wildcard);
        }
    }
}