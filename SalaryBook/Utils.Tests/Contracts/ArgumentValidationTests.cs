using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pellared.Utils.Contracts;

using FluentAssertions;

namespace Pellared.Utils.Tests.Contracts
{
    [TestClass]
    public class ArgumentValidationTests
    {
        [TestMethod]
        public void IsNotNull_ArgumentIsNotNull_NoException()
        {
            ArgumentValidation<string> validation = new ArgumentValidation<string>(new Argument<string>("value", "argumentName"));
            Action act = () => validation.IsNotNull();
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void IsNotNull_WhenArgumentNull_ThrowsArgumentNullException()
        {
            ArgumentValidation<string> validation = new ArgumentValidation<string>(new Argument<string>(null, "argumentName"));
            Action act = () => validation.IsNotNull();
            act.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void Is_WhenConditionPasses_NoException()
        {
            ArgumentValidation<int> validation = new ArgumentValidation<int>(new Argument<int>(1, "argumentName"));
            Action act = () => validation.Is(x => x > 0);
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void Is_WhenConditionFails_ThrowsArgumentException()
        {
            ArgumentValidation<int> validation = new ArgumentValidation<int>(new Argument<int>(1, "argumentName"));
            Action act = () => validation.Is(x => x < 0);
            act.ShouldThrow<ArgumentException>();
        }  
    }
}