using System;
using FluentAssertions;
using FPLite.Either;
using FPLite.Extensions;
using FPLite.Option;
using Xunit;

namespace FPLite.Tests.Extensions;

public class EitherTests
{
    [Fact]
    public void GivenAction_WhenActionIsExecuted_ThenReturnsNeither()
    {
        var option = Option<int>.Some(1);
        var result = EitherExtensions.TryEither<OptionUnwrapException<int>>(() => option.Unwrap());

        result.Type.Should().Be(EitherType.Neither);
        result.L.Should().BeNull();
        result.R.Should().BeNull();
    }

    [Fact]
    public void GivenAction_WhenActionFailsWithSpecifiedException_ThenReturnsSpecifiedException()
    {
        var option = Option<int>.None();
        var result = EitherExtensions.TryEither<OptionUnwrapException<int>>(() => option.Unwrap());

        result.Type.Should().Be(EitherType.Left);
        result.L.Should().BeOfType<OptionUnwrapException<int>>();
        result.L.Should().NotBeNull();
    }

    [Fact]
    public void GivenAction_WhenActionFailsWithOtherException_ThenReturnsException()
    {
        var result =
            EitherExtensions.TryEither<OptionUnwrapException<int>>(() => throw new ArrayTypeMismatchException());

        result.Type.Should().Be(EitherType.Right);
        result.R.Should().BeOfType<ArrayTypeMismatchException>();
        result.R.Should().NotBeNull();
    }
}