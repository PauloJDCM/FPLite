﻿using FluentAssertions;
using FPLite.Either;
using Xunit;

namespace FPLite.Tests.Core;

public class EitherTests
{
    [Fact]
    public void GivenStringOrInt_WhenValueIsNeither_ShouldBeNeither()
    {
        var either = Either<string, int>.Neither();
        var check = either.Match(_ => false, _ => false, () => true, (_, _) => false);

        either.Type.Should().Be(EitherType.Neither);
        either.L.Should().BeNull();
        either.R.Should().Be(default);
        check.Should().BeTrue();
    }

    [Fact]
    public void GivenStringOrInt_WhenValueIsLeft_ShouldBeString()
    {
        var either = Either<string, int>.Left("test");

        either.Type.Should().Be(EitherType.Left);
        either.L.Should().Be("test");
        either.R.Should().Be(default);
    }

    [Fact]
    public void GivenStringOrInt_WhenValueIsRight_ShouldBeInt()
    {
        var either = Either<string, int>.Right(1);

        either.Type.Should().Be(EitherType.Right);
        either.L.Should().BeNull();
        either.R.Should().Be(1);
    }

    [Fact]
    public void GivenStringOrInt_WhenValueIsBoth_ShouldBeBoth()
    {
        var either = Either<string, int>.Both("test", 1);

        either.Type.Should().Be(EitherType.Both);
        either.L.Should().Be("test");
        either.R.Should().Be(1);
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
        var either = Either<string, int>.Neither();
        var result = either.Match(s => s, i => $"{i}", () => "Neither", (s, i) => $"{s}{i}");

        result.Should().Be("Neither");
    }

    [Fact]
    public void GivenLeft_WhenMatching_ShouldExecuteLeftAction()
    {
        var either = Either<string, int>.Left("test");
        var result = false;
        either.Match(_ => { result = true; }, _ => { }, () => { }, (_, _) => { });

        result.Should().BeTrue();
    }

    [Fact]
    public void GivenRight_WhenMatching_ShouldExecuteRightAction()
    {
        var either = Either<string, int>.Right(1);
        var result = false;
        either.Match(_ => { }, _ => { result = true; }, () => { }, (_, _) => { });

        result.Should().BeTrue();
    }

    [Fact]
    public void GivenBoth_WhenMatching_ShouldExecuteBothAction()
    {
        var either = Either<string, int>.Both("test", 1);
        var result = false;
        either.Match(_ => { }, _ => { }, () => { }, (_, _) => { result = true; });

        result.Should().BeTrue();
    }

    [Fact]
    public void GivenNeither_WhenMatching_ShouldExecuteNeitherAction()
    {
        var either = Either<string, int>.Neither();
        var result = false;
        either.Match(_ => { }, _ => { }, () => { result = true; }, (_, _) => { });

        result.Should().BeTrue();
    }

    [Fact]
    public void Given2Neither_WhenEquating_ShouldBeEqual()
    {
        var either = Either<string, int>.Neither();
        var other = Either<string, int>.Neither();

        either.Should().Be(other);
        either.Equals(other).Should().BeTrue();
        (either == other).Should().BeTrue();
        (either != other).Should().BeFalse();
    }

    [Fact]
    public void Given2EqualLeft_WhenEquating_ShouldBeEqual()
    {
        var either = Either<string, int>.Left("test");
        var other = Either<string, int>.Left("test");

        either.Should().Be(other);
        either.Equals(other).Should().BeTrue();
        (either == other).Should().BeTrue();
        (either != other).Should().BeFalse();
    }

    [Fact]
    public void Given2DifferentLeft_WhenEquating_ShouldNotBeEqual()
    {
        var either = Either<string, int>.Left("test");
        var other = Either<string, int>.Left("test2");

        either.Should().NotBe(other);
        either.Equals(other).Should().BeFalse();
        (either == other).Should().BeFalse();
        (either != other).Should().BeTrue();
    }

    [Fact]
    public void Given2EqualRight_WhenEquating_ShouldBeEqual()
    {
        var either = Either<string, int>.Right(1);
        var other = Either<string, int>.Right(1);

        either.Should().Be(other);
        either.Equals(other).Should().BeTrue();
        (either == other).Should().BeTrue();
        (either != other).Should().BeFalse();
    }

    [Fact]
    public void Given2EqualBoth_WhenEquating_ShouldBeEqual()
    {
        var either = Either<string, int>.Both("Test", 1);
        var other = Either<string, int>.Both("Test", 1);

        either.Should().Be(other);
        either.Equals(other).Should().BeTrue();
        (either == other).Should().BeTrue();
        (either != other).Should().BeFalse();
    }

    [Fact]
    public void GivenDifferentTypes_WhenEquating_ShouldNotBeEqual()
    {
        var both = Either<string, int>.Both("Test", 1);
        var left = Either<string, int>.Left("test");
        var right = Either<string, int>.Right(1);
        var neither = Either<string, int>.Neither();

        both.Should().NotBe(left);
        both.Equals(left).Should().BeFalse();
        (both == left).Should().BeFalse();
        (both != left).Should().BeTrue();

        both.Should().NotBe(right);
        both.Equals(right).Should().BeFalse();
        (both == right).Should().BeFalse();
        (both != right).Should().BeTrue();
        
        both.Should().NotBe(neither);
        both.Equals(neither).Should().BeFalse();
        (both == neither).Should().BeFalse();
        (both != neither).Should().BeTrue();

        left.Should().NotBe(right);
        left.Equals(right).Should().BeFalse();
        (left == right).Should().BeFalse();
        (left != right).Should().BeTrue();
        
        left.Should().NotBe(neither);
        left.Equals(neither).Should().BeFalse();
        (left == neither).Should().BeFalse();
        (left != neither).Should().BeTrue();
        
        right.Should().NotBe(neither);
        right.Equals(neither).Should().BeFalse();
        (right == neither).Should().BeFalse();
        (right != neither).Should().BeTrue();
    }
}