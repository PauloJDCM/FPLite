using FluentAssertions;
using FPLite.Extensions;
using Xunit;

namespace FPLite.Tests.Extensions
{
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
        public void GivenValue_WhenConvertingToResult_ShouldReturnOk()
        {
            var result = 1.ToResult(() => new TestError());
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(1);
        }
        
        [Fact]
        public void GivenNull_WhenConvertingToResult_ShouldReturnNone()
        {
            int? value = null;
            var result = value.ToResult(() => new TestError());
            result.IsOk.Should().BeFalse();
        }
        
        [Fact]
        public void GivenValue_WhenCasting_ShouldReturnSome()
        {
            var result = new A().AsResultOf<A, IA, TestError>(() => new TestError());
            result.IsOk.Should().BeTrue();
            result.Unwrap().Value.Should().Be(1);
        }
        
        [Fact]
        public void GivenNull_WhenCasting_ShouldReturnNone()
        {
            A? value = null;
            var result = value.AsResultOf<A?, IA, TestError>(() => new TestError());
            result.IsOk.Should().BeFalse();
        }
        
        [Fact]
        public void GivenOtherTypeValue_WhenCasting_ShouldReturnNone()
        {
            var result = new B().AsResultOf<B, A, TestError>(() => new TestError());
            result.IsOk.Should().BeFalse();
        }
    }
}