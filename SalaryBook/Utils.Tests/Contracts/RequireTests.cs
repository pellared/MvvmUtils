using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pellared.Utils.Contracts;
using System;

namespace Pellared.Utils.Tests.Contracts
{
    [TestClass]
    public class RequireTests
    {
        [TestMethod]
        public void Argument_LocalArgument_ResultHasGoodArgumentValue()
        {
            string argument = "value";
            ArgumentValidator<string> result = Require.Argument(() => argument);
            result.Argument.Value.Should().Be(argument);
        }

        [TestMethod]
        public void Argument_LocalArgumentHasValue_ResultHasGoodArgumentName()
        {
            string argument = "value";
            ArgumentValidator<string> result = Require.Argument(() => argument);
            result.Argument.Name.Should().Be("argument");
        }

        private string field = "value";

        [TestMethod]
        public void Argument_FieldArgumentHasValue_ResultHasGoodArgumentName()
        {
            ArgumentValidator<string> result = Require.Argument(() => field);
            result.Argument.Name.Should().Be("field");
        }

        [TestMethod]
        public void Argument_FieldArgument_ResultHasGoodArgumentValue()
        {
            ArgumentValidator<string> result = Require.Argument(() => field);
            result.Argument.Value.Should().Be(field);
        }

        private string Property { get { return "value"; } }

        [TestMethod]
        public void Argument_PropertyArgumentHasValue_ResultHasGoodArgumentName()
        {
            ArgumentValidator<string> result = Require.Argument(() => Property);
            result.Argument.Name.Should().Be("Property");
        }

        [TestMethod]
        public void Argument_PropertyArgument_ResultHasGoodArgumentValue()
        {
            ArgumentValidator<string> result = Require.Argument(() => Property);
            result.Argument.Value.Should().Be(Property);
        }

        private ArgumentValidator<string> ArgumentAsParameter(string parameter)
        {
            return Require.Argument(() => parameter);
        }

        [TestMethod]
        public void Argument_ParameterArgumentHasValue_ResultHasGoodArgumentName()
        {
            string argument = "value";
            ArgumentValidator<string> result = ArgumentAsParameter(argument);
            result.Argument.Name.Should().Be("parameter");
        }

        [TestMethod]
        public void Argument_ParameterArgument_ResultHasGoodArgumentValue()
        {
            string argument = "value";
            ArgumentValidator<string> result = ArgumentAsParameter(argument);
            result.Argument.Value.Should().Be(argument);
        }

        [TestMethod]
        public void Argument_ConstArgument_ThrowsException()
        {
            const string argument = "value";
            Action act = () => Require.Argument(() => argument);
            act.ShouldThrow<Exception>();
        }
    }
}