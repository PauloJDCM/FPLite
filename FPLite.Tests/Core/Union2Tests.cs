using FluentAssertions;
using Xunit;

namespace FPLite.Tests.Core;

public class Union2Tests
{
    [Fact]
    public void GivenStringOrInt_WhenValueIsT1_ShouldReturnString()
    {
        var either = FPLite.T1<string, int>("test");
        var result = either.Match(_ => true, _ => false);
        
        either.Type.Should().Be(UnionType.T1);
        result.Should().BeTrue();
    }
    
    [Fact]
    public void GivenStringOrInt_WhenValueIsT2_ShouldReturnInt()
    {
        var either = FPLite.T2<string, int>(42);
        var result = either.Match(_ => false, _ => true);
        
        either.Type.Should().Be(UnionType.T2);
        result.Should().BeTrue();
    }
    
    [Fact]
    public void GivenT1_WhenMatching_ShouldDoT1Action()
    {
        var either = FPLite.T1<string, int>("test");
        var result = false;
        either.Match(_ => { result = true; }, _ => { });
        
        result.Should().BeTrue();
    }
    
    [Fact]
    public void GivenT2_WhenMatching_ShouldDoT2Action()
    {
        var either = FPLite.T2<string, int>(42);
        var result = false;
        either.Match(_ => { }, _ => { result = true; });
        
        result.Should().BeTrue();
    }
    
    [Fact]
    public void GivenT1_WhenMatching_ShouldReturnT1()
    {
        var either = FPLite.T1<string, int>("test");
        var result = either.Match(_ => true, _ => false);
        
        result.Should().BeTrue();
    }
    
    [Fact]
    public void GivenT2_WhenMatching_ShouldReturnT2()
    {
        var either = FPLite.T2<string, int>(42);
        var result = either.Match(_ => false, _ => true);
        
        result.Should().BeTrue();
    }
}