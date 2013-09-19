﻿using System;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pellared.Utils.Contracts;

namespace Pellared.Utils.Tests.Contracts.ArgumentExtensions
{
    [TestClass]
    public class ArgumentTests_IsNotNull
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

        [TestMethod]
        public void ExceptionDelegate_ArgumentNull_ThrowsGivenException()
        {
            Argument<string> validation = new Argument<string>(null, "argumentName");
            Action act = () => validation.IsNotNull(arg => new InvalidOperationException());
            act.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public void ConditionDescrption_ArgumentNull_FormatedConditionDescriptionInExceptionMessage()
        {
            Argument<string> validation = new Argument<string>(null, "argumentName");
            const string conditionDescription = "{0} is {1}";

            Action act = () => validation.IsNotNull(conditionDescription);
            act.ShouldThrow<Exception>().WithMessage("argumentName is ", ComparisonMode.EquivalentSubstring);
        }
    }
}