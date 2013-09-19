using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pellared.Utils.Contracts;
using System;

namespace Pellared.Utils.Tests.Contracts
{
    [TestClass]
    public class CheckTests
    {
        [TestMethod]
        public void Argument_LocalArgument_ResultHasGoodArgumentValue()
        {
            string argument = "value";
            Argument<string> result = Check.Argument(() => argument);
            result.Value.Should().Be(argument);
        }

        [TestMethod]
        public void Argument_LocalArgumentHasValue_ResultHasGoodArgumentName()
        {
            string argument = "value";
            Argument<string> result = Check.Argument(() => argument);
            result.Name.Should().Be("argument");
        }

        private string field = "value";

        [TestMethod]
        public void Argument_FieldArgumentHasValue_ResultHasGoodArgumentName()
        {
            Argument<string> result = Check.Argument(() => field);
            result.Name.Should().Be("field");
        }

        [TestMethod]
        public void Argument_FieldArgument_ResultHasGoodArgumentValue()
        {
            Argument<string> result = Check.Argument(() => field);
            result.Value.Should().Be(field);
        }

        private string Property { get { return "value"; } }

        [TestMethod]
        public void Argument_PropertyArgumentHasValue_ResultHasGoodArgumentName()
        {
            Argument<string> result = Check.Argument(() => Property);
            result.Name.Should().Be("Property");
        }

        [TestMethod]
        public void Argument_PropertyArgument_ResultHasGoodArgumentValue()
        {
            Argument<string> result = Check.Argument(() => Property);
            result.Value.Should().Be(Property);
        }

        private Argument<string> ArgumentAsParameter(string parameter)
        {
            return Check.Argument(() => parameter);
        }

        [TestMethod]
        public void Argument_ParameterArgumentHasValue_ResultHasGoodArgumentName()
        {
            string argument = "value";
            Argument<string> result = ArgumentAsParameter(argument);
            result.Name.Should().Be("parameter");
        }

        [TestMethod]
        public void Argument_ParameterArgument_ResultHasGoodArgumentValue()
        {
            string argument = "value";
            Argument<string> result = ArgumentAsParameter(argument);
            result.Value.Should().Be(argument);
        }

        [TestMethod]
        public void Argument_ConstArgument_ThrowsException()
        {
            const string argument = "value";
            Action act = () => Check.Argument(() => argument);
            act.ShouldThrow<Exception>();
        }
    }
}