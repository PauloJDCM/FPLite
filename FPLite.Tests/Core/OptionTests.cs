using FluentAssertions;
using FPLite.Option;
using FPLite.Union;
using Xunit;

namespace FPLite.Tests.Core;

public class OptionTests
{
    [Fact]
    public void GivenInt_WhenValueIsNone_ShouldBeNone()
    {
        var option = Option<int>.None();
        var result = option.Match(_ => false, () => true);

        option.Type.Should().Be(OptionType.None);
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    public void GivenInt_WhenValueIsSome_ShouldReturnValue(int originalValue)
    {
        var option = Option<int>.Some(originalValue);
        var value = option.Match(i => i, () => originalValue - 1);

        option.Type.Should().Be(OptionType.Some);
        value.Should().Be(originalValue);
    }

    [Fact]
    public void GivenNone_WhenMatching_ShouldExecuteNone()
    {
        var option = Option<int>.None();
        var result = false;
        option.Match(_ => { }, () => result = true);

        result.Should().BeTrue();
    }

    [Fact]
    public void GivenSome_WhenMatching_ShouldExecuteSome()
    {
        var option = Option<int>.Some(1);
        var result = false;
        option.Match(_ => result = true, () => { });

        result.Should().BeTrue();
    }

    [Fact]
    public void GivenNone_WhenBinding_ShouldNotBind()
    {
        var option = Option<int>.None();
        var bind = option.Bind(i => i + i);
        var result = bind.Match(i => i, () => 0);

        bind.Type.Should().Be(OptionType.None);
        result.Should().Be(0);
    }

    [Fact]
    public void GivenSome_WhenBinding_ShouldBind()
    {
        var option = Option<int>.Some(1);
        var bind = option.Bind(i => i + i);
        var result = bind.Match(i => i, () => 0);

        bind.Type.Should().Be(OptionType.Some);
        result.Should().Be(2);
    }

    [Fact]
    public void Given2None_WhenEquating_ShouldBeEqual()
    {
        var option = Option<int>.None();
        var other = Option<int>.None();

        option.Should().Be(other);
        option.Equals(other).Should().BeTrue();
    }

    [Fact]
    public void Given2EqualSome_WhenEquating_ShouldBeEqual()
    {
        var option = Option<int>.Some(1);
        var other = Option<int>.Some(1);

        option.Should().Be(other);
        option.Equals(other).Should().BeTrue();
    }

    [Fact]
    public void Given2DifferentSome_WhenEquating_ShouldNotBeEqual()
    {
        var option = Option<int>.Some(1);
        var other = Option<int>.Some(2);

        option.Should().NotBe(other);
        option.Equals(other).Should().BeFalse();
    }

    [Fact]
    public void GivenSomeAndNone_WhenEquating_ShouldBeNotBeEqual()
    {
        var option = Option<int>.Some(1);
        var other = Option<int>.None();

        option.Should().NotBe(other);
        option.Equals(other).Should().BeFalse();
    }

    [Fact]
    public void GivenNone_WhenUnwrapping_ShouldThrow()
    {
        var option = Option<int>.None();
        Assert.ThrowsAny<UnwrapException>(() => option.Unwrap());
        Assert.Throws<OptionUnwrapException<int>>(() => option.Unwrap());
    }

    [Fact]
    public void GivenSome_WhenUnwrapping_ShouldReturnValue()
    {
        var option = Option<int>.Some(1);
        option.Unwrap().Should().Be(1);
    }

    [Fact]
    public void GivenNone_WhenUnwrappingOrWithSameType_ShouldReturnSomeWithOrValue()
    {
        var option = Option<int>.None();
        option.UnwrapOr(() => 2).Should().Be(2);
    }

    [Fact]
    public void GivenSome_WhenUnwrappingOrWithSameType_ShouldReturnValue()
    {
        var option = Option<int>.Some(1);
        option.UnwrapOr(() => 2).Should().Be(1);
    }

    [Fact]
    public void GivenNone_WhenUnwrappingOrWithDifferentType_ShouldReturnSomeWithOrValue()
    {
        var option = Option<int>.None();
        var unwrap = option.UnwrapOr(() => "2");
        var result = unwrap.Match(i => i.ToString(), s => s);

        unwrap.Type.Should().Be(UnionType.T2);
        result.Should().Be("2");
    }

    [Fact]
    public void GivenSome_WhenUnwrappingOrWithDifferentType_ShouldReturnValue()
    {
        var option = Option<int>.Some(1);
        var unwrap = option.UnwrapOr(() => "2");
        var result = unwrap.Match(i => i.ToString(), s => s);

        unwrap.Type.Should().Be(UnionType.T1);
        result.Should().Be("1");
    }
}