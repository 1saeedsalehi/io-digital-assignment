namespace IO.TedTalk.Core.Exceptions;

/// <inheritdoc />
/// <summary>
/// Base Exception for all exceptions of Aivwa. 
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

    /// <summary>
    /// Technical-details are not allowed to be shown to the user.
    /// Just log them or use them internally by software-technicians.
    /// </summary>
    public string TechnicalMessage { get; protected set; }

    /// <summary>
    /// Severity of the exception. The main usage will be for logs and monitoring.
    /// This way we can distinguish various exceptions in logs, 
    /// Think about the difference of between severity of a ValidationException and an Exception related to DB connection or Infrastructure. 
    /// Default: Error.
    /// </summary>
    public LogSeverity Severity { get; protected set; }

    public override string ToString()
    {
        string baseMessage = base.ToString();
        if (!string.IsNullOrEmpty(Message))
        {
            baseMessage = baseMessage.Replace(Message, $"{Message}, TechnicalMessage: {TechnicalMessage}");
        }
        else
        {
            if (InnerException != null)
            {
                int index = baseMessage.IndexOf("--->");
                if (index >= 0)
                    baseMessage = baseMessage.Insert(index, $"TechnicalMessage: {TechnicalMessage}");
            }
            else
            {
                baseMessage = baseMessage + $" TechnicalMessage: {TechnicalMessage}";
            }
        }

        return baseMessage;
    }
}
