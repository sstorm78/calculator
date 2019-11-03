using CommandCalculator.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CommandCalculator.Tests.Models
{
    [TestFixture()]
    public class ValidationResultShould
    {
        [Test]
        public void ValidBuilderShouldReturnValidInstance()
        {
            var result = new ValidationResult().Valid();

            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void InvalidBuilderShouldReturnValidInstance()
        {
            var result = new ValidationResult().Invalid("test");

            result.IsValid.Should().BeFalse();
            result.Message.Should().Be("test");
        }
    }
}
