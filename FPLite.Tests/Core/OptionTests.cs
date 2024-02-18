using FluentAssertions;
using Xunit;

namespace FPLite.Tests.Core
{
    public class OptionTests
    {
        [Fact]
        public void GivenValueType_WhenValueIsNone_ShouldBeNone()
        {
            var option = Option<int>.None;
            var value = option.Match(_ => true, () => false);

            option.ToString().Should().Be("None");
            value.Should().BeFalse();
        }

        [Fact]
        public void GivenNullableValueType_WhenValueIsNull_ShouldBeNone()
        {
            var option = Option<int?>.Some(null);
            var value = option.Match(_ => true, () => false);

            option.ToString().Should().Be("None");
            value.Should().BeFalse();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenValueType_WhenValueIsSome_ShouldReturnValue(int originalValue)
        {
            var option = Option<int>.Some(originalValue);
            var value = option.Match(i => i, () => originalValue - 1);

            value.Should().Be(originalValue);
        }

        [Fact]
        public void GivenReferenceType_WhenValueIsNone_ShouldBeNone()
        {
            var option = Option<string>.None;
            var value = option.Match(_ => true, () => false);

            option.ToString().Should().Be("None");
            value.Should().BeFalse();
        }

        [Theory]
        [InlineData("test")]
        [InlineData("123")]
        public void GivenReferenceType_WhenValueIsSome_ShouldReturnValue(string originalValue)
        {
            var option = Option<string>.Some(originalValue);
            var value = option.Match(s => s, () => string.Empty);

            value.Should().Be(originalValue);
        }

        [Fact]
        public void GivenNullableReferenceType_WhenValueIsNone_ShouldBeNone()
        {
            var option = Option<string?>.None;
            var value = option.Match(_ => true, () => false);

            option.ToString().Should().Be("None");
            value.Should().BeFalse();
        }

        [Theory]
        [InlineData("test")]
        [InlineData("123")]
        public void GivenNullableReferenceType_WhenValueIsSome_ShouldReturnValue(string originalValue)
        {
            var option = Option<string?>.Some(originalValue);
            var value = option.Match(s => s, () => string.Empty);

            value.Should().Be(originalValue);
        }

        [Fact]
        public void GivenReferenceType_WhenValueIsNone_ShouldExecuteNone()
        {
            var option = Option<string>.None;
            var result = false;
            option.Match(_ => result = true, () => { });

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("test")]
        [InlineData("123")]
        public void GivenReferenceType_WhenValueIsSome_ShouldExecuteSome(string originalValue)
        {
            var option = Option<string>.Some(originalValue);
            var result = string.Empty;
            option.Match(s => result = s, () => { });

            result.Should().Be(originalValue);
        }

        [Fact]
        public void GivenReferenceType_WhenValueIsNone_ShouldNotBind()
        {
            var option = Option<string>.None;
            var bind = option.Bind(s => s + "test");
        
            bind.ToString().Should().Be("None");
        }

        [Theory]
        [InlineData("test")]
        [InlineData("123")]
        public void GivenReferenceType_WhenValueIsSome_ShouldBind(string originalValue)
        {
            var option = Option<string>.Some(originalValue);
            var bind = option.Bind(s => s + "test");
        
            bind.ToString().Should().Be(originalValue + "test");
        }

        [Fact]
        public void Given2DifferentTypes_WhenValuesMatch_ShouldNotBeEqual()
        {
            var option = Option<string>.Some("test");
            var other = Option<int>.Some(1);
        
            option.Should().NotBe(other);
        }

        [Fact]
        public void Given2EqualTypes_WhenValuesDontMatch_ShouldNotBeEqual()
        {
            var option = Option<string>.Some("test");
            var other = Option<string>.Some("test2");
        
            option.Should().NotBe(other);
            (option == other).Should().BeFalse();
            (option != other).Should().BeTrue();
        }

        [Fact]
        public void Given2EqualTypes_WhenValuesMatch_ShouldBeEqual()
        {
            var option = Option<string>.Some("test");
            var other = Option<string>.Some("test");
        
            option.Should().Be(other);
            (option == other).Should().BeTrue();
            (option != other).Should().BeFalse();
        }
        
        [Fact]
        public void GivenSomeValue_WhenUnwrapping_ShouldReturnValue()
        {
            var option = Option<string>.Some("test");
            option.Unwrap().Should().Be("test");
        }
        
        [Fact]
        public void GivenNoneValue_WhenUnwrapping_ShouldThrowException()
        {
            var option = Option<string>.None;
            Assert.Throws<OptionUnwrapException<string>>(() => option.Unwrap());
        }
        
        [Fact]
        public void GivenSomeValue_WhenUnwrappingOr_ShouldReturnValue()
        {
            var option = Option<string>.Some("test");
            option.UnwrapOr("default").Should().Be("test");
        }
        
        [Fact]
        public void GivenNoneValue_WhenUnwrappingOr_ShouldReturnDefault()
        {
            var option = Option<string>.None;
            option.UnwrapOr("default").Should().Be("default");
        }
    }
}