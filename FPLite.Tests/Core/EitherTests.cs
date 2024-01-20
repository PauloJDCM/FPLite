using FluentAssertions;
using Xunit;

namespace FPLite.Tests.Core
{
    public class EitherTests
    {
        [Fact]
        public void Given2Types_WhenValueIsNeither_ShouldBeNeither()
        {
            var either = Either<string, int>.Neither;
            var result = either.Match(_ => false, _ => false, () => true, (s, i) => false);

            result.Should().BeTrue();
            either.ToString().Should().Be("Neither");
        }

        [Theory]
        [InlineData("test")]
        [InlineData("123")]
        public void Given2Types_WhenValueIsLeft_ShouldBeLeft(string left)
        {
            var either = Either<string, int>.Left(left);
            var result = either.Match(_ => true, _ => false, () => false, (s, i) => false);

            result.Should().BeTrue();
            either.ToString().Should().Be($"Left({left})");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(123)]
        public void Given2Types_WhenValueIsRight_ShouldBeRight(int right)
        {
            var either = Either<string, int>.Right(right);
            var result = either.Match(_ => false, _ => true, () => false, (s, i) => false);

            result.Should().BeTrue();
            either.ToString().Should().Be($"Right({right})");
        }

        [Theory]
        [InlineData("test", 1)]
        [InlineData("123", 123)]
        public void Given2Types_WhenValueIsBoth_ShouldBeBoth(string left, int right)
        {
            var either = Either<string, int>.Both(left, right);
            var result = either.Match(_ => false, _ => false, () => false, (s, i) => true);

            result.Should().BeTrue();
            either.ToString().Should().Be($"Both({left}, {right})");
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        [InlineData(null, null)]
        public void Given1Type_WhenEitherIsBothAnd1ValueIsNull_ShouldNotBeBoth(int? left, int? right)
        {
            var either = Either<int?, int?>.Both(left, right);
            var result = either.Match(_ => false, _ => false, () => false, (s, i) => true);

            result.Should().BeFalse();
        }

        [Fact]
        public void Given2Types_WhenValueIsLeft_ShouldExecuteLeftFunc()
        {
            var either = Either<string, int>.Left("test");
            var result = false;
            either.Match(_ => { result = true; }, _ => { }, () => { }, (s, i) => { });

            result.Should().BeTrue();
        }

        [Fact]
        public void Given2Types_WhenValueIsRight_ShouldExecuteRightFunc()
        {
            var either = Either<string, int>.Right(1);
            var result = false;
            either.Match(_ => { }, _ => { result = true; }, () => { }, (s, i) => { });

            result.Should().BeTrue();
        }
    
        [Fact]
        public void Given2Types_WhenValueIsBoth_ShouldExecuteBothFunc()
        {
            var either = Either<string, int>.Both("test", 1);
            var result = false;
            either.Match(_ => { }, _ => { }, () => { }, (s, i) => { result = true; });
        
            result.Should().BeTrue();
        }
    
        [Fact]
        public void Given2Types_WhenValueIsNeither_ShouldExecuteNeitherFunc()
        {
            var either = Either<string, int>.Neither;
            var result = false;
            either.Match(_ => { }, _ => { }, () => { result = true; }, (s, i) => { });
        
            result.Should().BeTrue();
        }
    }
}