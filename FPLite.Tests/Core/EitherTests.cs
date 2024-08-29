using FluentAssertions;
using Xunit;

namespace FPLite.Tests.Core;

public class EitherTests
{
    [Fact]
    public void GivenStringOrInt_WhenValueIsNeither_ShouldBeNeither()
    {
        var either = FPLite.Neither<string, int>();
        var check = either.Match(_ => false, _ => false, () => true, (_, _) => false);

        either.Type.Should().Be(EitherType.Neither);
        check.Should().BeTrue();
    }

    [Fact]
    public void GivenStringOrInt_WhenValueIsLeft_ShouldBeString()
    {
        var either = FPLite.Left<string, int>("test");
        var check = either.Match(_ => true, _ => false, () => false, (_, _) => false);

        either.Type.Should().Be(EitherType.Left);
        check.Should().BeTrue();
    }

    [Fact]
    public void GivenStringOrInt_WhenValueIsRight_ShouldBeInt()
    {
        var either = FPLite.Right<string, int>(1);
        var check = either.Match(_ => false, _ => true, () => false, (_, _) => false);

        either.Type.Should().Be(EitherType.Right);
        check.Should().BeTrue();
    }

    [Fact]
    public void GivenStringOrInt_WhenValueIsBoth_ShouldBeBoth()
    {
        var either = FPLite.Both("test", 1);
        var check = either.Match(_ => false, _ => false, () => false, (_, _) => true);

        either.Type.Should().Be(EitherType.Both);
        check.Should().BeTrue();
    }

    [Fact]
    public void GivenLeft_WhenMatching_ShouldExecuteLeftFunction()
    {
        var either = FPLite.Left<string, int>("test");
        var result = either.Match(s => s, i => $"{i}", () => "", (s, i) => $"{s}{i}");

        result.Should().Be("test");
    }

    [Fact]
    public void GivenRight_WhenMatching_ShouldExecuteRightFunction()
    {
        var either = FPLite.Right<string, int>(1);
        var result = either.Match(s => s, i => $"{i}", () => "", (s, i) => $"{s}{i}");

        result.Should().Be("1");
    }

    [Fact]
    public void GivenBoth_WhenMatching_ShouldExecuteBothFunction()
    {
        var either = FPLite.Both("test", 1);
        var result = either.Match(s => s, i => $"{i}", () => "", (s, i) => $"{s}{i}");

        result.Should().Be("test1");
    }

    [Fact]
    public void GivenNeither_WhenMatching_ShouldExecuteNeitherFunction()
    {
        var either = FPLite.Neither<string, int>();
        var result = either.Match(s => s, i => $"{i}", () => "Neither", (s, i) => $"{s}{i}");

        result.Should().Be("Neither");
    }

    [Fact]
    public void GivenLeft_WhenMatching_ShouldExecuteLeftAction()
    {
        var either = FPLite.Left<string, int>("test");
        var result = false;
        either.Match(_ => { result = true; }, _ => { }, () => { }, (_, _) => { });

        result.Should().BeTrue();
    }

    [Fact]
    public void GivenRight_WhenMatching_ShouldExecuteRightAction()
    {
        var either = FPLite.Right<string, int>(1);
        var result = false;
        either.Match(_ => { }, _ => { result = true; }, () => { }, (_, _) => { });

        result.Should().BeTrue();
    }

    [Fact]
    public void GivenBoth_WhenMatching_ShouldExecuteBothAction()
    {
        var either = FPLite.Both("test", 1);
        var result = false;
        either.Match(_ => { }, _ => { }, () => { }, (_, _) => { result = true; });

        result.Should().BeTrue();
    }

    [Fact]
    public void GivenNeither_WhenMatching_ShouldExecuteNeitherAction()
    {
        var either = FPLite.Neither<string, int>();
        var result = false;
        either.Match(_ => { }, _ => { }, () => { result = true; }, (_, _) => { });

        result.Should().BeTrue();
    }

    [Fact]
    public void Given2Neither_WhenEquating_ShouldBeEqual()
    {
        var either = FPLite.Neither<string, int>();
        var other = FPLite.Neither<string, int>();

        either.Should().Be(other);
        either.Equals(other).Should().BeTrue();
    }

    [Fact]
    public void Given2EqualLeft_WhenEquating_ShouldBeEqual()
    {
        var either = FPLite.Left<string, int>("test");
        var other = FPLite.Left<string, int>("test");

        either.Should().Be(other);
        either.Equals(other).Should().BeTrue();
    }

    [Fact]
    public void Given2DifferentLeft_WhenEquating_ShouldNotBeEqual()
    {
        var either = FPLite.Left<string, int>("test");
        var other = FPLite.Left<string, int>("test2");

        either.Should().NotBe(other);
        either.Equals(other).Should().BeFalse();
    }

    [Fact]
    public void Given2EqualRight_WhenEquating_ShouldBeEqual()
    {
        var either = FPLite.Right<string, int>(1);
        var other = FPLite.Right<string, int>(1);

        either.Should().Be(other);
        either.Equals(other).Should().BeTrue();
    }

    [Fact]
    public void Given2EqualBoth_WhenEquating_ShouldBeEqual()
    {
        var either = FPLite.Both("Test", 1);
        var other = FPLite.Both("Test", 1);

        either.Should().Be(other);
        either.Equals(other).Should().BeTrue();
    }

    [Fact]
    public void Given2DifferentTypes_WhenEquating_ShouldNotBeEqual()
    {
        var either = FPLite.Both("Test", 1);
        var other = FPLite.Left<string, int>("test");

        either.Should().NotBe(other);
        either.Equals(other).Should().BeFalse();
    }
}