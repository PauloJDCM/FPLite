using FluentAssertions;
using FPLite;

namespace TestProject1.Core;

public class OptionTests
{
    [Fact]
    public void GivenIntType_WhenValueIsNone_ShouldBeNone()
    {
        var option = Option<int>.None;
        var value = option.Match(_ => true, () => false);
        
        option.ToString().Should().Be("None");
        value.Should().BeFalse();
    }
    
    [Fact]
    public void GivenIntType_WhenValueIsNull_ShouldBeNone()
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
    public void GivenIntType_WhenValueIsSome_ShouldReturnValue(int originalValue)
    {
        var option = Option<int>.Some(originalValue);
        var value = option.Match(i => i, () => originalValue - 1);
        
        value.Should().Be(originalValue);
    }
}