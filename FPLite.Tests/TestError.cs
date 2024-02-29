namespace FPLite.Tests
{
    public class TestError : IError
    {
        public string Code => "TEST_CODE";
        public string Message => "TEST_MESSAGE";
    }
}