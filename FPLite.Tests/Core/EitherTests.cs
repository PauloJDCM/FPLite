﻿using FluentAssertions;
using FPLite.Union;
using Xunit;

namespace FPLite.Tests.Core
{
    public class EitherTests
    {
        [Fact]
        public void GivenEither_WhenValueIsNeither_ShouldBeNeither()
        {
            var either = Either<string, int>.Neither;

            either.ToString().Should().Be("Neither");
            either.Type.Should().Be(EitherType.Neither);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("123")]
        public void GivenEither_WhenValueIsLeft_ShouldBeLeft(string left)
        {
            var either = Either<string, int>.Left(left);

            either.ToString().Should().Be($"Left({left})");
            either.Type.Should().Be(EitherType.Left);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(123)]
        public void GivenEither_WhenValueIsRight_ShouldBeRight(int right)
        {
            var either = Either<string, int>.Right(right);

            either.ToString().Should().Be($"Right({right})");
            either.Type.Should().Be(EitherType.Right);
        }

        [Theory]
        [InlineData("test", 1)]
        [InlineData("123", 123)]
        public void GivenEither_WhenValueIsBoth_ShouldBeBoth(string left, int right)
        {
            var either = Either<string, int>.Both(left, right);

            either.ToString().Should().Be($"Both({left}, {right})");
            either.Type.Should().Be(EitherType.Both);
        }

        [Theory]
        [InlineData(1, null)]
        [InlineData(null, 1)]
        [InlineData(null, null)]
        public void GivenBoth_WhenAnyValueIsNull_ShouldNotBeBoth(int? left, int? right)
        {
            var either = Either<int?, int?>.Both(left, right);
            
            either.Type.Should().NotBe(EitherType.Both);
        }
        
        [Fact]
        public void GivenLeft_WhenMatching_ShouldExecuteLeftFunction()
        {
            var either = Either<string, int>.Left("test");
            var result = either.Match(s => s, i => $"{i}", () => "", (s, i) => $"{s}{i}");
            
            result.Should().Be("test");
        }

        [Fact]
        public void GivenRight_WhenMatching_ShouldExecuteRightFunction()
        {
            var either = Either<string, int>.Right(1);
            var result = either.Match(s => s, i => $"{i}", () => "", (s, i) => $"{s}{i}");

            result.Should().Be("1");
        }

        [Fact]
        public void GivenBoth_WhenMatching_ShouldExecuteBothFunction()
        {
            var either = Either<string, int>.Both("test", 1);
            var result = either.Match(s => s, i => $"{i}", () => "", (s, i) => $"{s}{i}");
            
            result.Should().Be("test1");
        }

        [Fact]
        public void GivenNeither_WhenMatching_ShouldExecuteNeitherFunction()
        {
            var either = Either<string, int>.Neither;
            var result = either.Match(s => s, i => $"{i}", () => "", (s, i) => $"{s}{i}");

            result.Should().Be("");
        }

        [Fact]
        public void GivenLeft_WhenMatching_ShouldExecuteLeftAction()
        {
            var either = Either<string, int>.Left("test");
            var result = false;
            either.Match(_ => { result = true; }, _ => { }, () => { }, (s, i) => { });

            result.Should().BeTrue();
        }

        [Fact]
        public void GivenRight_WhenMatching_ShouldExecuteRightAction()
        {
            var either = Either<string, int>.Right(1);
            var result = false;
            either.Match(_ => { }, _ => { result = true; }, () => { }, (s, i) => { });

            result.Should().BeTrue();
        }

        [Fact]
        public void GivenBoth_WhenMatching_ShouldExecuteBothAction()
        {
            var either = Either<string, int>.Both("test", 1);
            var result = false;
            either.Match(_ => { }, _ => { }, () => { }, (s, i) => { result = true; });

            result.Should().BeTrue();
        }

        [Fact]
        public void GivenNeither_WhenMatching_ShouldExecuteNeitherAction()
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
        public void GivenBoth_WhenMatchReturnsMultipleTypes_ShouldExecuteCorrectFunc(int? left, int? right,
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
        public void GivenNull_WhenBindingLeft_ShouldReturnEitherWithNeither()
        {
            var either = Either<string?, int?>.Left(null);
            var result = either.BindLeft(s => $"{s}test");

            result.Should().BeOfType<Either<string, int?>>();
            result.ToString().Should().Be("Neither");
        }

        [Fact]
        public void GivenNull_WhenBindingRight_ShouldReturnEitherWithNeither()
        {
            var either = Either<string?, int?>.Right(null);
            var result = either.BindRight(i => (int) i! + 1);
            
            result.Should().BeOfType<Either<int, string>>();
            result.ToString().Should().Be("Neither");
        }

        [Fact]
        public void GivenNull_WhenBindingBoth_ShouldReturnUnionWithNeither()
        {
            var either = Either<string?, int?>.Both(null, null);
            var result = either.BindBoth((s, i) => $"{s}test{i}");

            result.Should().BeOfType<Union<string, string?, int?>>();
            result.Type.Should().Be(0);
        }

        [Fact]
        public void GivenLeft_WhenBindingLeft_ShouldReturnEitherWithLeft()
        {
            var either = Either<string?, int?>.Left("1");
            var result = either.BindLeft(s => $"{s}test");
            
            result.Should().BeOfType<Either<string, int?>>();
            result.ToString().Should().Be("Left(1test)");
        }
        
        [Fact]
        public void GivenRight_WhenBindingRight_ShouldReturnEitherWithRight()
        {
            var either = Either<string?, int?>.Right(1);
            var result = either.BindRight(i => (int) i! + 1);
            
            result.Should().BeOfType<Either<int, string>>();
            result.ToString().Should().Be("Left(2)");
        }
        
        [Fact]
        public void GivenBoth_WhenBindingBoth_ShouldReturnUnionWithT1()
        {
            var either = Either<string?, int?>.Both("1", 1);
            var result = either.BindBoth((s, i) => $"{s}test{i}");
            
            result.Should().BeOfType<Union<string, string?, int?>>();
            result.Type.Should().Be(1);
        }
        
        [Fact]
        public void GivenLeft_WhenBindingBoth_ShouldReturnUnionWithT2()
        {
            var either = Either<string?, int?>.Left("1");
            var result = either.BindBoth((s, i) => $"{s}test{i}");
            
            result.Should().BeOfType<Union<string, string?, int?>>();
            result.Type.Should().Be(2);
        }
        
        [Fact]
        public void GivenRight_WhenBindingBoth_ShouldReturnUnionWithT3()
        {
            var either = Either<string?, int?>.Right(1);
            var result = either.BindBoth((s, i) => $"{s}test{i}");
            
            result.Should().BeOfType<Union<string, string?, int?>>();
            result.Type.Should().Be(3);
        }
        
        [Fact]
        public void GivenEither_WhenValueIsNeither_ShouldBeEqual()
        {
            var either = Either<string?, int?>.Neither;
            var other = Either<string?, int?>.Neither;
            
            either.Should().Be(other);
            (either == other).Should().BeTrue();
            (either != other).Should().BeFalse();
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