using FluentAssertions;
using Xunit;

namespace FPLite.Tests.Core;

public class ResultTests
{
    [Fact]
    public void GivenInt_WhenValueIsErr_ShouldReturnError()
    {
        var value = FPLite.Err<int, TestError>(new TestError());
        var result = value.Match(_ => false, _ => true);
            
        value.IsOk.Should().BeFalse();
        result.Should().BeTrue();
    }
        
    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    public void GivenInt_WhenValueIsOk_ShouldReturnValue(int originalValue)
    {
        var value = FPLite.Ok<int, TestError>(originalValue);
        var result = value.Match(i => i, _ => originalValue - 1);
            
        value.IsOk.Should().BeTrue();
        result.Should().Be(originalValue);
    }

    [Fact]
    public void GivenErr_WhenMatching_ShouldDoErrorAction()
    {
        var value = FPLite.Err<int, TestError>(new TestError());
        var result = false;
        value.Match(_ => { }, _ => { result = true; });
            
        result.Should().BeTrue();
    }
        
    [Fact]
    public void GivenOk_WhenMatching_ShouldDoOkAction()
    {
        var value = FPLite.Ok<int, TestError>(1);
        var result = false;
        value.Match(_ => { result = true; }, _ => { });
            
        result.Should().BeTrue();
    }
        
    [Fact]
    public void GivenErr_WhenBinding_ShouldReturnError()
    {
        var value = FPLite.Err<int, TestError>(new TestError());
        var bind = value.Bind(i => i + 1);
        var result = bind.Match(i => i, _ => 0);
            
        value.IsOk.Should().BeFalse();
        result.Should().Be(0);
    }
        
    [Fact]
    public void GivenOk_WhenBinding_ShouldReturnOk()
    {
        var value = FPLite.Ok<int, TestError>(1);
        var bind = value.Bind(i => i + 1);
        var result = bind.Match(i => i, _ => 0);
            
        value.IsOk.Should().BeTrue();
        result.Should().Be(2);
    }
        
    [Fact]
    public void GivenErr_WhenUnwrapping_ShouldThrow()
    {
        var value = FPLite.Err<int, TestError>(new TestError());
        Assert.ThrowsAny<UnwrapException>(() => value.Unwrap());
    }
    
    [Fact]
    public void GivenOk_WhenUnwrappingOrWithSameType_ShouldReturnValue()
    {
        var value = FPLite.Ok<int, TestError>(1);
        value.UnwrapOr(_ => 0).Should().Be(1);
    }
    
    [Fact]
    public void GivenErr_WhenUnwrappingOrWithSameType_ShouldReturnOr()
    {
        var value = FPLite.Err<int, TestError>(new TestError());
        value.UnwrapOr(_ => 0).Should().Be(0);
    }
    
    [Fact]
    public void GivenOk_WhenUnwrappingOrWithDifferentType_ShouldReturnValue()
    {
        var value = FPLite.Ok<int, TestError>(1);
        var unwrap = value.UnwrapOr(_ => "test");
        
        unwrap.Type.Should().Be(UnionType.T1);
        unwrap.Match(i => i.ToString(), _ => "test").Should().Be("1");
    }
    
    [Fact]
    public void GivenErr_WhenUnwrappingOrWithDifferentType_ShouldReturnOr()
    {
        var value = FPLite.Err<int, TestError>(new TestError());
        var unwrap = value.UnwrapOr(_ => "test");
        
        unwrap.Type.Should().Be(UnionType.T2);
        unwrap.Match(i => i.ToString(), _ => "test").Should().Be("test");
    }
}