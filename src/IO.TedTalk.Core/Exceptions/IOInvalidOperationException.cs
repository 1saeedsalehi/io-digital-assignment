namespace IO.TedTalk.Core.Exceptions;

public class IOInvalidOperationException : IOException
{
    public const int ExceptionCode = 5;

    public IOInvalidOperationException(string message, string operation = "") : base(message, ExceptionCode)
    {
        Operation = operation;
    }

    public IOInvalidOperationException(string message, string operation, Exception innerException) : base(message, innerException, ExceptionCode)
    {
        Operation = operation;
    }

    public string Operation { get; }
}
