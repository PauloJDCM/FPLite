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

        [Theory]
        [InlineData(1, null, 1)]
        [InlineData(null, 10, 10)]
        [InlineData(2, 1000, 2000)]
        [InlineData(null, null, 0)]
        // Setting left parameter as int to avoid overflow exception in test theory
        public void Given2Types_WhenMatchReturnsMultipleTypes_ShouldExecuteCorrectFunc(int? left, int? right,
            int expected)
        {
            var either = Either<byte?, int?>.Both((byte?) left, right);
            var newEither = either.Match(
                b => (byte) b!,
                i => (int) i!,
                (b, i) => Either<byte, int>.Right((byte)b! * (int)i!)
            );
            
            var result = newEither.Match(b => b, i => i, () => 0, (b, i) => b * i);
            result.Should().Be(expected);
        }

        [Fact]
        public void GivenLeftBind_WhenValueIsNull_ShouldReturnEitherWithNeither()
        {
            var either = Either<string?, int?>.Left(null);
            var result = either.BindLeft(s => $"{s}test");

            result.Should().BeOfType<Either<string, int?>>();
            result.ToString().Should().Be("Neither");
        }

        [Fact]
        public void GivenRightBind_WhenValueIsNull_ShouldReturnEitherWithNeither()
        {
            var either = Either<string?, int?>.Right(null);
            var result = either.BindRight(i => (int) i! + 1);
            
            result.Should().BeOfType<Either<int, string>>();
            result.ToString().Should().Be("Neither");
        }

        [Fact]
        public void GivenBothBind_WhenValueIsNull_ShouldReturnOptionWithNone()
        {
            var either = Either<string?, int?>.Both(null, null);
            var result = either.BindBoth((s, i) => $"{s}test{i}");
            
            result.Should().BeOfType<Option<string>>();
            result.ToString().Should().Be("None");
        }

        [Fact]
        public void GivenLeftBind_WhenValueIsNotNull_ShouldReturnEitherWithLeft()
        {
            var either = Either<string?, int?>.Left("1");
            var result = either.BindLeft(s => $"{s}test");
            
            result.Should().BeOfType<Either<string, int?>>();
            result.ToString().Should().Be("Left(1test)");
        }
        
        [Fact]
        public void GivenRightBind_WhenValueIsNotNull_ShouldReturnEitherWithRight()
        {
            var either = Either<string?, int?>.Right(1);
            var result = either.BindRight(i => (int) i! + 1);
            
            result.Should().BeOfType<Either<int, string>>();
            result.ToString().Should().Be("Left(2)");
        }
        
        [Fact]
        public void GivenBothBind_WhenValueIsNotNull_ShouldReturnOptionWithSome()
        {
            var either = Either<string?, int?>.Both("1", 1);
            var result = either.BindBoth((s, i) => $"{s}test{i}");
            
            result.Should().BeOfType<Option<string>>();
            result.ToString().Should().Be("1test1");
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, 1)]
        [InlineData("test", null)]
        public void Given2Either_WhenValueIsEqual_ShouldBeEqual(string? left, int? right)
        {
            var either = Either<string?, int?>.Both(left, right);
            var other = Either<string?, int?>.Both(left, right);
            
            either.Should().Be(other);
            (either == other).Should().BeTrue();
            (either != other).Should().BeFalse();
        }
        
        [Theory]
        [InlineData(null, null)]
        [InlineData(null, 1)]
        [InlineData("test", null)]
        public void Given2Either_WhenValueIsNotEqual_ShouldNotBeEqual(string? left, int? right)
        {
            var either = Either<string?, int?>.Both(left, right);
            var other = Either<string?, int?>.Both("test", 1);
            
            either.Should().NotBe(other);
            (either == other).Should().BeFalse();
            (either != other).Should().BeTrue();
        }
    }
}