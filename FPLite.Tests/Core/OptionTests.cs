using FluentAssertions;
using Xunit;

namespace FPLite.Tests.Core;

public class OptionTests
{
    [Fact]
    public void GivenInt_WhenValueIsNone_ShouldBeNone()
    {
        var option = FPLite.None<string>();
        var result = option.Match(_ => false, () => true);
            
        option.IsSome.Should().BeFalse();
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    public void GivenInt_WhenValueIsSome_ShouldReturnValue(int originalValue)
    {
        var option = FPLite.Some(originalValue);
        var value = option.Match(i => i, () => originalValue - 1);

        option.IsSome.Should().BeTrue();
        value.Should().Be(originalValue);
    }

    [Fact]
    public void GivenNone_WhenMatching_ShouldExecuteNone()
    {
        var option = FPLite.None<int>();
        var result = false;
        option.Match(_ => { }, () => result = true);

        result.Should().BeTrue();
    }

    [Fact]
    public void GivenSome_WhenMatching_ShouldExecuteSome()
    {
        var option = FPLite.Some(1);
        var result = false;
        option.Match(_ => result = true, () => { });

        result.Should().BeTrue();
    }

    [Fact]
    public void GivenNone_WhenBinding_ShouldNotBind()
    {
        var option = FPLite.None<int>();
        var bind = option.Bind(i => i + i);
        var result = bind.Match(i => i, () => 0);

        bind.IsSome.Should().BeFalse();
        result.Should().Be(0);
    }

    [Fact]
    public void GivenSome_WhenBinding_ShouldBind()
    {
        var option = FPLite.Some(1);
        var bind = option.Bind(i => i + i);
        var result = bind.Match(i => i, () => 0);
            
        bind.IsSome.Should().BeTrue();
        result.Should().Be(2);
    }

    [Fact]
    public void Given2None_WhenEquating_ShouldBeEqual()
    {
        var option = FPLite.None<int>();
        var other = FPLite.None<int>();

        option.Should().Be(other);
        option.Equals(other).Should().BeTrue();
    }

    [Fact]
    public void Given2EqualSome_WhenEquating_ShouldBeEqual()
    {
        var option = FPLite.Some(1);
        var other = FPLite.Some(1);

        option.Should().Be(other);
        option.Equals(other).Should().BeTrue();
    }
    
    [Fact]
    public void Given2DifferentSome_WhenEquating_ShouldNotBeEqual()
    {
        var option = FPLite.Some(1);
        var other = FPLite.Some(2);
        
        option.Should().NotBe(other);
        option.Equals(other).Should().BeFalse();
    }
    
    [Fact]
    public void GivenSomeAndNone_WhenEquating_ShouldBeNotBeEqual()
    {
        var option = FPLite.Some(1);
        var other = FPLite.None<int>();
        
        option.Should().NotBe(other);
        option.Equals(other).Should().BeFalse();
    }

    [Fact]
    public void GivenNone_WhenUnwrapping_ShouldThrow()
    {
        var option = FPLite.None<int>();
        Assert.ThrowsAny<UnwrapException>(() => option.Unwrap());
    }

    [Fact]
    public void GivenSome_WhenUnwrapping_ShouldReturnValue()
    {
        var option = FPLite.Some(1);
        option.Unwrap().Should().Be(1);
    }
    
    [Fact]
    public void GivenNone_WhenUnwrappingOrWithSameType_ShouldReturnSomeWithOrValue()
    {
        var option = FPLite.None<int>();
        option.UnwrapOr(() => 2).Should().Be(2);
    }
    
    [Fact]
    public void GivenSome_WhenUnwrappingOrWithSameType_ShouldReturnValue()
    {
        var option = FPLite.Some(1);
        option.UnwrapOr(() => 2).Should().Be(1);
    }
    
    [Fact]
    public void GivenNone_WhenUnwrappingOrWithDifferentType_ShouldReturnSomeWithOrValue()
    {
        var option = FPLite.None<int>();
        var unwrap = option.UnwrapOr(() => "2");
        var result = unwrap.Match(i => i.ToString(), s => s);

        unwrap.Type.Should().Be(UnionType.T2);
        result.Should().Be("2");
    }
    
    [Fact]
    public void GivenSome_WhenUnwrappingOrWithDifferentType_ShouldReturnValue()
    {
        var option = FPLite.Some(1);
        var unwrap = option.UnwrapOr(() => "2");
        var result = unwrap.Match(i => i.ToString(), s => s);
        
        unwrap.Type.Should().Be(UnionType.T1);
        result.Should().Be("1");
    }
}