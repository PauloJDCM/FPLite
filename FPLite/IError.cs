namespace FPLite
{
    public interface IError
    {
        string Code { get; }
        string Message { get; }
        
        public string ToErrorString() => $"Error: {Code} - {Message}";
    }
}