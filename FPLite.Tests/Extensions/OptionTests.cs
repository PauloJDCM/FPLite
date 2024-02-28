using FluentAssertions;
using FPLite.Extensions;
using Xunit;

namespace FPLite.Tests.Extensions
{
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
            result.ToString().Should().Be("1");
            result.IsSome.Should().BeTrue();
        }
        
        [Fact]
        public void GivenNull_WhenConvertingToOption_ShouldReturnNone()
        {
            int? value = null;
            var result = value.ToOption();
            
            result.ToString().Should().Be("None");
            result.IsSome.Should().BeFalse();
        }

        [Fact]
        public void GivenValue_WhenCasting_ShouldReturnSome()
        {
            var result = new A().AsOptionOf<A, IA>();
            result.IsSome.Should().BeTrue();
            result.Unwrap().Value.Should().Be(1);
        }
        
        [Fact]
        public void GivenNull_WhenCasting_ShouldReturnNone()
        {
            A? value = null;
            var result = value.AsOptionOf<A?, IA>();
            
            result.IsSome.Should().BeFalse();
        }
        
        [Fact]
        public void GivenOtherTypeValue_WhenCasting_ShouldReturnNone()
        {
            var result = new B().AsOptionOf<B, A>();
            result.IsSome.Should().BeFalse();
        }
    }
}