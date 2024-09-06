using FluentAssertions;
using FPLite.Extensions;
using FPLite.Result;
using Xunit;

namespace FPLite.Tests.Extensions;

public class ResultTests
{
    private interface IA
    {
        int Value { get; }
    }

    private struct A : IA
    {
        public int Value => 1;
    }

    private struct B : IA
    {
        public int Value => 2;
    }
        
    [Fact]
    public void GivenValue_WhenCasting_ShouldReturnSome()
    {
        var result = new A().AsResultOf<A, IA, TestError>(() => new TestError());
        result.Type.Should().Be(ResultType.Ok);
        result.Unwrap().Value.Should().Be(1);
    }
        
    [Fact]
    public void GivenNull_WhenCasting_ShouldReturnNone()
    {
        A? value = null;
        var result = value.AsResultOf<A?, IA, TestError>(() => new TestError());
        result.Type.Should().Be(ResultType.Err);
    }
        
    [Fact]
    public void GivenOtherTypeValue_WhenCasting_ShouldReturnNone()
    {
        var result = new B().AsResultOf<B, A, TestError>(() => new TestError());
        result.Type.Should().Be(ResultType.Err);
    }
}