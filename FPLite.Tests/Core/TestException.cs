using System;

namespace FPLite.Tests.Core
{
    public class TestException : Exception
    {
        private const string ExceptionMessage = "Fail";

        public TestException() : base(ExceptionMessage)
        {
        }
    }
}