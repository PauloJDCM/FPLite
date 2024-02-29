using System;
using System.Collections.Generic;
using FluentAssertions;
using FPLite.Extensions;
using Xunit;

namespace FPLite.Tests.Extensions
{
    public class ResultEnumerableTests
    {
        private static readonly int[] Numbers = { 1, 2, 3, 4, 5 };

        [Fact]
        public void GivenPopulatedEnumerable_WhenFirstOrNone_ThenReturnsOkWithFirst()
        {
            var result = Numbers.FirstOrNone(() => new TestError());
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(1);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenFirstOrNone_ThenReturnsError()
        {
            var result = Array.Empty<int>().FirstOrNone(() => new TestError());
            result.IsOk.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerable_WhenFirstOrNoneWithPredicate_ThenReturnsOkWithFirst()
        {
            var result = Numbers.FirstOrNone(x => x > 2, () => new TestError());
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(3);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenFirstOrNoneWithPredicate_ThenReturnsError()
        {
            var result = Array.Empty<int>().FirstOrNone(x => x > 2, () => new TestError());
            result.IsOk.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerable_WhenLastOrNone_ThenReturnsOkWithLast()
        {
            var result = Numbers.LastOrNone(() => new TestError());
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(5);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenLastOrNone_ThenReturnsError()
        {
            var result = Array.Empty<int>().LastOrNone(() => new TestError());
            result.IsOk.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerable_WhenLastOrNoneWithPredicate_ThenReturnsOkWithLast()
        {
            var result = Numbers.LastOrNone(x => x < 4, () => new TestError());
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(3);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenLastOrNoneWithPredicate_ThenReturnsError()
        {
            var result = Array.Empty<int>().LastOrNone(x => x < 4, () => new TestError());
            result.IsOk.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerableWithOneValue_WhenSingleOrNone_ThenReturnsOkWithSingle()
        {
            var result = new[] { 5 }.SingleOrNone(() => new TestError());
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(5);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenSingleOrNone_ThenReturnsError()
        {
            var result = Array.Empty<int>().SingleOrNone(() => new TestError());
            result.IsOk.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerable_WhenSingleOrNoneWithPredicate_ThenReturnsOkWithSingle()
        {
            var result = Numbers.SingleOrNone(x => x == 4, () => new TestError());
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(4);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenSingleOrNoneWithPredicate_ThenReturnsError()
        {
            var result = Array.Empty<int>().SingleOrNone(x => x == 4, () => new TestError());
            result.IsOk.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerable_WhenElementAtOrNone_ThenReturnsOkWithElement()
        {
            var result = Numbers.ElementAtOrNone(3, () => new TestError());
            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be(4);
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenElementAtOrNone_ThenReturnsError()
        {
            var result = Array.Empty<int>().ElementAtOrNone(3, () => new TestError());
            result.IsOk.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedEnumerable_WhenElementAtOrNoneOutOfRange_ThenReturnsError()
        {
            var result = Array.Empty<int>().ElementAtOrNone(10, () => new TestError());
            result.IsOk.Should().BeFalse();
        }

        [Fact]
        public void GivenPopulatedKeyValuePairEnumerable_WhenGetValueOrNone_ThenReturnsOk()
        {
            var source = new[] { new KeyValuePair<int, string>(1, "Value") };
            var result = source.GetValueOrNone(1, () => new TestError());

            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be("Value");
        }

        [Fact]
        public void GivenEmptyEnumerable_WhenGetValueOrNone_ThenReturnsError()
        {
            var result = Array.Empty<KeyValuePair<int, string>>().GetValueOrNone(1, () => new TestError());
            result.IsOk.Should().BeFalse();
        }

        [Fact]
        public void GivenDictionaryWithKey_WhenGetValueOrNone_ThenReturnsOk()
        {
            var source = new Dictionary<int, string> { { 1, "Value" } };
            var result = source.GetValueOrNone(1, () => new TestError());

            result.IsOk.Should().BeTrue();
            result.Unwrap().Should().Be("Value");
        }

        [Fact]
        public void GivenDictionaryWithoutKey_WhenGetValueOrNone_ThenReturnsError()
        {
            var source = new Dictionary<int, string> { { 1, "One" } };
            var result = source.GetValueOrNone(2, () => new TestError());

            result.IsOk.Should().BeFalse();
        }
    }
}