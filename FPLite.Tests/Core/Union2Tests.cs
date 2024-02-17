using FluentAssertions;
using FPLite.Union;
using Xunit;

namespace FPLite.Tests.Core
{
    public class Union2Tests
    {
        [Fact]
        public void Given2Types_WhenValueIsNothing_ShouldBeNothing()
        {
            var union = Union<string, int>.Nothing;
            var result = union.Match(s => s, i => i.ToString(), () => "Nothing");

            var bResult = false;
            union.Match(s => { }, i => { }, () => bResult = true);

            bResult.Should().BeTrue();
            result.Should().Be("Nothing");
            union.ToString().Should().Be("Nothing");
        }

        [Fact]
        public void GivenType1Value_WhenValueIsNotNull_ShouldBeType1()
        {
            var union = Union<string, int>.Type1("test");
            var result = union.Match(s => s, i => i.ToString(), () => "Nothing");

            var bResult = false;
            union.Match(s => bResult = true, i => { }, () => { });

            bResult.Should().BeTrue();
            result.Should().Be("test");
            union.ToString().Should().Be("T1(test)");
        }

        [Fact]
        public void GivenType2Value_WhenValueIsNotNull_ShouldBeType2()
        {
            var union = Union<string, int>.Type2(123);
            var result = union.Match(s => s, i => i.ToString(), () => "Nothing");

            var bResult = false;
            union.Match(s => { }, i => bResult = true, () => { });

            bResult.Should().BeTrue();
            result.Should().Be("123");
            union.ToString().Should().Be("T2(123)");
        }

        [Fact]
        public void GivenType1Value_WhenValueIsNull_ShouldBeNothing()
        {
            var union = Union<string?, int?>.Type1(null);
            var result = union.Match(s => s!, i => i.ToString()!, () => "Nothing");

            result.Should().Be("Nothing");
            union.ToString().Should().Be("Nothing");
        }

        [Fact]
        public void GivenType2Value_WhenValueIsNull_ShouldBeNothing()
        {
            var union = Union<string?, int?>.Type2(null);
            var result = union.Match(s => s!, i => i.ToString()!, () => "Nothing");

            result.Should().Be("Nothing");
            union.ToString().Should().Be("Nothing");
        }
        
        [Theory]
        [InlineData(1, 2)]
        [InlineData(4, 8)]
        [InlineData(0, 0)]
        public void GivenType1Value_WhenFuncDependsOnType_ShouldReturnType1(byte value, byte expected)
        {
            var union = Union<byte, int>.Type1(value);
            var result = union.Match(b => b + b, i => i.ToString());
            
            result.ToString().Should().Be($"T1({expected})");
        }
        
        [Theory]
        [InlineData(1, "1")]
        [InlineData(4, "4")]
        [InlineData(0, "0")]
        public void GivenType2Value_WhenFuncDependsOnType_ShouldReturnType2(byte value, string expected)
        {
            var union = Union<byte, int>.Type2(value);
            var result = union.Match(b => b + b, i => i.ToString());
            
            result.ToString().Should().Be($"T2({expected})");
        }
        
        [Fact]
        public void GivenNothing_WhenFuncDependsOnType_ShouldReturnNothing()
        {
            var union = Union<byte, int>.Nothing;
            var result = union.Match(b => b + b, i => i.ToString());
            
            result.ToString().Should().Be("Nothing");
        }
        
        [Fact]
        public void GivenType1Value_WhenBindingType1_ShouldReturnT1()
        {
            var union = Union<byte, int>.Type1(1);
            var result = union.Bind1(b => b + b);
            
            result.ToString().Should().Be("T1(2)");
        }
        
        [Fact]
        public void GivenType1Value_WhenBindingType2_ShouldKeepType1AsType2()
        {
            var union = Union<byte, int>.Type1(1);
            var result = union.Bind2(i => i * i);
            
            result.ToString().Should().Be("T1(1)");
        }
        
        [Fact]
        public void GivenType2Value_WhenBindingType1_ShouldKeepType2AsType2()
        {
            var union = Union<byte, int>.Type2(10);
            var result = union.Bind1(b => b + b);
            
            result.ToString().Should().Be("T2(10)");
        }
        
        [Fact]
        public void GivenType2Value_WhenBindingType2_ShouldReturnT2()
        {
            var union = Union<byte, int>.Type2(10);
            var result = union.Bind2(i => i * i);
            
            result.ToString().Should().Be("T2(100)");
        }
        
        [Fact]
        public void GivenNothing_WhenBindingType1_ShouldReturnNothing()
        {
            var union = Union<byte, int>.Nothing;
            var result = union.Bind1(b => b + b);
            
            result.ToString().Should().Be("Nothing");
        }
        
        [Fact]
        public void GivenNothing_WhenBindingType2_ShouldReturnNothing()
        {
            var union = Union<byte, int>.Nothing;
            var result = union.Bind2(i => i * i);
            
            result.ToString().Should().Be("Nothing");
        }

        [Fact]
        public void Given2Unions_WhenTypesAndValuesAreEqual_ShouldBeEqual()
        {
            var union1 = Union<int, string>.Type1(1);
            var union2 = Union<int, string>.Type1(1);
            
            union1.Should().Be(union2);
            (union1 == union2).Should().BeTrue();
            (union1 != union2).Should().BeFalse();
        }
        
        [Fact]
        public void Given2Unions_WhenTypesAreNotEqual_ShouldNotBeEqual()
        {
            var union1 = Union<int, string>.Type1(1);
            var union2 = Union<byte, string>.Type2("1");
            
            union1.Should().NotBe(union2);
        }
        
        [Fact]
        public void Given2Unions_WhenValuesAreNotEqual_ShouldNotBeEqual()
        {
            var union1 = Union<int, string>.Type1(1);
            var union2 = Union<int, string>.Type1(2);
            
            union1.Should().NotBe(union2);
            (union1 == union2).Should().BeFalse();
            (union1 != union2).Should().BeTrue();
        }
    }
}