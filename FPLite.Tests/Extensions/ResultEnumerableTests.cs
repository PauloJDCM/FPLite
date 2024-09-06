using System;
using System.Collections.Generic;
using FluentAssertions;
using FPLite.Extensions;
using FPLite.Result;
using Xunit;

namespace FPLite.Tests.Extensions;

public class ResultEnumerableTests
{
    private static readonly int[] Numbers = { 1, 2, 3, 4, 5 };

    [Fact]
    public void GivenPopulatedEnumerable_WhenFirstOrError_ThenReturnsOkWithFirst()
    {
        var result = Numbers.FirstOrError(_ => new TestError());
        result.Type.Should().Be(ResultType.Ok);
        result.Unwrap().Should().Be(1);
    }

    [Fact]
    public void GivenEmptyEnumerable_WhenFirstOrError_ThenReturnsError()
    {
        var result = Array.Empty<int>().FirstOrError(_ => new TestError());
        result.Type.Should().Be(ResultType.Err);
    }

    [Fact]
    public void GivenPopulatedEnumerable_WhenFirstOrErrorWithPredicate_ThenReturnsOkWithFirst()
    {
        var result = Numbers.FirstOrError(x => x > 2, _ => new TestError());
        result.Type.Should().Be(ResultType.Ok);
        result.Unwrap().Should().Be(3);
    }

    [Fact]
    public void GivenEmptyEnumerable_WhenFirstOrErrorWithPredicate_ThenReturnsError()
    {
        var result = Array.Empty<int>().FirstOrError(x => x > 2, _ => new TestError());
        result.Type.Should().Be(ResultType.Err);
    }

    [Fact]
    public void GivenPopulatedEnumerable_WhenLastOrError_ThenReturnsOkWithLast()
    {
        var result = Numbers.LastOrError(_ => new TestError());
        result.Type.Should().Be(ResultType.Ok);
        result.Unwrap().Should().Be(5);
    }

    [Fact]
    public void GivenEmptyEnumerable_WhenLastOrError_ThenReturnsError()
    {
        var result = Array.Empty<int>().LastOrError(_ => new TestError());
        result.Type.Should().Be(ResultType.Err);
    }

    [Fact]
    public void GivenPopulatedEnumerable_WhenLastOrErrorWithPredicate_ThenReturnsOkWithLast()
    {
        var result = Numbers.LastOrError(x => x < 4, _ => new TestError());
        result.Type.Should().Be(ResultType.Ok);
        result.Unwrap().Should().Be(3);
    }

    [Fact]
    public void GivenEmptyEnumerable_WhenLastOrErrorWithPredicate_ThenReturnsError()
    {
        var result = Array.Empty<int>().LastOrError(x => x < 4, _ => new TestError());
        result.Type.Should().Be(ResultType.Err);
    }

    [Fact]
    public void GivenPopulatedEnumerableWithOneValue_WhenSingleOrError_ThenReturnsOkWithSingle()
    {
        var result = new[] { 5 }.SingleOrError(_ => new TestError());
        result.Type.Should().Be(ResultType.Ok);
        result.Unwrap().Should().Be(5);
    }

    [Fact]
    public void GivenEmptyEnumerable_WhenSingleOrError_ThenReturnsError()
    {
        var result = Array.Empty<int>().SingleOrError(_ => new TestError());
        result.Type.Should().Be(ResultType.Err);
    }

    [Fact]
    public void GivenPopulatedEnumerable_WhenSingleOrErrorWithPredicate_ThenReturnsOkWithSingle()
    {
        var result = Numbers.SingleOrError(x => x == 4, _ => new TestError());
        result.Type.Should().Be(ResultType.Ok);
        result.Unwrap().Should().Be(4);
    }

    [Fact]
    public void GivenEmptyEnumerable_WhenSingleOrErrorWithPredicate_ThenReturnsError()
    {
        var result = Array.Empty<int>().SingleOrError(x => x == 4, _ => new TestError());
        result.Type.Should().Be(ResultType.Err);
    }

    [Fact]
    public void GivenPopulatedEnumerable_WhenElementAtOrError_ThenReturnsOkWithElement()
    {
        var result = Numbers.ElementAtOrError(3, _ => new TestError());
        result.Type.Should().Be(ResultType.Ok);
        result.Unwrap().Should().Be(4);
    }

    [Fact]
    public void GivenEmptyEnumerable_WhenElementAtOrError_ThenReturnsError()
    {
        var result = Array.Empty<int>().ElementAtOrError(3, _ => new TestError());
        result.Type.Should().Be(ResultType.Err);
    }

    [Fact]
    public void GivenPopulatedEnumerable_WhenElementAtOrErrorOutOfRange_ThenReturnsError()
    {
        var result = Array.Empty<int>().ElementAtOrError(10, _ => new TestError());
        result.Type.Should().Be(ResultType.Err);
    }

    [Fact]
    public void GivenPopulatedKeyValuePairEnumerable_WhenGetValueOrError_ThenReturnsOk()
    {
        var source = new[] { new KeyValuePair<int, string>(1, "Value") };
        var result = source.GetValueOrError(1, _ => new TestError());

        result.Type.Should().Be(ResultType.Ok);
        result.Unwrap().Should().Be("Value");
    }

    [Fact]
    public void GivenEmptyEnumerable_WhenGetValueOrError_ThenReturnsError()
    {
        var result = Array.Empty<KeyValuePair<int, string>>().GetValueOrError(1, _ => new TestError());
        result.Type.Should().Be(ResultType.Err);
    }

    [Fact]
    public void GivenDictionaryWithKey_WhenGetValueOrError_ThenReturnsOk()
    {
        var source = new Dictionary<int, string> { { 1, "Value" } };
        var result = source.GetValueOrError(1, _ => new TestError());

        result.Type.Should().Be(ResultType.Ok);
        result.Unwrap().Should().Be("Value");
    }

    [Fact]
    public void GivenDictionaryWithoutKey_WhenGetValueOrError_ThenReturnsError()
    {
        var source = new Dictionary<int, string> { { 1, "One" } };
        var result = source.GetValueOrError(2, _ => new TestError());

        result.Type.Should().Be(ResultType.Err);
    }
}