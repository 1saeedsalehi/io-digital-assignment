namespace IO.TedTalk.Core.Exceptions;

/// <inheritdoc />
/// <summary>
/// Base Exception for all exceptions of IOException. 
/// </summary>
public class IOException : Exception
{
    public IOException(string message, int? errorCode = null)
        : base(message)
    {
        ErrorCode = errorCode;
        Severity = LogSeverity.Error;
    }

    public IOException(string message, Exception innerException, int? errorCode = null)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        Severity = LogSeverity.Error;
    }

    /// <summary>
    /// An arbitrary error code.
    /// </summary>
    public int? ErrorCode { get; protected set; }


    public LogSeverity Severity { get; protected set; }

}
