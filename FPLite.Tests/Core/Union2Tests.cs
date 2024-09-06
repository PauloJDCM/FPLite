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
        var result = either.Match(_ => true, _ => false);
        
        either.Type.Should().Be(UnionType.T1);
        result.Should().BeTrue();
    }
    
    [Fact]
    public void GivenStringOrInt_WhenValueIsT2_ShouldReturnInt()
    {
        var either = Union<string, int>.U2(42);
        var result = either.Match(_ => false, _ => true);
        
        either.Type.Should().Be(UnionType.T2);
        result.Should().BeTrue();
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
}