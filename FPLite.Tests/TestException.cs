using System;

namespace FPLite.Tests
{
    public class TestException : Exception
    {
        private const string ExceptionMessage = "Fail";

        public TestException() : base(ExceptionMessage)
        {
        }
    }
}