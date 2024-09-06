using FluentAssertions;
using FPLite.Extensions;
using FPLite.Option;
using Xunit;

namespace FPLite.Tests.Extensions;

public class OptionTests
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
    public void GivenValue_WhenConvertingToOption_ShouldReturnSome()
    {
        var result = 1.ToOption();
        result.Type.Should().Be(OptionType.Some);
    }

    [Fact]
    public void GivenValueNull_WhenConvertingToOption_ShouldReturnNone()
    {
        int? value = null;
        var result = value.ToOption();

        result.Type.Should().Be(OptionType.None);
    }

    [Fact]
    public void GivenRefNull_WhenConvertingToOption_ShouldReturnNone()
    {
        string? value = null;
        var result = value.ToOption();

        result.Type.Should().Be(OptionType.None);
    }

    [Fact]
    public void GivenValue_WhenCasting_ShouldReturnSome()
    {
        var result = new A().AsOptionOf<A, IA>();
        result.Type.Should().Be(OptionType.Some);
        result.Unwrap().Value.Should().Be(1);
    }

    [Fact]
    public void GivenOtherTypeValue_WhenCasting_ShouldReturnNone()
    {
        var result = new B().AsOptionOf<B, A>();
        result.Type.Should().Be(OptionType.None);
    }

    [Fact]
    public void GivenNone_WhenUnwrappingWithTryOption_ShouldReturnSomeWithExceptionMessage()
    {
        var option = Option<int>.None();
        
        // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
        var result = OptionExtensions.TryOption<OptionUnwrapException<int>, TestError>(() => option.Unwrap(),
            exception => new TestError(exception.Message));

        result.Type.Should().Be(OptionType.Some);
        result.Value.Message.Should().Contain("Called Option");
    }
}