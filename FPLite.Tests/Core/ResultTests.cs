using FluentAssertions;
using FPLite.Result;
using FPLite.Union;
using Xunit;

namespace FPLite.Tests.Core;

public class ResultTests
{
    [Fact]
    public void GivenInt_WhenValueIsErr_ShouldReturnError()
    {
        var value = Result<int, TestError>.Err(new TestError());
        var result = value.Match(_ => false, _ => true);

        value.Type.Should().Be(ResultType.Err);
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(-1)]
    public void GivenInt_WhenValueIsOk_ShouldReturnValue(int originalValue)
    {
        var value = Result<int, TestError>.Ok(originalValue);
        var result = value.Match(i => i, _ => originalValue - 1);

        value.Type.Should().Be(ResultType.Ok);
        result.Should().Be(originalValue);
    }

    [Fact]
    public void GivenErr_WhenMatching_ShouldDoErrorAction()
    {
        var value = Result<int, TestError>.Err(new TestError());
        var result = false;
        value.Match(_ => { }, _ => { result = true; });

        result.Should().BeTrue();
    }

    [Fact]
    public void GivenOk_WhenMatching_ShouldDoOkAction()
    {
        var value = Result<int, TestError>.Ok(1);
        var result = false;
        value.Match(_ => { result = true; }, _ => { });

        result.Should().BeTrue();
    }

    [Fact]
    public void GivenErr_WhenBinding_ShouldReturnError()
    {
        var value = Result<int, TestError>.Err(new TestError());
        var bind = value.Bind(i => i + 1);
        var result = bind.Match(i => i, _ => 0);

        value.Type.Should().Be(ResultType.Err);
        result.Should().Be(0);
    }

    [Fact]
    public void GivenOk_WhenBinding_ShouldReturnOk()
    {
        var value = Result<int, TestError>.Ok(1);
        var bind = value.Bind(i => i + 1);
        var result = bind.Match(i => i, _ => 0);

        value.Type.Should().Be(ResultType.Ok);
        result.Should().Be(2);
    }

    [Fact]
    public void GivenErr_WhenUnwrapping_ShouldThrow()
    {
        var value = Result<int, TestError>.Err(new TestError());
        Assert.ThrowsAny<UnwrapException>(() => value.Unwrap());
        Assert.Throws<ResultUnwrapException<int, TestError>>(() => value.Unwrap());
    }

    [Fact]
    public void GivenOk_WhenUnwrappingOrWithSameType_ShouldReturnValue()
    {
        var value = Result<int, TestError>.Ok(1);
        value.UnwrapOr(_ => 0).Should().Be(1);
    }

    [Fact]
    public void GivenErr_WhenUnwrappingOrWithSameType_ShouldReturnOr()
    {
        var value = Result<int, TestError>.Err(new TestError());
        value.UnwrapOr(_ => 0).Should().Be(0);
    }

    [Fact]
    public void GivenOk_WhenUnwrappingOrWithDifferentType_ShouldReturnValue()
    {
        var value = Result<int, TestError>.Ok(1);
        var unwrap = value.UnwrapOr(_ => "test");

        unwrap.Type.Should().Be(UnionType.T1);
        unwrap.Match(i => i.ToString(), _ => "test").Should().Be("1");
    }

    [Fact]
    public void GivenErr_WhenUnwrappingOrWithDifferentType_ShouldReturnOr()
    {
        var value = Result<int, TestError>.Err(new TestError());
        var unwrap = value.UnwrapOr(_ => "test");

        unwrap.Type.Should().Be(UnionType.T2);
        unwrap.Match(i => i.ToString(), _ => "test").Should().Be("test");
    }
}