using FluentAssertions;
using Xunit;

namespace FPLite.Tests.Core
{
    public class ResultTests
    {
        private class TestError : IError
        {
            public string Code => "TEST_CODE";
            public string Message => "TEST_MESSAGE";
        }

        [Fact]
        public void GivenSomeValue_WhenCreatingOk_ShouldReturnOk()
        {
            var result = Result<string, TestError>.Ok("test");
            result.Unwrap().Should().Be("test");
            result.ToString().Should().Be("Ok(test)");
        }

        [Fact]
        public void GivenNoneValue_WhenCreatingOk_ShouldThrow()
        {
            Assert.Throws<ResultCreateException<string>>(() => Result<string, TestError>.Ok(null!));
        }

        [Fact]
        public void GivenSomeValue_WhenCreatingError_ShouldReturnError()
        {
            var result = Result<string, TestError>.Err(new TestError());
            result.ToString().Should().Be("Err(Error: TEST_CODE - TEST_MESSAGE)");
        }

        [Fact]
        public void GivenNoneValue_WhenCreatingError_ShouldThrow()
        {
            Assert.Throws<ResultCreateException<TestError>>(() => Result<string, TestError>.Err(null!));
        }

        [Fact]
        public void GivenSomeValue_WhenUnwrapping_ShouldReturnValue()
        {
            var result = Result<string, TestError>.Ok("test");
            result.Unwrap().Should().Be("test");
        }

        [Fact]
        public void GivenNoneValue_WhenUnwrapping_ShouldThrow()
        {
            var result = Result<string, TestError>.Err(new TestError());
            Assert.Throws<ResultUnwrapException<string, TestError>>(() => result.Unwrap());
        }
        
        [Fact]
        public void GivenSomeValue_WhenUnwrappingWithOr_ShouldReturnValue()
        {
            var result = Result<string, TestError>.Ok("test");
            result.UnwrapOr("default").Should().Be("test");
        }
        
        [Fact]
        public void GivenNoneValue_WhenUnwrappingWithOr_ShouldReturnDefault()
        {
            var result = Result<string, TestError>.Err(new TestError());
            result.UnwrapOr("default").Should().Be("default");
        }

        [Fact]
        public void GivenValue_WhenMatching_ShouldMatchValue()
        {
            var result = Result<int, TestError>.Ok(1);

            var testResult = false;
            result.Match(i => testResult = true, _ => testResult = false);
            
            testResult.Should().BeTrue();
            result.ToString().Should().Be("Ok(1)");
        }
        
        [Fact]
        public void GivenError_WhenMatching_ShouldMatchError()
        {
            var result = Result<int, TestError>.Err(new TestError());

            var testResult = false;
            result.Match(i => testResult = false, _ => testResult = true);
            
            testResult.Should().BeTrue();
            result.ToString().Should().Be("Err(Error: TEST_CODE - TEST_MESSAGE)");
        }
        
        [Fact]
        public void GivenValue_WhenBinding_ShouldBindValue()
        {
            var result = Result<int, TestError>.Ok(1);
            var result2 = result.Bind(i => i + 1);
            
            result2.ToString().Should().Be("Ok(2)");
        }
        
        [Fact]
        public void GivenError_WhenBinding_ShouldKeepError()
        {
            var result = Result<int, TestError>.Err(new TestError());
            var result2 = result.Bind(i => Result<int, TestError>.Ok(i + 1));
            
            result2.ToString().Should().Be("Err(Error: TEST_CODE - TEST_MESSAGE)");
        }
    }
}