using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pellared.Common.Conditions;
using System;

namespace Pellared.Common.Tests.Contracts
{
    [TestClass]
    public class RequireTests
    {
        [TestMethod]
        public void Argument_LocalVariable_ResultHasGoodArgumentValue()
        {
            string argument = "value";
            Argument<string> result = Require.That(() => argument);
            result.Value.Should().Be(argument);
        }

        [TestMethod]
        public void Argument_LocalVariable_ResultHasGoodArgumentName()
        {
            string argument = "value";
            Argument<string> result = Require.That(() => argument);
            result.Name.Should().Be("argument");
        }

        private string field = "value";

        [TestMethod]
        public void Argument_Field_ResultHasGoodArgumentName()
        {
            Argument<string> result = Require.That(() => field);
            result.Name.Should().Be("field");
        }

        [TestMethod]
        public void Argument_Field_ResultHasGoodArgumentValue()
        {
            Argument<string> result = Require.That(() => field);
            result.Value.Should().Be(field);
        }

        private string Property { get { return "value"; } }

        [TestMethod]
        public void Argument_Property_ResultHasGoodArgumentName()
        {
            Argument<string> result = Require.That(() => Property);
            result.Name.Should().Be("Property");
        }

        [TestMethod]
        public void Argument_Property_ResultHasGoodArgumentValue()
        {
            Argument<string> result = Require.That(() => Property);
            result.Value.Should().Be(Property);
        }

        private Argument<string> ArgumentAsParameter(string parameter)
        {
            return Require.That(() => parameter);
        }

        [TestMethod]
        public void Argument_Parameter_ResultHasGoodArgumentName()
        {
            string argument = "value";
            Argument<string> result = ArgumentAsParameter(argument);
            result.Name.Should().Be("parameter");
        }

        [TestMethod]
        public void Argument_Parameter_ResultHasGoodArgumentValue()
        {
            string argument = "value";
            Argument<string> result = ArgumentAsParameter(argument);
            result.Value.Should().Be(argument);
        }

        private static string StaticField = "value";

        [TestMethod]
        public void Argument_StaticField_ResultHasGoodArgumentName()
        {
            Argument<string> result = Require.That(() => StaticField);
            result.Name.Should().Be("StaticField");
        }

        private string StaticProperty { get { return "value"; } }

        [TestMethod]
        public void Argument_StaticProperty_ResultHasGoodArgumentName()
        {
            Argument<string> result = Require.That(() => StaticProperty);
            result.Name.Should().Be("StaticProperty");
        }
    }
}