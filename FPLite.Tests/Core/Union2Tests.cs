using FluentAssertions;
using FPLite.Union;
using Xunit;

namespace FPLite.Tests.Core;

public class Union2Tests
{
    [Fact]
    public void GivenStringOrInt_WhenValueIsT1_ShouldReturnString()
    {
        var either = Union<string, int>.U1("test");
        
        either.Type.Should().Be(UnionType.T1);
        either.V1.Should().Be("test");
        either.V2.Should().Be(default);
    }
    
    [Fact]
    public void GivenStringOrInt_WhenValueIsT2_ShouldReturnInt()
    {
        var either = Union<string, int>.U2(42);
        
        either.Type.Should().Be(UnionType.T2);
        either.V1.Should().BeNull();
        either.V2.Should().Be(42);
    }
    
    [Fact]
    public void GivenT1_WhenMatching_ShouldDoT1Action()
    {
        var either = Union<string, int>.U1("test");
        var result = false;
        either.Match(_ => { result = true; }, _ => { });
        
        result.Should().BeTrue();
    }
    
    [Fact]
    public void GivenT2_WhenMatching_ShouldDoT2Action()
    {
        var either = Union<string, int>.U2(42);
        var result = false;
        either.Match(_ => { }, _ => { result = true; });
        
        result.Should().BeTrue();
    }
    
    [Fact]
    public void GivenT1_WhenMatching_ShouldReturnT1()
    {
        var either = Union<string, int>.U1("test");
        var result = either.Match(_ => true, _ => false);
        
        result.Should().BeTrue();
    }
    
    [Fact]
    public void GivenT2_WhenMatching_ShouldReturnT2()
    {
        var either = Union<string, int>.U2(42);
        var result = either.Match(_ => false, _ => true);
        
        result.Should().BeTrue();
    }
    
    [Fact]
    public void Given2T1_WhenEquatingWithSameValue_ShouldReturnTrue()
    {
        var either = Union<string, int>.U1("test");
        var other = Union<string, int>.U1("test");
        
        either.Equals(other).Should().BeTrue();
        (either == other).Should().BeTrue();
        (either != other).Should().BeFalse();
    }
    
    [Fact]
    public void Given2T2_WhenEquatingWithSameValue_ShouldReturnTrue()
    {
        var either = Union<string, int>.U2(42);
        var other = Union<string, int>.U2(42);
        
        either.Equals(other).Should().BeTrue();
        (either == other).Should().BeTrue();
        (either != other).Should().BeFalse();
    }
    
    [Fact]
    public void Given2T1_WhenEquatingWithDifferentValue_ShouldReturnFalse()
    {
        var either = Union<string, int>.U1("test");
        var other = Union<string, int>.U1("other");
        
        either.Equals(other).Should().BeFalse();
        (either == other).Should().BeFalse();
        (either != other).Should().BeTrue();
    }
    
    [Fact]
    public void Given2T2_WhenEquatingWithDifferentValue_ShouldReturnFalse()
    {
        var either = Union<string, int>.U2(42);
        var other = Union<string, int>.U2(43);
        
        either.Equals(other).Should().BeFalse();
        (either == other).Should().BeFalse();
        (either != other).Should().BeTrue();
    }
    
    [Fact]
    public void GivenT1AndT2_WhenEquating_ShouldReturnFalse()
    {
        var either = Union<string, int>.U1("test");
        var other = Union<string, int>.U2(42);
        
        either.Equals(other).Should().BeFalse();
        (either == other).Should().BeFalse();
        (either != other).Should().BeTrue();
    }
}