using System;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pellared.Common.Contracts;
using Pellared.Common.Conditions;

namespace Pellared.Common.Tests.Contracts
{
    [TestClass]
    public class ArgumentValidatonsTests_IsNotNull
    {
        [TestMethod]
        public void NoArguments_ArgumentIsNotNull_NoException()
        {
            Argument<string> validation = new Argument<string>("value", "argumentName");
            Action act = () => validation.IsNotNull();
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void NoArguments_ArgumentNull_ThrowsArgumentNullException()
        {
            Argument<string> validation = new Argument<string>(null, "argumentName");
            Action act = () => validation.IsNotNull();
            act.ShouldThrow<ArgumentNullException>();
        }
    }
}