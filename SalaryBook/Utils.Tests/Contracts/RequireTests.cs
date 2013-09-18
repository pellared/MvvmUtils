using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Pellared.Utils.Contracts;

namespace Pellared.Utils.Tests.Contracts
{
    [TestClass]
    public class RequireTests
    {
        [TestMethod]
        public void Argument_ArgumentHasValue_ResultHasGoodArgumentValue()
        {
            string argument = "value";
            ArgumentValidation<string> result = Require.Argument(() => argument);
            result.Argument.Value.Should().Be(argument);
        }

        [TestMethod]
        public void Argument_ArgumentHasValue_ResultHasGoodArgumentName()
        {
            string argument = "value";
            ArgumentValidation<string> result = Require.Argument(() => argument);
            result.Argument.Name.Should().Be("argument");
        }
    }
}