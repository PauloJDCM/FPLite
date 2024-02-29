using System;
using System.Collections.Generic;
using FluentAssertions;
using FPLite.Extensions;
using Xunit;

namespace FPLite.Tests.Extensions
{
    public class OptionEnumerableTests
    {
        private static readonly int[] Numbers = { 1, 2, 3, 4, 5 };

        [Fact]
        public void GivenPopulatedEnumerable_WhenFirstOrNone_ThenReturnsSomeWithFirst()
        {
            var result = Numbers.FirstOrNone();
            result.IsSome.Should().BeTrue();
            result.Unwrap().Should().Be(1);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenFirstOrNone_ThenReturnsNone()
        {
            var result = Array.Empty<int>().FirstOrNone();
            result.IsSome.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerable_WhenFirstOrNoneWithPredicate_ThenReturnsSomeWithFirst()
        {
            var result = Numbers.FirstOrNone(x => x > 2);
            result.IsSome.Should().BeTrue();
            result.Unwrap().Should().Be(3);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenFirstOrNoneWithPredicate_ThenReturnsNone()
        {
            var result = Array.Empty<int>().FirstOrNone(x => x > 2);
            result.IsSome.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerable_WhenLastOrNone_ThenReturnsSomeWithLast()
        {
            var result = Numbers.LastOrNone();
            result.IsSome.Should().BeTrue();
            result.Unwrap().Should().Be(5);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenLastOrNone_ThenReturnsNone()
        {
            var result = Array.Empty<int>().LastOrNone();
            result.IsSome.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerable_WhenLastOrNoneWithPredicate_ThenReturnsSomeWithLast()
        {
            var result = Numbers.LastOrNone(x => x < 4);
            result.IsSome.Should().BeTrue();
            result.Unwrap().Should().Be(3);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenLastOrNoneWithPredicate_ThenReturnsNone()
        {
            var result = Array.Empty<int>().LastOrNone(x => x < 4);
            result.IsSome.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerableWithOneValue_WhenSingleOrNone_ThenReturnsSomeWithSingle()
        {
            var result = new[] { 5 }.SingleOrNone();
            result.IsSome.Should().BeTrue();
            result.Unwrap().Should().Be(5);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenSingleOrNone_ThenReturnsNone()
        {
            var result = Array.Empty<int>().SingleOrNone();
            result.IsSome.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerable_WhenSingleOrNoneWithPredicate_ThenReturnsSomeWithSingle()
        {
            var result = Numbers.SingleOrNone(x => x == 4);
            result.IsSome.Should().BeTrue();
            result.Unwrap().Should().Be(4);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenSingleOrNoneWithPredicate_ThenReturnsNone()
        {
            var result = Array.Empty<int>().SingleOrNone(x => x == 4);
            result.IsSome.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerable_WhenElementAtOrNone_ThenReturnsSomeWithElement()
        {
            var result = Numbers.ElementAtOrNone(3);
            result.IsSome.Should().BeTrue();
            result.Unwrap().Should().Be(4);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenElementAtOrNone_ThenReturnsNone()
        {
            var result = Array.Empty<int>().ElementAtOrNone(3);
            result.IsSome.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerable_WhenElementAtOrNoneOutOfRange_ThenReturnsNone()
        {
            var result = Array.Empty<int>().ElementAtOrNone(10);
            result.IsSome.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedKeyValuePairEnumerable_WhenGetValueOrNone_ThenReturnsSome()
        {
            var source = new[] { new KeyValuePair<int, string>(1, "Value") };
            var result = source.GetValueOrNone(1);

            result.IsSome.Should().BeTrue();
            result.Unwrap().Should().Be("Value");
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenGetValueOrNone_ThenReturnsNone()
        {
            var result = Array.Empty<KeyValuePair<int, string>>().GetValueOrNone(1);
            result.IsSome.Should().BeFalse();
        }

        [Fact]
        public void GivenDictionaryWithKey_WhenGetValueOrNone_ThenReturnsSome()
        {
            var source = new Dictionary<int, string> { { 1, "Value" } };
            var result = source.GetValueOrNone(1);

            result.IsSome.Should().BeTrue();
            result.Unwrap().Should().Be("Value");
        }

        [Fact]
        public void GivenDictionaryWithoutKey_WhenGetValueOrNone_ThenReturnsNone()
        {
            var source = new Dictionary<int, string> { { 1, "One" } };
            var result = source.GetValueOrNone(2);

            result.IsSome.Should().BeFalse();
        }
    }
}