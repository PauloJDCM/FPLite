﻿using FluentAssertions;
using FPLite.Extensions;
using Xunit;

namespace FPLite.Tests.Extensions
{
    public class CommonTests
    {
        [Fact]
        public void GivenFunction_WhenPiping_ShouldReturnFunctionResult()
        {
            var result = 5.Pipe(x => x + 1);

            result.Should().Be(6);
        }

        [Fact]
        public void GivenAction_WhenPiping_ShouldPerformAction()
        {
            var result = false;
            5.Pipe(x => { result = true; });

            result.Should().Be(true);
        }

        [Fact]
        public void GivenValue_WhenIgnoring_ShouldDoNothing()
        {
            var result = 5;
            result.Pipe(x => CommonExtensions.Ignore());

            result.Should().Be(5);
        }
    }
}